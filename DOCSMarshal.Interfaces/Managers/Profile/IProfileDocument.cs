using System;
using System.Threading.Tasks;

namespace DocsMarshal.Interfaces.Managers.Profile
{
    public interface IProfileDocumentManager: IDisposable
    {
        DocsMarshal.Entities.ProfileDocumentResponse GetDefaultDocument(Guid objectId);
        Task<DocsMarshal.Entities.ProfileDocumentResponse> GetDefaultDocumentAsync (Guid objectId);
        Task<DocsMarshal.Entities.FileValue> GetDocumentByFieldId(Guid objectId, int fieldId);
        Task<DocsMarshal.Entities.FileValue> GetDocumentByExternalFieldId(Guid objectId, string externalId);
    }
}
