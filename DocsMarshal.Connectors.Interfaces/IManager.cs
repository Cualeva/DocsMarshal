using System;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces
{
    public interface IManager: IDisposable
    {
        string SessionId { get; }
        Managers.Profile.IProfileManager Profile { get; }
        Managers.Portal.IPortalManager Portal { get; }
        Managers.Workflow.IWorkflowManager Workflow { get; }
        Managers.Sources.ISource Sources { get; }
        Managers.Configuration.IConfigurationManager Configuration { get; }
        Task<Entities.LogonToken> Logon(string username, string password, string softwareName);
        bool Logon(string staticSessionId, string softwareName);
        bool Logoff();
    }
}