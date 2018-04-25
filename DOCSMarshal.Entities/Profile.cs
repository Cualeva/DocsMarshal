using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Entities.Interfaces;

namespace DocsMarshal.Entities
{
    public class Profile : Interfaces.IProfile
    {
        public string ClassType { get;  set; }
        public int ClassTypeId { get;  set; }
        public string ClassType_ExternalId { get;  set; }
        public int DeleteStatus { get;  set; }
        public string Domain { get;  set; }
        public int DomainId { get;  set; }
        public string Domain_ExternalId { get;  set; }
        public Guid? IdRegister { get;  set; }
        public DateTime InsertDt { get;  set; }
        public string LanguageCode { get;  set; }
        public int? LanguageId { get;  set; }
        public DateTime LastUpdate { get;  set; }
        public Guid ObjectId { get;  set; }
        public string ObjectState { get;  set; }
        public int ObjectStateId { get;  set; }
        public string ObjectState_ExternalId { get;  set; }
        public string ProtocolCode { get;  set; }
        public int? ProtocolDomainId { get;  set; }
        public DateTime? ProtocolInsertDt { get;  set; }
        public int? ProtocolNumber { get;  set; }
        public int? ProtocolYear { get;  set; }
        public int UserId { get;  set; }

        public List<Field> Fields { get; set; }
        private Dictionary<string, object> ProfileAsDictionary { get; set; }
        private List<Languages> Languages { get; set; }

        public Profile()
        {}

        public Profile(ProfileSearchResult Result, int i)
        {
            if (Result == null)
                throw new ArgumentNullException("ProfileSearchResult");
            if (Result.Profiles == null)
                throw new ArgumentNullException("ProfileSearchResult.Profiles");
            if (Result.Fields == null)
                throw new ArgumentNullException("ProfileSearchResult.Fields");
            if (i < 0 || i >= Result.Profiles.Count)
                throw new IndexOutOfRangeException(string.Format("Profile index {0} not found in ProfileSearchResult", i));

            this.Fields = Result.Fields;
            this.ProfileAsDictionary = Result.Profiles[i];
            this.Languages = Result.Languages;

            this.ObjectId = GetGuidValueFromDictionary("ObjectId").Value;
            this.DomainId = GetIntValueFromDictionary("DomainId").Value;
            this.Domain_ExternalId = GetStringValueFromDictionary("Domain_ExternalId");
            this.Domain = GetStringValueFromDictionary("Domain");
            this.ClassTypeId = GetIntValueFromDictionary("ClassTypeId").Value;
            this.ClassType_ExternalId = GetStringValueFromDictionary("ClassType_ExternalId");
            this.ClassType = GetStringValueFromDictionary("ClassType");
            this.ObjectStateId = GetIntValueFromDictionary("ObjectStateId").Value;
            this.ObjectState_ExternalId = GetStringValueFromDictionary("ObjectState_ExternalId");
            this.ObjectState = GetStringValueFromDictionary("ObjectState");
            if (ProfileAsDictionary.ContainsKey("LanguageId"))
            {
                this.LanguageId = GetIntValueFromDictionary("LanguageId");
                if (LanguageId.HasValue)
                {
                    var langCode = Languages.FirstOrDefault(x => x.Id == LanguageId.Value);
                    if (langCode != null) this.LanguageCode = langCode.Code;
                }
            }
            //this.IdRegister = GetGuidValueFromDictionary("IdRegister");
            //this.ProtocolCode = GetStringValueFromDictionary("ProtocolCode");
            //this.ProtocolDomainId = GetIntValueFromDictionary("ProtocolDomainId");
            //this.ProtocolInsertDt = GetDateTimeValueFromDictionary("ProtocolInsertDt");
            //this.ProtocolNumber = GetIntValueFromDictionary("ProtocolNumber");
            //this.ProtocolYear = GetIntValueFromDictionary("ProtocolYear");
            //this.UserId = GetIntValueFromDictionary("UserId").Value;
            //this.DeleteStatus = GetIntValueFromDictionary("DeleteStatus").Value;
        }

