using System;
using DocsMarshal.Interfaces.Managers.Portal;

namespace DocsMarshal.Orchestrator.Managers
{
    public class PortalManager: DocsMarshal.Interfaces.Managers.Portal.IPortalManager
    {
        private Manager Orchestrator = null;

        public PortalManager(DocsMarshal.Orchestrator.Manager manager)
        {
            Orchestrator = manager;
            Urls = new UrlsManager(Orchestrator);
        } 


        public IUrlsManager Urls { get; private set; }

        public void Dispose()
        {
            if (Urls != null) Urls.Dispose();
        }
    }
}
