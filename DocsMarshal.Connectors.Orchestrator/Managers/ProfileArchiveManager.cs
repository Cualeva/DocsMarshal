using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Connectors.Entities;
using DocsMarshal.Connectors.Interfaces.Managers.Profile;
using Newtonsoft.Json;

namespace DocsMarshal.Connectors.Orchestrator.Managers
{
    public class ProfileArchiveManager : DocsMarshal.Connectors.Interfaces.Managers.Profile.IProfileArchiveManager
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

        public async Task<ProfileInserted> Insert(DocsMarshal.Connectors.Entities.ProfileForInsert profile)
        {
            return await InsertAsync(profile);
        }
        public ProfileForInsert GetNewInstanceForInsertByClassType(string classTypeExternalId)
        {
            return Manager.From_Async_To_Sync(() => GetNewInstanceForInsertByClassTypeAsync(classTypeExternalId));
        }
        public async Task<ProfileForInsert> GetNewInstanceForInsertByClassTypeAsync(string classTypeExternalId)
        {
            if (string.IsNullOrWhiteSpace(classTypeExternalId)) throw new ArgumentNullException("classTypeExternalId");
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/{1}", Orchestrator.DocsMarshalUrl, "/DMProfile/GetProfileForInsertByClasstypeExternalId");
                UriBuilder builder = new UriBuilder(url);
                builder.Query = string.Format("sessionId={0}&classtypeExternalId={1}", Orchestrator.SessionId, classTypeExternalId);
                var response = await client.GetAsync(builder.Uri);
                string rit = await response.Content.ReadAsStringAsync();
                var ritO = JsonConvert.DeserializeObject<DMBaseJsonResult<ProfileForInsert>>(rit);
                return ritO?.result;
            }
        }
        public ProfileForInsert GetNewInstanceForInsertByObjectId(Guid objectId)
        {
            return Manager.From_Async_To_Sync(() => GetNewInstanceForInsertByObjectIdAsync(objectId));
        }
        public async Task<ProfileForInsert> GetNewInstanceForInsertByObjectIdAsync(Guid objectId)
        {
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/{1}", Orchestrator.DocsMarshalUrl, "/DMProfile/GetProfileForInsertByObjectId");
                UriBuilder builder = new UriBuilder(url);
                builder.Query = string.Format("sessionId={0}&objectId={1}", Orchestrator.SessionId, objectId);
                var response = await client.GetAsync(builder.Uri);
                string rit = await response.Content.ReadAsStringAsync();
                var ritO = JsonConvert.DeserializeObject<DMBaseJsonResult<ProfileForInsert>>(rit);
                return ritO?.result;
            }
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
                if (profileForInsert == null) throw new ArgumentNullException("profile is null");
                
                /*
                    If they gave me files to upload i save them as temporary files on DMPortal
                    DMProfile/Insert is outdated and does not support the byte[] type but we can't change the types or the call structure
                    We can't also perform profile insert and then a set document because we want just one wf event
                    So, DMProfile/Insert will receive a dictionary of <FieldExternalId, tempId>
                */
                var profileForInsertInternal = new ProfileForInsertInternal(profileForInsert);
                if (profileForInsertInternal.Fields.Any(c => c.ValueType == Entities.Enums.EFieldType.ByteArray))
                {
                    using (var common = new Common(Orchestrator))
                    {
                        foreach (var field in profileForInsertInternal.Fields)
                        {
                            if (String.IsNullOrWhiteSpace(field.ExternalID))
                                continue;
                            if (field.ValueType != Entities.Enums.EFieldType.ByteArray)
                                continue;
                            if (!(field is FieldValueByteArray))
                                continue;
                            var fieldConverted = profileForInsertInternal.GetFieldByteArray_By_ExternalId(field.ExternalID);
                            if (fieldConverted.FileValue?.Content == null || fieldConverted.FileValue.Content.Length == 0)
                                continue;
                            if (profileForInsertInternal.TempFileIds.ContainsKey(field.ExternalID))
                                continue; //field externalId duplication, user error

                            //upload temp file to DMPortal
                            var tempUploadRes = await common.UploadTmpFile(fieldConverted.FileValue);
                            if (tempUploadRes.Error)
                                throw new Exception($"Error while trying to upload file {field.ExternalID}. Error: {tempUploadRes.ErrorDescription}");
                            if (tempUploadRes.LoggedOut)
                                throw new Exception("Logged out");
                            if (!tempUploadRes.Data.HasValue)
                                continue;
                            fieldConverted.Value = null;
                            fieldConverted.FileValue = null;
                            profileForInsertInternal.TempFileIds.Add(field.ExternalID, tempUploadRes.Data.Value);
                        }
                    }
                }
                var rit = await Orchestrator.PostAsync("/DMProfile/Insert", new { sessionID = Orchestrator.SessionId, ProfileForInsert = profileForInsertInternal, emptyFileFieldsResponse = true }, new { Result = new Entities.ProfileInserted() });

                if (rit.Result.HasError)
                {
                    //If something went wrong, i try to remove the previous uploaded temp files
                    try
                    {
                        using (var common = new Common(Orchestrator))
                        {
                            foreach (var tempId in profileForInsertInternal.TempFileIds)
                                await common.DeleteUploadedTmpFile(tempId.Value);
                        }
                    }
                    catch (Exception e) { }
                }

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
                var rit = await Orchestrator.PostAsync<BaseJsonResult>("/Profile/ChangeStatus", new
                {
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

        private class ProfileForInsertInternal : Connectors.Entities.ProfileForInsert
        {
            public ProfileForInsertInternal()
            {
                TempFileIds = new Dictionary<string, Guid>();
            }
            public ProfileForInsertInternal(Connectors.Entities.ProfileForInsert pInsert)
            {
                ClassTypeExternalID = pInsert.ClassTypeExternalID;
                RaiseWorkflowEvents = pInsert.RaiseWorkflowEvents;
                DomainExternalID = pInsert.DomainExternalID;
                ClassTypeExternalID = pInsert.ClassTypeExternalID;
                ObjectStateExternalID = pInsert.ObjectStateExternalID;
                LanguageCode = pInsert.LanguageCode;
                Fields = pInsert.Fields;
                TempFileIds = new Dictionary<string, Guid>();
            }

            public Dictionary<string, Guid> TempFileIds { get; set; } //FieldExternalId - TempId
        }
    }
}
