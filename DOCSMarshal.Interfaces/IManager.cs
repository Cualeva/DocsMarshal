using System;
namespace DocsMarshal.Interfaces
{
    public interface IManager: IDisposable
    {
        DocsMarshal.Interfaces.Managers.Profile.IProfileManager Profile { get; }
        bool Logon(string username, string password, string softwareName);
        bool Logon(string staticSessionId, string softwareName);
    }

}
