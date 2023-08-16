using System;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces
{
    public interface IManager: IDisposable
    {
        string SessionId { get; }
        DocsMarshal.Connectors.Interfaces.Managers.Profile.IProfileManager Profile { get; }
        DocsMarshal.Connectors.Interfaces.Managers.Portal.IPortalManager Portal { get; }
        DocsMarshal.Connectors.Interfaces.Managers.Workflow.IWorkflowManager Workflow { get; }
        DocsMarshal.Connectors.Interfaces.Managers.Sources.ISource Sources { get; }
        Task<DocsMarshal.Connectors.Entities.LogonToken> Logon(string username, string password, string softwareName);
        bool Logon(string staticSessionId, string softwareName);
        bool Logoff();
    }

}
