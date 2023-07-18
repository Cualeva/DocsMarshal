using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Profile
{
    public interface IProfileArchiveManager: IDisposable
    {
        Task<Entities.ProfileInserted> Insert(Entities.ProfileForInsert profileForInsert);
        //Entities.ProfileForUpdate GetNewInstanceForUpdate(Guid objectId);
        Task<Entities.ProfileUpdated> Update(Entities.ProfileForUpdate profileForUpdate);
        Task<Entities.ProfileDeleted> Delete(Guid objectId);
        Task<Entities.BaseJsonResult> ChangeStatusAsync(List<Guid> objectIds, int? objectStateId, string objectStateExternalId);
        Entities.BaseJsonResult ChangeStatus(List<Guid> objectIds, int? objectStateId, string objectStateExternalId);
    }

}
