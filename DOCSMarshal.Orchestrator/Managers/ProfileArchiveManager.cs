using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Entities;
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

        public ProfileForInsert GetNewInstanceForInsertByClassTypeExternalId(string classTypeExternalId)
        {
            return GetNewInstanceForInsertByClassTypeExternalIdAsync(classTypeExternalId).Result;

        }

        public async Task<ProfileForInsert> GetNewInstanceForInsertByClassTypeExternalIdAsync(string classTypeExternalId)
        {
            if (string.IsNullOrWhiteSpace(classTypeExternalId)) throw new ArgumentNullException("classTypeExternalId");
            using (var client = new HttpClient())
            {
                string url = string.Format("{0}/{1}", Orchestrator.DocsMarshalUrl, "/DMProfile/GetProfileForInsertByClasstypeExternalId");
                UriBuilder builder = new UriBuilder(url);
                builder.Query = string.Format("sessionId={0}&classtypeExternalId={1}",Orchestrator.SessionId, classTypeExternalId);
                var serializedItem = JsonConvert.SerializeObject(new { sessionID = Orchestrator.SessionId, classtypeExternalId = classTypeExternalId });
                var response = await client.GetAsync(builder.Uri);
                string rit = response.Content.ReadAsStringAsync().Result;
                var ritO = JsonConvert.DeserializeObject<root>(rit);
                return ritO.result;
            }
        }

        class root
        {
            public DocsMarshal.Entities.ProfileForInsert result { get; set; }
        }
    }
}
