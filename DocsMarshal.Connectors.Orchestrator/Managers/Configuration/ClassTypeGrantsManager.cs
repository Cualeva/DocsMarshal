using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using DocsMarshal.Connectors.Orchestrator.Models;
using System.Linq;

namespace DocsMarshal.Connectors.Orchestrator.Managers.Configuration
{
    public class ClassTypeGrantsManager : Interfaces.Managers.Configuration.IClassTypeGrantsManager
    {
        private Manager Orchestrator = null;

        public ClassTypeGrantsManager(Manager manager)
        {
            Orchestrator = manager;
        }

        public async Task<List<Entities.ClassTypeGrant>> GetUserGrants(int? classTypeId, int? domainId)
        {
            var result = await Orchestrator.PostAsync("/Configuration/GetUserClassTypeGrants", new { sessionId = Orchestrator.SessionId }, new BaseJsonModel<List<Entities.ClassTypeGrant>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            if (classTypeId != null)
                result.Data = result.Data.Where(x => x.ClassTypeId == classTypeId).ToList();
            if (domainId != null)
                result.Data = result.Data.Where(x => x.DomainId == domainId).ToList();
            return result.Data;
        }

        public void Dispose()
        {
            if (Orchestrator != null) Orchestrator = null;
        }
    }
}