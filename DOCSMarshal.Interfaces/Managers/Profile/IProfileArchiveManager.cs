using System;
using System.Threading.Tasks;

namespace DocsMarshal.Interfaces.Managers.Profile
{
    public interface IProfileArchiveManager: IDisposable
    {
        Task<Entities.ProfileForInsert> GetNewInstanceForInsertByClassTypeExternalId(string classTypeExternalId);
        Task<Entities.ProfileInserted> Insert(Entities.ProfileForInsert profileForInsert);
        //Entities.ProfileForUpdate GetNewInstanceForUpdate(Guid objectId);
        //Entities.ProfileUpdated Update(Entities.ProfileForUpdate profileForUpdate);
    }

}
