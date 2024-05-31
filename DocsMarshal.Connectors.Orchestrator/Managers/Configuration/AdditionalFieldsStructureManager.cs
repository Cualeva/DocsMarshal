using DocsMarshal.Connectors.Orchestrator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DocsMarshal.Connectors.Entities;

namespace DocsMarshal.Connectors.Orchestrator.Managers.Configuration
{
    public class AdditionalFieldsStructureManager : Interfaces.Managers.Configuration.IAdditionalFieldsStructureManager
    {
        private Manager Orchestrator = null;

        public AdditionalFieldsStructureManager(Manager manager)
        {
            Orchestrator = manager;
        }

        public async Task<List<FieldDescriptor>> GetAll()
        {
            var result = await Orchestrator.PostAsync("/Config/AdditionalFieldsStructure/GetAll", new { sessionId = Orchestrator.SessionId }, new BaseJsonModel<List<FieldDescriptor>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }
        public async Task<List<FieldDescriptorInClass>> GetByClassTypeId(int classTypeId, bool loadSystemFields)
        {
            var result = await Orchestrator.PostAsync("/Config/AdditionalFieldsStructure/GetByClassTypeId", new { sessionId = Orchestrator.SessionId, classTypeId, loadSystemFields }, new BaseJsonModel<List<FieldDescriptorInClass>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }
        public async Task<List<FieldDescriptorInClass>> GetByClassTypeExternalId(string classExternalId, bool loadSystemFields)
        {
            var result = await Orchestrator.PostAsync("/Config/AdditionalFieldsStructure/GetByClassTypeExternalId", new { sessionId = Orchestrator.SessionId, classExternalId, loadSystemFields }, new BaseJsonModel<List<FieldDescriptorInClass>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }
        public async Task<FieldDescriptor> GetById(int fieldId)
        {
            var result = await Orchestrator.PostAsync("/Config/AdditionalFieldsStructure/GetById", new { sessionId = Orchestrator.SessionId, fieldId }, new BaseJsonModel<FieldDescriptor>());
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
