using DocsMarshal.Connectors.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Configuration
{
    public interface ILanguagesManager : IDisposable
    {
        Task<List<Language>> GetAll();
        Task<Language> GetById(int domainId);
        Task<Language> GetByCode(string code);
    }
}