using System;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Profile
{
    public interface IProfileDocumentManager : IDisposable
    {
        DocsMarshal.Connectors.Entities.ProfileDocumentResponse GetDefaultDocument(Guid objectId);
        Task<DocsMarshal.Connectors.Entities.ProfileDocumentResponse> GetDefaultDocumentAsync(Guid objectId);
        Task<DocsMarshal.Connectors.Entities.FileValue> GetDocumentByFieldId(Guid objectId, int fieldId);
        Task<DocsMarshal.Connectors.Entities.FileValue> GetDocumentByExternalFieldId(Guid objectId, string externalId);
        Task<Entities.ProfileDocumentResponse> GetProfileDocumentByFieldExternalIdAsync(Guid objectId, string fieldExternalId);
        Entities.ProfileDocumentResponse GetProfileDocumentByFieldExternalId(Guid objectId, string fieldExternalId);
    }
}
