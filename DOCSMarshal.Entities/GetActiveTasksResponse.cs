using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities
{
    public class GetActiveTasksResponse: BaseReturnEntity
    {
        public List<DmTask> Tasks { get; set; }
    }
}
