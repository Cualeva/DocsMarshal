using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Connectors.Entities;
using Newtonsoft.Json;

namespace DocsMarshal.Connectors.Orchestrator.Managers
{
    public class ProfileQueryManager : DocsMarshal.Connectors.Interfaces.Managers.Profile.IProfileQueryManager
    {
        private Manager Orchestrator = null;

        public ProfileQueryManager(DocsMarshal.Connectors.Orchestrator.Manager manager)
        {
            Orchestrator = manager;
        }

        public void Dispose()
        {
            if (Orchestrator != null)
                Orchestrator = null;
        }

        public async Task<ProfileSearchResult> ExecuteAsync(ProfileSearch query)
        {
            if (query == null) throw new ArgumentNullException("query cannot be null");
            if (string.IsNullOrWhiteSpace(query.sessionID)) query.sessionID = Orchestrator.SessionId;
            var ritO = await Orchestrator.PostAsync<Root>("/DMSearch/Execute", query, false);
            if (ritO == null || ritO.Result == null)
                throw new Exception("Profile Search result is null");
            if (ritO.Result.HasError)
                throw new Exception(ritO.Result.Error);
            if (ritO.Result.Profiles == null)
                throw new Exception("Profile Search Profiles result is null");
            return ritO.Result;
        }

        public ProfileSearchResult Execute(ProfileSearch query)
        {
            if (query == null) throw new ArgumentNullException("query cannot be null");
            if (string.IsNullOrWhiteSpace(query.sessionID)) query.sessionID = Orchestrator.SessionId;
            var ritO = Orchestrator.Post<Root>("/DMSearch/Execute", query, false);
            if (ritO == null || ritO.Result == null)
                throw new Exception("Profile Search result is null");
            if (ritO.Result.HasError)
                throw new Exception(ritO.Result.Error);
            if (ritO.Result.Profiles == null)
                throw new Exception("Profile Search Profiles result is null");
            return ritO.Result;
        }


        private class Root
        {
            public DocsMarshal.Connectors.Entities.ProfileSearchResult Result { get; set; }
        }
    }
}