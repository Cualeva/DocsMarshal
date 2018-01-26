using System;
using System.Collections.Generic;
using DocsMarshal.Entities;
using DocsMarshal.Interfaces.Managers.Profile;

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

        public ProfileSearchResult ByDynAssExternalId(string dynAssExternalId, Guid objectId)
        {
            throw new NotImplementedException();
        }

        public ProfileSearchResult ByDynAssExternalId(string dynAssExternalId, List<Guid> objectIds)
        {
            throw new NotImplementedException();
        }

        public ProfileSearchResult ByDynAssId(Guid dynAssId, Guid objectId)
        {
            throw new NotImplementedException();
        }

        public ProfileSearchResult ByDynAssId(Guid dynAssId, List<Guid> objectIds)
        {
            throw new NotImplementedException();
        }

        public ProfileSearchResult ById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (Query != null) { Query.Dispose(); Query = null; }
            if (Orchestrator != null) Orchestrator = null;
        }
    }
}
