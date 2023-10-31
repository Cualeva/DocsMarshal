using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Configuration
{
    public interface IObjectStatesManager : IDisposable
    {
        Task<List<Entities.ObjectState>> GetAll();
        Task<List<Entities.ObjectState>> GetByClassTypeId(int classTypeId);
        Task<List<Entities.ObjectState>> GetByClassTypeExternalId(string classTypeExternalId);
    }
}