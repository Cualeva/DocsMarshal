using System;
using System.Collections.Generic;

namespace DocsMarshal.Interfaces.Managers.Profile
{
    public interface IProfileSearchManager: IDisposable
    {
        Entities.ProfileSearchResult ById(Guid Id);
        Entities.ProfileSearchResult ByDynAssId(Guid dynAssId, Guid objectId);
        Entities.ProfileSearchResult ByDynAssId(Guid dynAssId, List<Guid> objectIds);
        Entities.ProfileSearchResult ByDynAssExternalId(string dynAssExternalId, Guid objectId);
        Entities.ProfileSearchResult ByDynAssExternalId(string dynAssExternalId, List<Guid> objectIds);
        IProfileQueryManager Query { get; }
    }
}
