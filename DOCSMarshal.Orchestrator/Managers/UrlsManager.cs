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

        public string Profile_GetDocumentByFieldExternalId(Guid objectId, string fieldExternalId)
        {
            throw new NotImplementedException();
        }

        public string Profile_GetDocumentByFieldExternalId(Guid objectId, string fieldExternalId, string staticSessionId)
        {
            throw new NotImplementedException();
        }

        public string Profile_GetDocumentByFieldId(Guid objectId, int fieldId)
        {
            return Profile_GetDocumentByFieldId(objectId, fieldId, Orchestrator.SessionId);
        }

        public string Profile_GetDocumentByFieldId(Guid objectId, int fieldId, string staticSessionId)
        {
            return string.Format("{0}/Profile/GetDocumentByFieldId?objectId={1}&fieldId={2}&staticSessionId={3}", Orchestrator.DocsMarshalUrl, objectId, fieldId, staticSessionId);
        }
    }
}
