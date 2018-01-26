using System;
using System.Threading.Tasks;

namespace DocsMarshal.Interfaces.Managers.Profile
{
    public interface IProfileQueryManager: IDisposable
    {
        DocsMarshal.Entities.ProfileSearchResult Execute(DocsMarshal.Entities.ProfileSearch query);
        Task<DocsMarshal.Entities.ProfileSearchResult> ExecuteAsync(DocsMarshal.Entities.ProfileSearch query);
    }
}