using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Configuration
{
    public interface IClassTypesManager : IDisposable
    {
        IClassTypeGrantsManager Grants { get; }

        Task<List<Entities.ClassType>> GetAll();
    }
}