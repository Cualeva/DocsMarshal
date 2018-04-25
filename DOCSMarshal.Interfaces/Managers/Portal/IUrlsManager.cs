using System;
using System.Collections.Generic;

namespace DocsMarshal.Interfaces.Managers.Portal
{
    public interface IUrlsManager: IDisposable
    {
        string Profile_GetDocumentByFieldId(Guid objectId, int fieldId);
        string Profile_GetDocumentByFieldId(Guid objectId, int fieldId, string staticSessionId);
        string Profile_GetDocumentByFieldExternalId(Guid objectId, string fieldExternalId);
        string Profile_GetDocumentByFieldExternalId(Guid objectId, string fieldExternalId, string staticSessionId);
    }
}
