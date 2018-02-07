using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Entities;
using Newtonsoft.Json;

namespace DocsMarshal.Orchestrator.Managers
{
    public class ProfileQueryManager : DocsMarshal.Interfaces.Managers.Profile.IProfileQueryManager
    {
        private Manager Orchestrator = null;

        public ProfileQueryManager(DocsMarshal.Orchestrator.Manager manager)
        {
            Orchestrator = manager;
        }

        public void Dispose()
        {
            if (Orchestrator != null) Orchestrator = null;
        }


        public async Task<ProfileSearchResult> ExecuteAsync(ProfileSearch query)
        {
            if (query == null) throw new ArgumentNullException("query cannot be null");
            var serializedItem = JsonConvert.SerializeObject(query);
            using (var client = new HttpClient())
            {
                try
                {
                    var url = query.SearchUrl(Orchestrator.DocsMarshalUrl);
                    if (string.IsNullOrWhiteSpace(query.sessionID)) query.sessionID = Orchestrator.SessionId;
                    var response = await client.PostAsync(query.SearchUrl(Orchestrator.DocsMarshalUrl), new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    var ricerca = new DocsMarshal.Entities.ProfileSearch();
                    string rit = response.Content.ReadAsStringAsync().Result;
                    var ritO = JsonConvert.DeserializeObject<Root>(rit);
                    if (ritO != null && ritO.Result != null)
                    {
                        if (ritO.Result.Profiles == null) ritO.Result.Profiles = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
                    }
                    return ritO.Result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }

        }

        private class Root
        {
            public DocsMarshal.Entities.ProfileSearchResult Result { get; set; }
        }



    }
}

