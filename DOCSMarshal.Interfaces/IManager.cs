using System;
using System.Threading.Tasks;

namespace DocsMarshal.Interfaces
{
    public interface IManager: IDisposable
    {
        string SessionId { get; }
        DocsMarshal.Interfaces.Managers.Profile.IProfileManager Profile { get; }
        DocsMarshal.Interfaces.Managers.Portal.IPortalManager Portal { get; }
        Task<DocsMarshal.Entities.LogonToken> Logon(string username, string password, string softwareName);
        bool Logon(string staticSessionId, string softwareName);
    }

}
