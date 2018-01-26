using System;
using DocsMarshal.Interfaces.Managers.Profile;

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
        }

        public IProfileManager Profile => throw new NotImplementedException();

        public void Dispose()
        {
           
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
