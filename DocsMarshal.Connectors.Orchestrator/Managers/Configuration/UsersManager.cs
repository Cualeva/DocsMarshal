using DocsMarshal.Connectors.Entities;
using DocsMarshal.Connectors.Orchestrator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Orchestrator.Managers.Configuration
{
    public class UsersManager : Interfaces.Managers.Configuration.IUsersManager
    {
        private Manager Orchestrator = null;

        public UsersManager(Manager manager)
        {
            Orchestrator = manager;
        }

        public async Task<List<User>> GetAll()
        {
            var result = await Orchestrator.PostAsync("/Config/Users/GetAll", new { sessionId = Orchestrator.SessionId }, new BaseJsonModel<List<User>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public async Task<User> GetById(int securityIdentityId)
        {
            var result = await Orchestrator.PostAsync("/Config/Users/GetById", new { sessionId = Orchestrator.SessionId, securityIdentityId }, new BaseJsonModel<User>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public async Task<List<User>> GetByName(string name)
        {
            var result = await Orchestrator.PostAsync("/Config/Users/GetByName", new { sessionId = Orchestrator.SessionId, name }, new BaseJsonModel<List<User>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public async Task<User> GetByNameDomainId(string name, int domainId)
        {
            var result = await Orchestrator.PostAsync("/Config/Users/GetByName", new { sessionId = Orchestrator.SessionId, name, domainId }, new BaseJsonModel<User>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public async Task<User> Insert(User user)
        {
            var result = await Orchestrator.PostAsync("/Config/Users/Insert", new { sessionId = Orchestrator.SessionId, element = user }, new BaseJsonModel<User>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public async Task<User> Update(User user)
        {
            var result = await Orchestrator.PostAsync("/Config/Users/Update", new { sessionId = Orchestrator.SessionId, element = user }, new BaseJsonModel<User>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }

        public async Task<User> Delete(User user)
        {
            var result = await Orchestrator.PostAsync("/Config/Users/Delete", new { sessionId = Orchestrator.SessionId, element = user }, new BaseJsonModel<User>());
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
