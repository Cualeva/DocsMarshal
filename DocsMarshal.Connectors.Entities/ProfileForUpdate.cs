using System;
using System.Collections.Generic;

namespace DocsMarshal.Connectors.Entities
{
    public class ProfileForUpdate :ProfileForBase
    {
        public ProfileForUpdate()
        {
            Fields = new List<FieldValue>();
        }
        public Guid ObjectId { get; set; }
    }

}
