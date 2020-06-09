using DocsMarshal.Entities.Interfaces;
using DocsMarshal.MVVM.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DocsMarshal.MVVM.Services
{
    public abstract class BaseDmDataStore<T> : IDataStore<T> where T : Models.BaseModelEntity, new()
    {
        // abstract methods
        protected abstract SQLite.SQLiteAsyncConnection GetConnection();
        protected abstract Task EmptyTable(SQLite.SQLiteAsyncConnection connection, Type type);
        protected abstract bool IsDeviceConnectedToInternet();
        public abstract void Filler(T model, IProfile profile);
        public abstract void Filler(DocsMarshal.Entities.ProfileForBase profileFor, T model);

        // virtual methods
        protected virtual OfflineManager GetOfflineManager() => null;
        protected virtual string GetCurrentDomainExternalId() => null;
        protected virtual string GetCurrentLanguageCode() => null;
        protected virtual string GetCacheKey() => $"cache_last_update_{GetType().FullName}";
        protected virtual TimeSpan GetCacheDuration() => TimeSpan.FromDays(1);
        protected virtual bool ShouldRefreshCache(DateTime lastUpdate) => DateTime.Now - lastUpdate > GetCacheDuration();
        protected virtual void SetLastUpdate(DateTime? lastUpdate)
        {
            var key = GetCacheKey();
            var app = Application.Current;
            if (lastUpdate.HasValue) // set
                app.Properties[key] = lastUpdate.Value;
            else // clear
            {
                if (app.Properties.ContainsKey(key))
                    app.Properties.Remove(key);
            }
            app.SavePropertiesAsync();
        }
        protected virtual DateTime? GetLastUpdate()
        {
            var key = GetCacheKey();
            var app = Application.Current;
            if (app.Properties.ContainsKey(key))
                return app.Properties[key] as DateTime?;
            else
                return null;
        }

        // private/protected properties
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

        public async Task Delete(Guid objectId, bool raiseWorkflowEvents = true)
        {
            var items = await GetConnection().Table<T>().Where(x => x.Objectid == objectId).ToListAsync();
            foreach (var item in items)
                await Delete(item, raiseWorkflowEvents);
        }

        public async Task Delete(T model, bool raiseWorkflowEvents = true)
        {
            if (!IsDeviceConnectedToInternet())
            {
                var om = GetOfflineManager();
                if(om != null)
                {
                    await GetConnection().DeleteAsync(model);
                    await om.QueueProfileDelete(model.Objectid);
                    return;
                }
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
                {
                    await om.QueueProfileInsert(profileForInsert);
                    return toInsert;
                }
            }

            var profileInserted = await Orchestrator.Profile.Archive.Insert(profileForInsert);
            if (profileInserted.HasError)
                throw new Exception(profileInserted.Error);

            var inserted = FromProfileToEntity(profileInserted.Profile);
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
                {
                    await om.QueueProfileUpdate(profileForUpdate);
                    return toUpdate;
                }
            }

            var updated = await Orchestrator.Profile.Archive.Update(profileForUpdate);
            if (updated.HasError)
                throw new Exception(updated.Error);

            var updatedModel = FromProfileToEntity(updated.Profile);
            await GetConnection().InsertOrReplaceAsync(updatedModel);
            return updatedModel;
        }

        public T FromProfileToEntity(IProfile profile)
        {
            var model = new T();
            model.LoadStandardFieldFromProfileSearchResult(profile);
            Filler(model, profile);
            return model;
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
                return FromProfileToEntity(profilo);
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

            // force a refresh if the cache expired
            if(useCache && isConnected && !forceRefresh)
            {
                var lastUpdate = GetLastUpdate();
                forceRefresh = !lastUpdate.HasValue || ShouldRefreshCache(lastUpdate.Value);
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"CACHE {(forceRefresh ? "MISS" : "HIT")} - {GetCacheKey()} - Last Update {(lastUpdate == null ? "NULL" : lastUpdate.Value.ToString("dd/MM/yyyy HH:mm:ss"))}");
#endif
            }

            if (useCache && (!forceRefresh || !isConnected))
                return await connection.Table<T>().ToListAsync();
            if (!isConnected)
                throw new Exception("Could not execute search because the device is disconnected");
            if (ricerca == null)
                ricerca = new Entities.ProfileSearch();
            SetDefaultSearchParams(ricerca);
            var risultato = await Orchestrator.Profile.Search.Query.ExecuteAsync(ricerca);
            var ritorno = new List<T>();
            foreach (var elemento in risultato.GetResultAsProfiles())
                ritorno.Add(FromProfileToEntity(elemento));

            if (useCache)
            {
                await EmptyTable(connection, typeof(T));
                await connection.InsertAllAsync(ritorno);
                SetLastUpdate(DateTime.Now);
            }
            return ritorno;
        }

        public async Task Clear()
        {
            var connection = GetConnection();
            await EmptyTable(connection, typeof(T));
            SetLastUpdate(null);
        }
    }
}
