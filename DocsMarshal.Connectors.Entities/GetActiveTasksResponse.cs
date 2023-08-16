using System;
using System.Collections.Generic;

namespace DocsMarshal.Connectors.Entities
{
    public class GetActiveTasksResponse: BaseReturnEntity
    {
        public List<DmTask> Tasks { get; set; }
    }
}
