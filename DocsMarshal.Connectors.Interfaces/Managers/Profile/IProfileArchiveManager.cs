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
    }

}
