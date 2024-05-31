using System;
namespace DocsMarshal.Connectors.Interfaces.Managers.Configuration
{
    public interface IConfigurationManager : IDisposable
    {
        IClassTypesManager ClassTypes { get; }
        IDomainsManager Domains { get; }
        ILanguagesManager Languages { get; }
        IObjectStatesManager ObjectStates { get; }
        IAdditionalFieldsStructureManager AdditionalFieldsStructure { get; }
        IUsersManager Users { get; }
    }
}