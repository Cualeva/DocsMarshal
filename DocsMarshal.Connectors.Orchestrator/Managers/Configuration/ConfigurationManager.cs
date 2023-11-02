using DocsMarshal.Connectors.Interfaces.Managers.Configuration;

namespace DocsMarshal.Connectors.Orchestrator.Managers.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        private Manager Orchestrator = null;

        public ConfigurationManager(Manager manager)
        {
            Orchestrator = manager;
            ClassTypes = new ClassTypesManager(Orchestrator);
            Domains = new DomainsManager(Orchestrator);
            Languages = new LanguagesManager(Orchestrator);
            ObjectStates = new ObjectStatesManager(Orchestrator);
            Users = new UsersManager(Orchestrator);
            AdditionalFieldsStructure = new AdditionalFieldsStructureManager(Orchestrator);
        }

        public IClassTypesManager ClassTypes { get; private set; }
        public IDomainsManager Domains { get; private set; }
        public ILanguagesManager Languages { get; private set; }
        public IAdditionalFieldsStructureManager AdditionalFieldsStructure { get; private set; }
        public IObjectStatesManager ObjectStates { get; private set; }
        public IUsersManager Users { get; private set; }

        public void Dispose()
        {
            if (ClassTypes != null) { ClassTypes.Dispose(); ClassTypes = null; }
            if (Domains != null) { Domains.Dispose(); Domains = null; }
            if (ObjectStates != null) { ObjectStates.Dispose(); ObjectStates = null; }
            if (Users != null) { Users.Dispose(); Users = null; }
            if (Languages != null) { Languages.Dispose(); Languages = null; }
            if (AdditionalFieldsStructure != null) { AdditionalFieldsStructure.Dispose(); AdditionalFieldsStructure = null; }
            if (Orchestrator != null) Orchestrator = null;
        }
    }
}