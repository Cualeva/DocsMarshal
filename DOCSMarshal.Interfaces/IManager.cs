using System;
namespace DocsMarshal.Interfaces
{
    public interface IManager: IDisposable
    {
        string SessionId { get; }
        DocsMarshal.Interfaces.Managers.Profile.IProfileManager Profile { get; }
        DocsMarshal.Interfaces.Managers.Portal.IPortalManager Portal { get; }
        bool Logon(string username, string password, string softwareName);
        bool Logon(string staticSessionId, string softwareName);
    }

}
