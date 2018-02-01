using System;
using System.Threading.Tasks;

namespace DocsMarshal.Interfaces.Managers.Profile
{
    public interface IProfileQueryManager: IDisposable
    {
        Task<DocsMarshal.Entities.ProfileSearchResult> ExecuteAsync(DocsMarshal.Entities.ProfileSearch query);
    }
}