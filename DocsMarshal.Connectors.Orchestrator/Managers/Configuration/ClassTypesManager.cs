using DocsMarshal.Connectors.Interfaces.Managers.Configuration;
using DocsMarshal.Connectors.Orchestrator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DocsMarshal.Connectors.Orchestrator.Managers.Configuration
{
    public class ClassTypesManager : IClassTypesManager
    {
        private Manager Orchestrator = null;
        public IClassTypeGrantsManager Grants { get; private set; }

        public ClassTypesManager(Manager manager)
        {
            Orchestrator = manager;
            Grants = new ClassTypeGrantsManager(Orchestrator);
        }

        public async Task<List<Entities.ClassType>> GetAll()
        {
            var result = await Orchestrator.PostAsync("/Config/Classes/GetAll", new { sessionId = Orchestrator.SessionId }, new BaseJsonModel<List<Entities.ClassType>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }
        public async Task<Entities.ClassType> GetById(int classTypeId)
        {
            var result = await Orchestrator.PostAsync("/Config/Classes/GetById", new { sessionId = Orchestrator.SessionId, classId = classTypeId }, new BaseJsonModel<Entities.ClassType>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public void Dispose()
        {
            if (Grants != null) { Grants.Dispose(); Grants = null; }
            if (Orchestrator != null) Orchestrator = null;
        }
    }
}