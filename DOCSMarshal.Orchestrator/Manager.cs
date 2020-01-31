using System;
using DocsMarshal.Interfaces.Managers.Profile;
using DocsMarshal.Interfaces.Managers.Portal;
using DocsMarshal.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Interfaces.Managers.Workflow;

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
            Workflow = new Managers.WorkflowManager(this);
            Sources = new Managers.SourceManager(this);
        }

        public IProfileManager Profile { get; private set; }
        public IPortalManager Portal { get; private set; }
        public IWorkflowManager Workflow { get; private set; }
        public Interfaces.Managers.Sources.ISource Sources { get; private set; }

        public void Dispose()
        {
            if (Profile != null) { Profile.Dispose(); Profile = null; };
            if (Portal != null) { Portal.Dispose(); Portal = null; };
            if (Workflow != null) { Workflow.Dispose(); Workflow = null; };
            if (Sources != null) { Sources.Dispose(); Sources = null; };
        }

        public async Task<Entities.LogonToken> Logon(string username, string password, string softwareName)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException("username cannot be empty");
            using (var client = new HttpClient())
            {
                try
                {
                    var url = string.Format("{0}/DMLogin/Login", DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new{username=username, password=password, softwareName=softwareName});
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
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
                    return new Entities.LogonToken { LoggedIn = false, LogOnError = ex.Message };
                }
            }
        }

        public bool Logon(string staticSessionId, string softwareName)
        {
            if (string.IsNullOrWhiteSpace(staticSessionId)) throw new ArgumentNullException("staticSessionId cannot be empty");
            SessionId = staticSessionId;
            SoftwareName = softwareName;
            return true;
        }

        public bool Logoff()
        {
            SessionId = null;
            SoftwareName = null;
            return true;
        }

        internal async Task<T> PostAsync<T>(string endpoint, object data, bool localTime = true)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}{1}", DocsMarshalUrl, endpoint);
                    var serializedItem = JsonConvert.SerializeObject(data, new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd HH:mm:ss" });
                    var responseHead = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string responseBodyString = await responseHead.Content.ReadAsStringAsync();
                    var parseOptions = new JsonSerializerSettings();
                    if (localTime)
                        parseOptions.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    var responseBodyObj = await Task.Run(() => JsonConvert.DeserializeObject<T>(responseBodyString, parseOptions));
                    return responseBodyObj;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal async Task<T> PostAsync<T>(string endpoint, object data, T instance)
        {
            return await PostAsync<T>(endpoint, data);
        }
    }
}
