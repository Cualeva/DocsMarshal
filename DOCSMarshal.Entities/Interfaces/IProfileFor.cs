using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities.Interfaces
{
    public interface IProfileFor: IDisposable
    {
        Boolean RaiseWorkflowEvents { get; set; }
        string DomainExternalID { get; set; }
        string ClassTypeExternalID { get; set; }
        string ObjectStateExternalID { get; set; }
        string LanguageCode { get; set; }
        List<FieldValue> Fields { get; set; }

        FieldValue GetField_By_ExternalId(string ExternalId);
        FieldValueString GetFieldString_By_ExternalId(string externalId);
        FieldValueGuid GetFieldGuid_By_ExternalId(string externalId);
        FieldValueDecimal GetFieldDecimal_By_ExternalId(string externalId);
        FieldValueDecimal GetFieldDouble_By_ExternalId(string externalId);
        FieldValueInt GetFieldInt_By_ExternalId(string externalId);
        FieldValueBoolean GetFieldBool_By_ExternalId(string externalId);
        FieldValueDateTime GetFieldDateTime_By_ExternalId(string externalId);
        FieldValueDateTime GetFieldDate_By_ExternalId(string externalId);
        FieldValueLang GetFieldMultilanguage_ByExternalId(string externalId, string lang);

    }
}
