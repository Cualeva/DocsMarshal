using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Entities;
using DocsMarshal.Interfaces.Managers.Profile;
using Newtonsoft.Json;

namespace DocsMarshal.Orchestrator.Managers
{
    public class ProfileArchiveManager: DocsMarshal.Interfaces.Managers.Profile.IProfileArchiveManager
    {
        private Manager Orchestrator = null;

        public ProfileArchiveManager(DocsMarshal.Orchestrator.Manager manager)
        {
            Orchestrator = manager;

        }

        public void Dispose()
        {
            if (Orchestrator != null) Orchestrator = null;
        }

        public async Task<ProfileForInsert> GetNewInstanceForInsertByClassTypeExternalId(string classTypeExternalId)
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
        }

        public async Task<ProfileInserted> Insert(DocsMarshal.Entities.ProfileForInsert profile)
        {
            return await InsertAsync(profile);
        }

        public async Task<ProfileInserted> InsertAsync(DocsMarshal.Entities.ProfileForInsert profile)
        {
            // controllo che il profilo non sia null
            if (profile == null) throw new ArgumentNullException("profile is null");
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/{1}", Orchestrator.DocsMarshalUrl, "/DMProfile/Insert");
                UriBuilder builder = new UriBuilder(url);
                var tmpQuery = string.Format("sessionId={0}&RaiseWorkflowEvents={1}&ClassTypeExternalID={2}", Orchestrator.SessionId,
                                             profile.RaiseWorkflowEvents, profile.DomainExternalID);
                // per ogni field dell profile aggiungo il valore all'url
                var k = 0;
                foreach (var campo in profile.Fields)
                {
                    tmpQuery = string.Format("{0}&ExternalId[{1}]={2}&Value[{1}]={3}&ValueType[{1}]={4}", tmpQuery,
                                             k, campo.ExternalID, campo.Value, campo.FieldType.ToString());
                    k++;
                }
                builder.Query = tmpQuery;
                var response = await client.GetAsync(builder.Uri);
                string rit = await response.Content.ReadAsStringAsync();
                var ritO = JsonConvert.DeserializeObject<inserted>(rit);
                return ritO.result;
            }
        }

        class root
        {
            public DocsMarshal.Entities.ProfileForInsert result { get; set; }

        }

        class inserted
        {
            public DocsMarshal.Entities.ProfileInserted result { get; set; }
        }
    }
}
