using DocsMarshal.Connectors.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Configuration
{
    public interface IDomainsManager : IDisposable
    {
        Task<List<Entities.Domain>> GetAll();
        Task<Domain> GetById(int domainId);
    }
}