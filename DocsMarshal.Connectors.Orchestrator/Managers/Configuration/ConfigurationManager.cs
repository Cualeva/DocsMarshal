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
            ObjectStates = new ObjectStatesManager(Orchestrator);
            Users = new UsersManager(Orchestrator);
        }

        public IClassTypesManager ClassTypes { get; private set; }
        public IDomainsManager Domains { get; private set; }
        public IObjectStatesManager ObjectStates { get; private set; }
        public IUsersManager Users { get; private set; }

        public void Dispose()
        {
            if (ClassTypes != null) { ClassTypes.Dispose(); ClassTypes = null; }
            if (Domains != null) { Domains.Dispose(); Domains = null; }
            if (ObjectStates != null) { ObjectStates.Dispose(); ObjectStates = null; }
            if (Users != null) { Users.Dispose(); Users = null; }
            if (Orchestrator != null) Orchestrator = null;
        }
    }
}