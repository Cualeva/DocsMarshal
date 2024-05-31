using DocsMarshal.Connectors.Orchestrator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DocsMarshal.Connectors.Orchestrator.Managers.Configuration
{
    public class ObjectStatesManager : Interfaces.Managers.Configuration.IObjectStatesManager
    {
        private Manager Orchestrator = null;

        public ObjectStatesManager(Manager manager)
        {
            Orchestrator = manager;
        }

        public async Task<List<Entities.ObjectState>> GetAll()
        {
            var result = await Orchestrator.PostAsync("/Config/ObjectStates/GetAll", new { sessionId = Orchestrator.SessionId }, new BaseJsonModel<List<Entities.ObjectState>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public async Task<List<Entities.ObjectState>> GetByClassTypeId(int classTypeId)
        {
            var result = await Orchestrator.PostAsync("/Config/ObjectStates/GetByClassTypeId", new { sessionId = Orchestrator.SessionId, classTypeId }, new BaseJsonModel<List<Entities.ObjectState>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public async Task<List<Entities.ObjectState>> GetByClassTypeExternalId(string classTypeExternalId)
        {
            var result = await Orchestrator.PostAsync("/Config/ObjectStates/GetByClassTypeExternalId", new { sessionId = Orchestrator.SessionId, classExternalId = classTypeExternalId }, new BaseJsonModel<List<Entities.ObjectState>>());
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
