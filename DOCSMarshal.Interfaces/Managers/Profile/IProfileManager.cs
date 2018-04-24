using System;
namespace DocsMarshal.Interfaces.Managers.Profile
{
    public interface IProfileManager: IDisposable
    {
        IProfileSearchManager Search { get; }
        IProfileDocumentManager Documents { get; }
        IProfileArchiveManager Archive { get; }
    }
}
