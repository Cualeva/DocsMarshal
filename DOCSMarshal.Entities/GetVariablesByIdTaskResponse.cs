using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities
{
    public class GetVariablesByIdTaskResponse: BaseReturnEntity
    {
        public List<DmTaskVariable> Variables { get; set; }
    }
}
