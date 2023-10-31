using DocsMarshal.Connectors.Orchestrator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DocsMarshal.Connectors.Entities;

namespace DocsMarshal.Connectors.Orchestrator.Managers.Configuration
{
    public class LanguagesManager : Interfaces.Managers.Configuration.ILanguagesManager
    {
        private Manager Orchestrator = null;

        public LanguagesManager(Manager manager)
        {
            Orchestrator = manager;
        }

        public async Task<List<Entities.Language>> GetAll()
        {
            var result = await Orchestrator.PostAsync("/Config/Languages/GetAll", new { sessionId = Orchestrator.SessionId }, new BaseJsonModel<List<Language>>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }
        public async Task<Language> GetById(int languageId)
        {
            var result = await Orchestrator.PostAsync("/Config/Languages/GetById", new { sessionId = Orchestrator.SessionId, languageId }, new BaseJsonModel<Language>());
            if (result.Error)
                throw new Exception(result.ErrorDescription);
            return result.Data;
        }
        public async Task<Language> GetByCode(string code)
        {
            var result = await Orchestrator.PostAsync("/Config/Languages/GetByCode", new { sessionId = Orchestrator.SessionId, code }, new BaseJsonModel<Language>());
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
