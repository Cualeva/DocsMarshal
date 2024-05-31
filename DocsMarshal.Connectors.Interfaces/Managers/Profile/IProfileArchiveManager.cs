using DocsMarshal.Connectors.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Profile
{
    public interface IProfileArchiveManager: IDisposable
    {
        Task<ProfileInserted> Insert(ProfileForInsert profileForInsert);
        //Entities.ProfileForUpdate GetNewInstanceForUpdate(Guid objectId);
        Task<ProfileUpdated> Update(ProfileForUpdate profileForUpdate);
        Task<ProfileDeleted> Delete(Guid objectId);
        Task<BaseJsonResult> ChangeStatusAsync(List<Guid> objectIds, int? objectStateId, string objectStateExternalId);
        BaseJsonResult ChangeStatus(List<Guid> objectIds, int? objectStateId, string objectStateExternalId);
        ProfileForInsert GetNewInstanceForInsertByClassType(string classTypeExternalId);
        Task<ProfileForInsert> GetNewInstanceForInsertByClassTypeAsync(string classTypeExternalId);
        ProfileForInsert GetNewInstanceForInsertByObjectId(Guid objectId);
        Task<ProfileForInsert> GetNewInstanceForInsertByObjectIdAsync(Guid objectId);
        Task<DMException> CanUpdateAsync(Guid objectId);
        DMException CanUpdate(Guid objectId);
        Task<DMException> CanUpdateDocumentAsync(Guid objectId);
        DMException CanUpdateDocument(Guid objectId);
        Task<DMException> CanDeleteAsync(Guid objectId);
        DMException CanDelete(Guid objectId);
        Task<DMException> CanShareAsync(Guid objectId);
        DMException CanShare(Guid objectId);
    }

}
