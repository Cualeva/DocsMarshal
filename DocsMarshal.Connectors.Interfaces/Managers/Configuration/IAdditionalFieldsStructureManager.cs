using DocsMarshal.Connectors.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Configuration
{
    public interface IAdditionalFieldsStructureManager : IDisposable
    {
        Task<List<FieldDescriptor>> GetAll();
        Task<List<FieldDescriptorInClass>> GetByClassTypeId(int classTypeId, bool loadSystemFields);
        Task<List<FieldDescriptorInClass>> GetByClassTypeExternalId(string classExternalId, bool loadSystemFields);
        Task<FieldDescriptor> GetById(int fieldId);
    }
}