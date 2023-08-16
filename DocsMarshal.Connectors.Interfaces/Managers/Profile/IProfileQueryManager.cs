using System;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Profile
{
    public interface IProfileQueryManager: IDisposable
    {
        Task<DocsMarshal.Connectors.Entities.ProfileSearchResult> ExecuteAsync(DocsMarshal.Connectors.Entities.ProfileSearch query);
        Entities.ProfileSearchResult Execute(Entities.ProfileSearch query);
    }
}