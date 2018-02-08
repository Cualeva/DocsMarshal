using System;
using DocsMarshal.Interfaces.Managers.Profile;
using DocsMarshal.Interfaces.Managers.Portal;
using DocsMarshal.Interfaces;

namespace DocsMarshal.Orchestrator
{
    public class Manager : DocsMarshal.Interfaces.IManager
    {
        public string DocsMarshalUrl
        {
            get;
            private set;
        }
        public string SessionId { get; private set; }
        public string SoftwareName { get; set; }

        private Manager(){}
        public Manager(string docsmarshalUrl)
        {
            if (string.IsNullOrWhiteSpace(docsmarshalUrl)) throw new ArgumentNullException("DocsMarshalUrl cannot be empty");
            DocsMarshalUrl = docsmarshalUrl;
            Profile = new Managers.ProfileManager(this);
            Portal = new Managers.PortalManager(this);
        }

        public IProfileManager Profile { get; private set; }
        public IPortalManager Portal { get; private set; }

        public void Dispose()
        {
            if (Profile != null) { Profile.Dispose(); Profile = null; };
            if (Portal != null) { Portal.Dispose(); Portal = null; };
        }

        public bool Logon(string username, string password, string softwareName)
        {
            throw new NotImplementedException();
        }

        public bool Logon(string staticSessionId, string softwareName)
        {
            if (string.IsNullOrWhiteSpace(staticSessionId)) throw new ArgumentNullException("staticSessionId cannot be empty");
            SessionId = staticSessionId;
            SoftwareName = softwareName;
            return true;
        }
    }
}
