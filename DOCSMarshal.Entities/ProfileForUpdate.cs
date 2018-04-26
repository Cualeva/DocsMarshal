using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities
{
    public class ProfileForUpdate : Interfaces.IProfileFor
    {
        public ProfileForUpdate()
        {
            Fields = new List<FieldValue>();
        }
        public Guid ObjectId { get; set; }
        public Boolean RaiseWorkflowEvents { get; set; }
        public string DomainExternalID { get; set; }
        public string ClassTypeExternalID { get; set; }
        public string ObjectStateExternalID { get; set; }
        public string LanguageCode { get; set; }
        public List<FieldValue> Fields { get; set; }
    }

}
