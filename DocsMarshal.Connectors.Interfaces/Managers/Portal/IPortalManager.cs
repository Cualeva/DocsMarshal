using System;
namespace DocsMarshal.Connectors.Interfaces.Managers.Portal
{
    public interface IPortalManager: IDisposable
    {
        IUrlsManager Urls { get; }
    }
}
