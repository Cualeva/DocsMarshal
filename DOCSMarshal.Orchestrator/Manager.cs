using System;
using DocsMarshal.Interfaces.Managers.Profile;
using DocsMarshal.Interfaces.Managers.Portal;
using DocsMarshal.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Entities.LogonToken> Logon(string username, string password, string softwareName)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException("username cannot be empty");
            using (var client = new HttpClient())
            {
                try
                {
                    var serializedItem = JsonConvert.SerializeObject(new{username=username, password=password, softwareName=softwareName});
                    var response = await client.PostAsync(Portal.Urls.Login(), new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = response.Content.ReadAsStringAsync().Result;
                    var ritO = await Task.Run(() => JsonConvert.DeserializeAnonymousType(rit, new { Result = new Entities.LogonToken() }).Result);
                    if (ritO.LoggedIn)
                    {
                        SessionId = ritO.SessionId.ToString();
                        SoftwareName = softwareName;
                    }
                    else
                        SessionId = string.Empty;
                    

                   return ritO;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

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