        private object GetValueFromDictionary(string Key)
        {
            if (string.IsNullOrWhiteSpace(Key))
                throw new ArgumentNullException("Key");
            if (this.ProfileAsDictionary == null)
                throw new ArgumentNullException("Profile.ProfileAsDictionary");
            if (!this.ProfileAsDictionary.ContainsKey(Key))
                throw new KeyNotFoundException(string.Format("Field key {0}", Key));
            return this.ProfileAsDictionary[Key];
        }

        private bool? GetBoolValueFromDictionary(string Key)
        {
            var Value = GetValueFromDictionary(Key);
            if (Value == null)
                return null;
            bool Result;
            if (bool.TryParse(Value.ToString(), out Result))
                return Result;
            else
                throw new InvalidCastException(Key);
        }

        private DateTime? GetDateTimeValueFromDictionary(string Key)
        {
            var Value = GetValueFromDictionary(Key);
            if (Value == null)
                return null;
            DateTime Result;
            if (DateTime.TryParse(Value.ToString(), out Result))
                return Result;
            else
                throw new InvalidCastException(Key);
        }

        private decimal? GetDecimalValueFromDictionary(string Key)
        {
            var Value = GetValueFromDictionary(Key);
            if (Value == null)
                return null;
            decimal Result;
            if (decimal.TryParse(Value.ToString(), out Result))
                return Result;
            else
                throw new InvalidCastException(Key);
        }

        private int? GetIntValueFromDictionary(string Key)
        {
            var Value = GetValueFromDictionary(Key);
            if (Value == null)
                return null;
            int Result;
            if (int.TryParse(Value.ToString(), out Result))
                return Result;
            else
                throw new InvalidCastException(Key);
        }

        private Guid? GetGuidValueFromDictionary(string Key)
        {
            var Value = GetValueFromDictionary(Key);
            if (Value == null)
                return null;
            Guid Result;
            if (Guid.TryParse(Value.ToString(), out Result))
                return Result;
            else
                throw new InvalidCastException(Key);
        }

        private string GetStringValueFromDictionary(string Key)
        {
            var Value = GetValueFromDictionary(Key);
            if (Value == null)
                return null;
            else
                return Value.ToString();
        }

        private Field GetField_By_ExternalId(string ExternalId)
        {
            if (string.IsNullOrWhiteSpace(ExternalId))
                throw new ArgumentNullException("ExternalId");
            if (this.Fields == null)
                throw new ArgumentNullException("Profile.Fields");
            var Field = Fields.FirstOrDefault(x => string.Equals(ExternalId, x.ExternalId, StringComparison.CurrentCultureIgnoreCase));
            if (Field == null)
                throw new KeyNotFoundException(string.Format("Field with ExternalId \"{0}\"", ExternalId));
            return Field;
        }

        private Field GetField_By_ExternalId(string ExternalId, DocsMarshal.Entities.Enums.EFieldType Type)
        {
            var Field = GetField_By_ExternalId(ExternalId);
            if (Field.FieldType != Type)
                throw new InvalidCastException(string.Format("Field with ExternalId {0} is not of type {1}", ExternalId, Type.ToString()));
            else
                return Field;
        }

        private Field GetField_By_Id(int Id)
        {
            if (this.Fields == null)
                throw new ArgumentNullException("Profile.Fields");
            var Field = Fields.FirstOrDefault(x => x.Id == Id);
            if (Field == null)
                throw new KeyNotFoundException(string.Format("Field with Id \"{0}\"", Id));
            return Field;
        }

        private Field GetField_By_Id(int Id, DocsMarshal.Entities.Enums.EFieldType Type)
        {
            var Field = GetField_By_Id(Id);
            if (Field.FieldType != Type)
                throw new InvalidCastException(string.Format("Field with Id {0} is not of type {1}", Id, Type.ToString()));
            else
                return Field;
        }

        public object GetFieldValue_By_ExternalId(string ExternalId)
        {
            var Field = GetField_By_ExternalId(ExternalId);
            return GetValueFromDictionary(Field.DbFieldName);
        }

