using System;
using DocsMarshal.Connectors.Interfaces.Managers.Profile;

namespace DocsMarshal.Connectors.Orchestrator.Managers
{
    public class ProfileManager: DocsMarshal.Connectors.Interfaces.Managers.Profile.IProfileManager
    {
        private Manager Orchestrator = null;

        public ProfileManager(DocsMarshal.Connectors.Orchestrator.Manager manager)
        {
            Orchestrator = manager;
            Search = new ProfileSearchManager(Orchestrator);
            Documents = new ProfileDocumentManager(Orchestrator);
            Archive = new ProfileArchiveManager(Orchestrator);
        }


        public IProfileSearchManager Search { get; private set; }
        public IProfileDocumentManager Documents { get; private set; }
        public IProfileArchiveManager Archive { get; private set; }

        public void Dispose()
        {
            if (Search != null) { Search.Dispose(); Search = null; }
            if (Documents != null) { Documents.Dispose(); Documents = null; }
            if (Orchestrator != null) Orchestrator = null;
        }
    }
}
