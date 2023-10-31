using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Configuration
{
    public interface IClassTypeGrantsManager : IDisposable
    {
        Task<List<Entities.ClassTypeGrant>> GetUserGrants(int? classTypeId, int? domainId);
    }
}