using System;
namespace DocsMarshal.Interfaces.Managers.Portal
{
    public interface IPortalManager: IDisposable
    {
        IUrlsManager Urls { get; }
    }
}
