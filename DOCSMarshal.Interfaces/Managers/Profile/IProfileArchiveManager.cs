using System;
using System.Threading.Tasks;

namespace DocsMarshal.Interfaces.Managers.Profile
{
    public interface IProfileArchiveManager: IDisposable
    {
        Task<Entities.ProfileInserted> Insert(Entities.ProfileForInsert profileForInsert);
        //Entities.ProfileForUpdate GetNewInstanceForUpdate(Guid objectId);
        Task<Entities.ProfileUpdated> Update(Entities.ProfileForUpdate profileForUpdate);
        Task<Entities.ProfileDeleted> Delete(Guid objectId);
    }

}
