using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities
{
    public class ProfileForInsert : Interfaces.IProfileFor
    {
        public ProfileForInsert()
        {
            Fields = new List<FieldValue>();
        }

        public Boolean RaiseWorkflowEvents { get; set; }
        public string DomainExternalID { get; set; }
        public string ClassTypeExternalID { get; set; }
        public string ObjectStateExternalID { get; set; }
        public string LanguageCode { get; set; }
        public List<FieldValue> Fields { get; set; }
    }

}
