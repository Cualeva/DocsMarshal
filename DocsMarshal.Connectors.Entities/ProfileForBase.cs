using System;
using System.Collections.Generic;
using System.Linq;

namespace DocsMarshal.Connectors.Entities
{
    public class ProfileForBase : Interfaces.IProfileFor
    {
        public ProfileForBase()
        {
            Fields = new List<FieldValue>();
        }

        public Boolean RaiseWorkflowEvents { get; set; }
        public string DomainExternalID { get; set; }
        public string ClassTypeExternalID { get; set; }
        public string ObjectStateExternalID { get; set; }
        public string LanguageCode { get; set; }
        public string ExternalId { get; set; }
        public List<FieldValue> Fields { get; set; }

        public void Dispose()
        {
            if (Fields != null) Fields.Clear();
            Fields = null;
        }

        public FieldValueBoolean GetFieldBool_By_ExternalId(string externalId)
        {
            var f = GetField_By_ExternalId(externalId);
            if (f == null) return null;
            if (!(f is FieldValueBoolean)) throw new Exception(string.Format("{0} is not a field of type boolean", externalId));
            return (FieldValueBoolean)f;
        }

        public FieldValueDateTime GetFieldDateTime_By_ExternalId(string externalId)
        {
            var f = GetField_By_ExternalId(externalId);
            if (f == null) return null;
            if (!(f is FieldValueDateTime)) throw new Exception(string.Format("{0} is not a field of type DateTime", externalId));
            return (FieldValueDateTime)f;
        }

        public FieldValueDateTime GetFieldDate_By_ExternalId(string externalId)
        {
            var f = GetField_By_ExternalId(externalId);
            if (f == null) return null;
            if (!(f is FieldValueDateTime)) throw new Exception(string.Format("{0} is not a field of type Date", externalId));
            return (FieldValueDateTime)f;
        }

        public FieldValueDecimal GetFieldDecimal_By_ExternalId(string externalId)
        {
            var f = GetField_By_ExternalId(externalId);
            if (f == null) return null;
            if (!(f is FieldValueDecimal)) throw new Exception(string.Format("{0} is not a field of type Decimal", externalId));
            return (FieldValueDecimal)f;
        }

        public FieldValueDecimal GetFieldDouble_By_ExternalId(string externalId)
        {
            var f = GetField_By_ExternalId(externalId);
            if (f == null) return null;
            if (!(f is FieldValueDecimal)) throw new Exception(string.Format("{0} is not a field of type Double", externalId));
            return (FieldValueDecimal)f;
        }

        public FieldValueGuid GetFieldGuid_By_ExternalId(string externalId)
        {
            var f = GetField_By_ExternalId(externalId);
            if (f == null) return null;
            if (!(f is FieldValueGuid)) throw new Exception(string.Format("{0} is not a field of type Guid", externalId));
            return (FieldValueGuid)f;
        }

        public FieldValueInt GetFieldInt_By_ExternalId(string externalId)
        {
            var f = GetField_By_ExternalId(externalId);
            if (f == null) return null;
            if (!(f is FieldValueInt)) throw new Exception(string.Format("{0} is not a field of type Int", externalId));
            return (FieldValueInt)f;
        }
        public FieldValueByteArray GetFieldByteArray_By_ExternalId(string externalId)
        {
            var f = GetField_By_ExternalId(externalId);
            if (f == null) return null;
            if (!(f is FieldValueByteArray)) throw new Exception(string.Format("{0} is not a field of type ByteArray", externalId));
            return (FieldValueByteArray)f;
        }

        public FieldValueLang GetFieldMultilanguage_ByExternalId(string externalId, string lang)
        {
            throw new NotImplementedException("GetFieldMultilanguage_ByExternalId");
        }

        public FieldValueString GetFieldString_By_ExternalId(string externalId)
        {
            var f = GetField_By_ExternalId(externalId);
            if (f == null) return null;
            if (!(f is FieldValueString)) throw new Exception(string.Format("{0} is not a field of type String", externalId));
            return (FieldValueString)f;
        }

        public FieldValue GetField_By_ExternalId(string ExternalId)
        {
            if (Fields == null) return null;
            return Fields.FirstOrDefault(x => String.Equals(x.ExternalID, ExternalId, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
