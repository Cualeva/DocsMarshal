using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Connectors.Entities;
using DocsMarshal.Connectors.Interfaces.Managers.Profile;
using Newtonsoft.Json;

namespace DocsMarshal.Connectors.Orchestrator.Managers
{
    public class ProfileArchiveManager: DocsMarshal.Connectors.Interfaces.Managers.Profile.IProfileArchiveManager
    {
        private Manager Orchestrator = null;

        public ProfileArchiveManager(DocsMarshal.Connectors.Orchestrator.Manager manager)
        {
            Orchestrator = manager;

        }

        public void Dispose()
        {
            if (Orchestrator != null) Orchestrator = null;
        }

   /*     public async Task<ProfileForInsert> GetNewInstanceForInsertByClassTypeExternalId(string classTypeExternalId)
        {
            return await GetNewInstanceForInsertByClassTypeExternalIdAsync(classTypeExternalId);
        }

        public async Task<ProfileForInsert> GetNewInstanceForInsertByClassTypeExternalIdAsync(string classTypeExternalId)
        {
            if (string.IsNullOrWhiteSpace(classTypeExternalId)) throw new ArgumentNullException("classTypeExternalId");
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/{1}", Orchestrator.DocsMarshalUrl, "/DMProfile/GetProfileForInsertByClasstypeExternalId");
                UriBuilder builder = new UriBuilder(url);
                builder.Query = string.Format("sessionId={0}&classtypeExternalId={1}",Orchestrator.SessionId, classTypeExternalId);
                var response = await client.GetAsync(builder.Uri);
                string rit = await response.Content.ReadAsStringAsync();
                var ritO = JsonConvert.DeserializeObject<root>(rit);
                return ritO.result;
            }
        }*/

        public async Task<ProfileInserted> Insert(DocsMarshal.Connectors.Entities.ProfileForInsert profile)
        {
            return await InsertAsync(profile);
        }

        public async Task<ProfileDeleted> Delete(Guid objectId)
        {
            return await DeleteAsync(objectId);
        }

        public async Task<ProfileDeleted> DeleteAsync(Guid objectId)
        {
            try
            {
                var rit = await Orchestrator.PostAsync("/DMProfile/Delete", new { sessionID = Orchestrator.SessionId, ObjectId = objectId }, new { Result = new Entities.ProfileDeleted() });
                return rit.Result;
            }
            catch (Exception ex)
            {
                return new ProfileDeleted { Error = ex.Message, HasError = true };
            }

        }
       

        public async Task<ProfileInserted> InsertAsync(DocsMarshal.Connectors.Entities.ProfileForInsert profileForInsert)
        {
            try
            {
                // controllo che il profilo non sia null
                if (profileForInsert == null) throw new ArgumentNullException("profile is null");
                var rit = await Orchestrator.PostAsync("/DMProfile/Insert", new { sessionID = Orchestrator.SessionId, ProfileForInsert = profileForInsert }, new { Result = new Entities.ProfileInserted() });
                return rit.Result;
            }
            catch (Exception ex)
            {
                return new ProfileInserted { Error = ex.Message, HasError = true };
            }

        }

        public async Task<ProfileUpdated> Update(DocsMarshal.Connectors.Entities.ProfileForUpdate profile)
        {
            return await UpdateAsync(profile);
        }

        public async Task<ProfileUpdated> UpdateAsync(DocsMarshal.Connectors.Entities.ProfileForUpdate profileForUpdate)
        {
            try
            {
                // controllo che il profilo non sia null
                if (profileForUpdate == null) throw new ArgumentNullException("profile is null");
                var rit = await Orchestrator.PostAsync("/DMProfile/Update", new { sessionID = Orchestrator.SessionId, ProfileForUpdate = profileForUpdate, ObjectId = profileForUpdate.ObjectId }, new { Result = new Entities.ProfileUpdated() });
                return rit.Result;
            }
            catch (Exception ex)
            {
                return new ProfileUpdated { Error = ex.Message, HasError = true };
            }

        }


        public BaseJsonResult ChangeStatus(List<Guid> objectIds, int? objectStateId, string objectStateExternalId)
        {
            return Manager.From_Async_To_Sync(() => ChangeStatusAsync(objectIds, objectStateId, objectStateExternalId));
        }

        public async Task<BaseJsonResult> ChangeStatusAsync(List<Guid> objectIds, int? objectStateId, string objectStateExternalId)
        {
            try
            {
                var rit = await Orchestrator.PostAsync<BaseJsonResult>("/Profile/ChangeStatus", new { 
                    sessionId = Orchestrator.SessionId,
                    objectIds = objectIds,
                    objectStateId = objectStateId,
                    objectStateExternalId = objectStateExternalId,
                });
                return rit;
            }
            catch (Exception ex)
            {
                return new BaseJsonResult { ErrorDescription = ex.Message, Error = true };
            }
        }

    }
}
