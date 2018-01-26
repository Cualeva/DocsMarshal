using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Entities;
using Newtonsoft.Json;

namespace DocsMarshal.Orchestrator.Managers
{
    public class ProfileDocumentManager: DocsMarshal.Interfaces.Managers.Profile.IProfileDocumentManager
    {
        private Manager Orchestrator = null;

        public ProfileDocumentManager(DocsMarshal.Orchestrator.Manager manager)
        {
            Orchestrator = manager;

        }


        public void Dispose()
        {
            if (Orchestrator != null) Orchestrator = null;
        }


        public ProfileDocumentResponse GetDefaultDocument(Guid objectId)
        {
            return GetDefaultDocumentAsync(objectId).Result;
        }

        public async Task<ProfileDocumentResponse> GetDefaultDocumentAsync (Guid objectId)
        {
            if (Guid.Empty == objectId) throw new ArgumentNullException("ObjectId cannot be empty");
            string defaultUrl = string.Format("{0}/{1}", Orchestrator.DocsMarshalUrl, "/DMDocuments/GetProfileDefaultDocumentByObjectId");
            var serializedItem = JsonConvert.SerializeObject(new { sessionID= Orchestrator.SessionId, objectID = objectId});
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(defaultUrl, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                string rit = response.Content.ReadAsStringAsync().Result;
                var ritO = JsonConvert.DeserializeObject<RootFile>(rit);
                return ritO.result;
            }
        }

        public ProfileDocumentResponse GetDocumentByExternalFieldId(Guid objectId, string externalId)
        {
            throw new NotImplementedException();
        }

        public ProfileDocumentResponse GetDocumentByFieldId(Guid DmObjectId, int fieldId)
        {
            throw new NotImplementedException();
        }

        class RootFile
        {
            public DocsMarshal.Entities.ProfileDocumentResponse result { get; set; }
        }
    }
}