        public bool? GetBoolValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Boolean);
            return GetBoolValueFromDictionary(Field.DbFieldName);
        }

        public DateTime? GetDateTimeValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.DateTime);
            return GetDateTimeValueFromDictionary(Field.DbFieldName);
        }

        public DateTime? GetDateValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Date);
            var Value = GetDateTimeValueFromDictionary(Field.DbFieldName);
            if (Value.HasValue)
                return Value.Value.Date;
            else
                return null;
        }

        public decimal? GetDecimalValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Decimal);
            return GetDecimalValueFromDictionary(Field.DbFieldName);
        }

        public Guid? GetGuidValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Guid);
            return GetGuidValueFromDictionary(Field.DbFieldName);
        }

        public int? GetIntValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Int);
            return GetIntValueFromDictionary(Field.DbFieldName);
        }

        private Languages GetLangage_By_LanguageCode(string LanguageCode)
        {
            if (string.IsNullOrWhiteSpace(LanguageCode))
                throw new ArgumentNullException("LanguageCode cannot be null");
            if (this.Languages == null)
                throw new ArgumentNullException("Languages");
            var Language = this.Languages.FirstOrDefault(x => string.Equals(LanguageCode, x.Code, StringComparison.CurrentCultureIgnoreCase));
            if (Language == null)
                throw new KeyNotFoundException(string.Format("Language code {0} not found", LanguageCode));
            else
                return Language;
        }

        public string GetMultilanguageFieldValueByExternalId(string externalId, string lang)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.MultiLanguage);
            var Language = GetLangage_By_LanguageCode(lang);
            var FieldCode = GetDbFieldCodeByFieldIdLanguageId(Field.Id, Language.Id);
            return GetStringValueFromDictionary(FieldCode);
        }

        public string GetMultilanguageFieldValueById(int id, int languegeId)
        {
            var Field = GetField_By_Id(id, Enums.EFieldType.MultiLanguage);
            var FieldCode = GetDbFieldCodeByFieldIdLanguageId(id, languegeId);
            return GetStringValueFromDictionary(FieldCode);
        }

        public string GetStringValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.String);
            return GetStringValueFromDictionary(Field.DbFieldName);
        }

        public static string GetDbFieldCodeByFieldId(int FieldId)
        {
            return string.Format("C{0}", FieldId);
        }

        public static string GetDbFieldCodeByFieldIdLanguageId(int FieldId, int LanguageId)
        {
            return string.Format("C{0}_{1}", FieldId, LanguageId);
        }

        public Dictionary<string, string> GetMultilanguageFieldValuesByExternalId(string externalId)
        {
            if (this.Languages == null)
                throw new ArgumentNullException("Languages");
            var Field = GetField_By_ExternalId(externalId);
            var Result = new Dictionary<string, string>();
            foreach (var Language in this.Languages)
                Result[Language.Code] = GetStringValueFromDictionary(GetDbFieldCodeByFieldIdLanguageId(Field.Id, Language.Id));
            return Result;
        }

        public Dictionary<int, string> GetMultilanguageFieldValuesById(int id)
        {
            if (this.Languages == null)
                throw new ArgumentNullException("Languages");
            var Field = GetField_By_Id(id);
            var Result = new Dictionary<int, string>();
            foreach (var Language in this.Languages)
                Result[Language.Id] = GetStringValueFromDictionary(GetDbFieldCodeByFieldIdLanguageId(Field.Id, Language.Id));
            return Result;
        }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>(this.ProfileAsDictionary);
        }

        public double? GetDoubleValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Decimal);
            return GetDoubleValueFromDictionary(Field.DbFieldName);
        }

        private double? GetDoubleValueFromDictionary(string key)
        {
            var Value = GetValueFromDictionary(key);
            if (Value == null)
                return null;
            double Result;
            if (double.TryParse(Value.ToString(), out Result))
                return Result;
            else
                throw new InvalidCastException(key);
        }


    }
}