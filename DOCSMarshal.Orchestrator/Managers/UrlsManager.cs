using System;
using System.Collections.Generic;
using System.Linq;

namespace DocsMarshal.Orchestrator.Managers
{
    public class UrlsManager : DocsMarshal.Interfaces.Managers.Portal.IUrlsManager
    {
        private Manager Orchestrator = null;

        public UrlsManager(DocsMarshal.Orchestrator.Manager manager)
        {
            Orchestrator = manager;
        }

        public void Dispose()
        {
            Orchestrator = null;
        }

        public string DocumentModels_CreateAndDownloadDocumentModelFromObjectId(Guid objectId, string documentModelExternalId)
        {
            if (Orchestrator != null)
                return DocumentModels_CreateAndDownloadDocumentModelFromObjectId(objectId, documentModelExternalId, Orchestrator.SessionId);
            else
                return DocumentModels_CreateAndDownloadDocumentModelFromObjectId(objectId, documentModelExternalId, string.Empty);
        }

        public string DocumentModels_CreateAndDownloadDocumentModelFromObjectId(Guid objectId, string documentModelExternalId, string sessionId)
        {
            return string.Format("{0}/DocumentModel/CreateAndDownloadDocumentModelFromObjectId?objectId={1}&documentModelExternalId={2}&sessionID={3}", Orchestrator.DocsMarshalUrl, objectId, documentModelExternalId, sessionId);
        }

        public string Profile_GetDocumentByFieldExternalId(Guid objectId, string fieldExternalId)
        {
            return Profile_GetDocumentByFieldExternalId(objectId, fieldExternalId, Orchestrator.SessionId);
        }

        public string Profile_GetDocumentByFieldExternalId(Guid objectId, string fieldExternalId, string staticSessionId)
        {
            return string.Format("{0}/Profile/GetDocumentByFieldExternalId?objectId={1}&fieldExternalId={2}&SessionId={3}", Orchestrator.DocsMarshalUrl, objectId, fieldExternalId, staticSessionId);
        }

        public string Profile_GetDocumentByFieldId(Guid objectId, int fieldId)
        {
            return Profile_GetDocumentByFieldId(objectId, fieldId, Orchestrator.SessionId);
        }

        public string Profile_GetDocumentByFieldId(Guid objectId, int fieldId, string staticSessionId)
        {
            return string.Format("{0}/Profile/GetDocumentByFieldId?objectId={1}&fieldId={2}&SessionId={3}", Orchestrator.DocsMarshalUrl, objectId, fieldId, staticSessionId);
        }

      
    }
}
