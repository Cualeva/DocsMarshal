using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities.Interfaces
{
    public interface IProfileFor
    {
        Boolean RaiseWorkflowEvents { get; set; }
        string DomainExternalID { get; set; }
        string ClassTypeExternalID { get; set; }
        string ObjectStateExternalID { get; set; }
        string LanguageCode { get; set; }
        List<FieldValue> Fields { get; set; }
    }
}
