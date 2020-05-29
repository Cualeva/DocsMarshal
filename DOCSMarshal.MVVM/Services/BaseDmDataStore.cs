using DocsMarshal.Entities.Interfaces;
using DocsMarshal.MVVM.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.MVVM.Services
{
    public abstract class BaseDmDataStore<T> : IDataStore<T> where T : Models.BaseModelEntity, new()
    {
        protected abstract SQLite.SQLiteAsyncConnection GetConnection();
        protected abstract Task EmptyTable(SQLite.SQLiteAsyncConnection connection, Type type);
        protected abstract bool IsDeviceConnectedToInternet();
        protected virtual OfflineManager GetOfflineManager() => null;
        protected virtual string GetCurrentDomainExternalId() => null;
        protected virtual string GetCurrentLanguageCode() => null;

        //protected CaffeVergnano.Interfaces.ISQLiteDb Db => DependencyService.Get<Interfaces.ISQLiteDb>();
        //protected static CaffeVergnano.Interfaces.ICaffeVergnanoDataStore DataStore => DependencyService.Get<Interfaces.ICaffeVergnanoDataStore>();

        protected DocsMarshal.Interfaces.IManager Orchestrator { get; private set; }
        public string ClassTypeExternalId { get; private set; }
        public bool DomainOriented { get; private set; }
        public bool ManageContentByLanguage { get; private set; }

        public BaseDmDataStore(DocsMarshal.Interfaces.IManager orchestrator, string classTypeExternalId) : this(orchestrator, classTypeExternalId, true, false)
        {
        }

        public BaseDmDataStore(DocsMarshal.Interfaces.IManager orchestrator, string classTypeExternalId, bool domainOriented, bool manageContentByLanguage)
        {
            Orchestrator = orchestrator;
            ClassTypeExternalId = classTypeExternalId;
            DomainOriented = domainOriented;
            ManageContentByLanguage = manageContentByLanguage;
        }

        public abstract void Filler(T model, IProfile profile);
        public abstract void Filler(DocsMarshal.Entities.ProfileForBase profileFor, T model);

        public T MakeEntityFromProfile(IProfile profile)
        {
            var model = new T();
            model.LoadStandardFieldFromProfileSearchResult(profile);
            Filler(model, profile);
            return model;
        }

        public async Task Delete(Guid objectId)
        {
            var items = await GetConnection().Table<T>().Where(x => x.Objectid == objectId).ToListAsync();
            foreach (var item in items)
                await Delete(item);
        }

        public async Task Delete(T model, bool raiseWorkflowEvents = true)
        {
            if (!IsDeviceConnectedToInternet())
            {
                await GetConnection().DeleteAsync(model);
                var om = GetOfflineManager();
                if(om != null)
                    await om.QueueProfileDelete(model.Objectid);
            }

            var ritorno = await Orchestrator.Profile.Archive.Delete(model.Objectid);
            if (ritorno.HasError)
                throw new Exception(ritorno.Error);
            await GetConnection().DeleteAsync(model);
        }

        public virtual async Task<T> Insert(T model, bool raiseWorkflowEvents = true)
        {
            var profileForInsert = FromEntityToProfileForInsert(model, raiseWorkflowEvents);

            if (!IsDeviceConnectedToInternet())
            {
                var toInsert = Models.BaseModelEntity.Clone(model);
                toInsert._IsLocal = true;
                toInsert.Objectid = Guid.NewGuid();
                toInsert.InsertDt = toInsert.LastUpdate = DateTime.Now;
                await GetConnection().InsertAsync(toInsert);
                var om = GetOfflineManager();
                if(om != null)
                    await om.QueueProfileInsert(profileForInsert);
                return toInsert;
            }

            var profileInserted = await Orchestrator.Profile.Archive.Insert(profileForInsert);
            if (profileInserted.HasError)
                throw new Exception(profileInserted.Error);

            var inserted = MakeEntityFromProfile(profileInserted.Profile);
            await GetConnection().InsertOrReplaceAsync(inserted);
            return inserted;
        }

        public virtual async Task<T> Update(T model, bool raiseWorkflowEvents = true)
        {
            var profileForUpdate = FromEntityToProfileForUpdate(model, raiseWorkflowEvents);

            if (!IsDeviceConnectedToInternet())
            {
                var toUpdate = Models.BaseModelEntity.Clone(model);
                toUpdate._IsLocal = true;
                await GetConnection().InsertOrReplaceAsync(toUpdate);
                var om = GetOfflineManager();
                if(om != null)
                    await om.QueueProfileUpdate(profileForUpdate);
                return toUpdate;
            }

            var updated = await Orchestrator.Profile.Archive.Update(profileForUpdate);
            if (updated.HasError)
                throw new Exception(updated.Error);

            var updatedModel = MakeEntityFromProfile(updated.Profile);
            await GetConnection().InsertOrReplaceAsync(updatedModel);
            return updatedModel;
        }

        private DocsMarshal.Entities.ProfileForInsert FromEntityToProfileForInsert(T model, bool raiseWorkflowEvents)
        {
            var forInsert = model.ToProfileForInsert(raiseWorkflowEvents);
            forInsert.ClassTypeExternalID = ClassTypeExternalId;
            if (DomainOriented)
                forInsert.DomainExternalID = GetCurrentDomainExternalId();
            if (ManageContentByLanguage)
                forInsert.LanguageCode = GetCurrentLanguageCode();
            Filler(forInsert, model);
            return forInsert;
        }

        private DocsMarshal.Entities.ProfileForUpdate FromEntityToProfileForUpdate(T model, bool raiseWorkflowEvents)
        {
            var forUpdate = model.ToProfileForUpdate(raiseWorkflowEvents);
            Filler(forUpdate, model);
            return forUpdate;
        }

        private async System.Threading.Tasks.Task<T> GetItemAsync(Guid objectId)
        {
            var profilo = await Orchestrator.Profile.Search.ById(objectId);
            if (profilo == null)
                return default(T);
            else
                return MakeEntityFromProfile(profilo);
        }

        public virtual async Task<IEnumerable<T>> GetItems(bool forceRefresh)
        {
            return await GetItemsAsync(null, forceRefresh: forceRefresh);
        }
        public async Task<T> GetItem(Guid objectId, bool forceReload)
        {
            if (IsDeviceConnectedToInternet())
            {
                var item = await GetItemAsync(objectId);
                if (item == null)
                    return null;
                await GetConnection().InsertOrReplaceAsync(item);
                return item;
            }
            else
            {
                var items = await GetItems(forceReload);
                return items.FirstOrDefault(x => x.Objectid == objectId);
            }
        }

        protected virtual void SetDefaultSearchParams(DocsMarshal.Entities.ProfileSearch search)
        {
            search.sessionID = Orchestrator.SessionId;
            search.classTypeExternalId = ClassTypeExternalId;
            if(DomainOriented)
                search.domainExternalId = GetCurrentDomainExternalId();
        }

        protected virtual async Task<IEnumerable<T>> GetItemsAsync(DocsMarshal.Entities.ProfileSearch ricerca, bool forceRefresh = false, bool useCache = true)
        {
            var connection = GetConnection();
            var isConnected = IsDeviceConnectedToInternet();
            if (useCache && (!forceRefresh || !isConnected))
            {
                var items = await connection.Table<T>().ToListAsync();
                if (items.Count > 0)
                    return items;
            }
            if (!isConnected)
                throw new Exception("Could not execute search because the device is disconnected");
            if (ricerca == null)
                ricerca = new Entities.ProfileSearch();
            SetDefaultSearchParams(ricerca);
            var risultato = await Orchestrator.Profile.Search.Query.ExecuteAsync(ricerca);
            var ritorno = new List<T>();
            foreach (var elemento in risultato.GetResultAsProfiles())
                ritorno.Add(MakeEntityFromProfile(elemento));

            // var canStoreResult = fieldsToSelect == null && (parameters == null || parameters.Count == 0);
            if (useCache)
            {
                await EmptyTable(connection, typeof(T));
                await connection.InsertAllAsync(ritorno);
            }
            return ritorno;
        }

        public abstract Task<IEnumerable<T>> GetLastItemsUpdatesAsync(DateTime? lastUpdate);
    }
}
