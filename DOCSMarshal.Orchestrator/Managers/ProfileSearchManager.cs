using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Entities;
using DocsMarshal.Interfaces.Managers.Profile;
using Newtonsoft.Json;

namespace DocsMarshal.Orchestrator.Managers
{
    public class ProfileSearchManager: DocsMarshal.Interfaces.Managers.Profile.IProfileSearchManager
    {
        private Manager Orchestrator = null;

        public ProfileSearchManager(DocsMarshal.Orchestrator.Manager manager)
        {
            Orchestrator = manager;
            Query = new ProfileQueryManager(manager);
        }

        public IProfileQueryManager Query { get; private set; }

        public Task<Entities.ProfileSearchResult> ByDynAssExternalId(string dynAssExternalId, Guid objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.ProfileSearchResult> ByDynAssExternalId(string dynAssExternalId, List<Guid> objectIds)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.ProfileSearchResult> ByDynAssId(Guid dynAssId, Guid objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.ProfileSearchResult> ByDynAssId(Guid dynAssId, List<Guid> objectIds)
        {
            throw new NotImplementedException();
        }

        public async Task<Entities.Profile> ById(Guid Id)
        {
            var ritO = await Orchestrator.PostAsync("/DMSearch/GetProfileByObjectId", new { ObjectId = Id, sessionId = Orchestrator.SessionId }, new { result = new Root() });
            if (ritO.result == null || ritO.result.Profiles == null) return null;
            var profilo = ritO.result.Profiles.FirstOrDefault();
            return profilo;
        }

        private class CampoMultiLingua
        {
            public int Id { get; set; }
            public string Lang { get; set; }
        }

        private class Root
        {
            public string Error { get; set; }
            public DocsMarshal.Entities.Profile[] Profiles { get; set; }
            public bool HasError { get; set; }
        }

        public void Dispose()
        {
            if (Query != null) { Query.Dispose(); Query = null; }
            if (Orchestrator != null) Orchestrator = null;
        }
    }
}
