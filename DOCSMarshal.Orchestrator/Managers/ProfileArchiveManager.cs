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

        public async Task<ProfileInserted> InsertAsync(DocsMarshal.Entities.ProfileForInsert profileForInsert)
        {
            try
            {
                // controllo che il profilo non sia null
                if (profileForInsert == null) throw new ArgumentNullException("profile is null");
                using (var client = new HttpClient())
                {
                    string url = Orchestrator.Portal.Urls.Insert();
                    UriBuilder builder = new UriBuilder(url);
                    var serializedItem = JsonConvert.SerializeObject(new { sessionID = Orchestrator.SessionId, ProfileForInsert = profileForInsert });
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = response.Content.ReadAsStringAsync().Result;
                    var ritO = await Task.Run(() => JsonConvert.DeserializeAnonymousType(rit, new { Result = new Entities.ProfileInserted() }).Result);
                    return ritO;
                }
            }
            catch (Exception ex)
            {
                return new ProfileInserted { Error = ex.Message, HasError = true };
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
