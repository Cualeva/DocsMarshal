using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Configuration
{
    public interface IUsersManager : IDisposable
    {
        Task<List<Entities.User>> GetAll();
        Task<Entities.User> Insert(Entities.User user);
        Task<Entities.User> Update(Entities.User user);
        Task<Entities.User> Delete(Entities.User user);
    }
}