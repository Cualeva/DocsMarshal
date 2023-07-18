using System;
using DocsMarshal.Connectors.Interfaces.Managers.Portal;

namespace DocsMarshal.Connectors.Orchestrator.Managers
{
    public class PortalManager: DocsMarshal.Connectors.Interfaces.Managers.Portal.IPortalManager
    {
        private Manager Orchestrator = null;

        public PortalManager(DocsMarshal.Connectors.Orchestrator.Manager manager)
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
