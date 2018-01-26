using System;
using System.Threading.Tasks;

namespace DocsMarshal.Interfaces.Managers.Profile
{
    public interface IProfileDocumentManager: IDisposable
    {
        DocsMarshal.Entities.ProfileDocumentResponse GetDefaultDocument(Guid objectId);
        Task<DocsMarshal.Entities.ProfileDocumentResponse> GetDefaultDocumentAsync (Guid objectId);
        DocsMarshal.Entities.ProfileDocumentResponse GetDocumentByFieldId(Guid objectId, int fieldId);
        DocsMarshal.Entities.ProfileDocumentResponse GetDocumentByExternalFieldId(Guid objectId, string externalId);
    }
}
