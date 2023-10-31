using System;
namespace DocsMarshal.Connectors.Interfaces.Managers.Configuration
{
    public interface IConfigurationManager : IDisposable
    {
        IClassTypesManager ClassTypes { get; }
        IDomainsManager Domains { get; }
        IObjectStatesManager ObjectStates { get; }
        IUsersManager Users { get; }
    }
}