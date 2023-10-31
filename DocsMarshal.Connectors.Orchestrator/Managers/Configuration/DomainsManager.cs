using DocsMarshal.Connectors.Orchestrator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DocsMarshal.Connectors.Orchestrator.Managers.Configuration
{
    public class DomainsManager : Interfaces.Managers.Configuration.IDomainsManager
    {
        private Manager Orchestrator = null;

        public DomainsManager(Manager manager)
        {
            Orchestrator = manager;
        }

        public async Task<List<Entities.Domain>> GetAll()
        {
            var result = await Orchestrator.PostAsync("/Config/Domains/GetAll", new { sessionId = Orchestrator.SessionId }, new BaseJsonModel<List<Entities.Domain>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public void Dispose()
        {
            if (Orchestrator != null) Orchestrator = null;
        }
    }
}
