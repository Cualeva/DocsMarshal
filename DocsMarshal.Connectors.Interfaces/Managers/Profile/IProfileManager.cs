using System;
namespace DocsMarshal.Connectors.Interfaces.Managers.Profile
{
    public interface IProfileManager: IDisposable
    {
        IProfileSearchManager Search { get; }
        IProfileDocumentManager Documents { get; }
        IProfileArchiveManager Archive { get; }
    }
}
