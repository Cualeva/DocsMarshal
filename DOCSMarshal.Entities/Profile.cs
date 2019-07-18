using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Entities.Interfaces;
using Newtonsoft.Json;

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
        public Dictionary<string, object> ProfileAsDictionary { get; set; }
        private List<Languages> _Languages = null;
        public List<Languages> Languages { 
            get
            {
                if (_Languages == null)
                {
                    if (Fields != null)
                    {
                        // cerco di recuperare l'elenco delle lingue presenti nei campi aggiuntivi di tipo lingua per popolare 
                        var campiMultiLingua = Fields.Where(x => x.FieldType == Entities.Enums.EFieldType.MultiLanguage).ToList();
                        foreach (var campo in campiMultiLingua)
                        {
                            if (campo.GenericValue == null) continue;
                            var valore = campo.GenericValue.ToString();
                            if (string.IsNullOrWhiteSpace(valore)) continue;
                            var lingue = JsonConvert.DeserializeAnonymousType(valore, new List<Languages>());
                            if (_Languages == null) _Languages = new List<Languages>();
                            foreach (var lingua in lingue)
                            {
                                if (_Languages.Count(x => x.Id == lingua.Id) == 0) _Languages.Add(lingua);
                            }
                        }
                    }
                    else
                        _Languages = new List<Languages>();
                }
                return _Languages;
            }
            set
            {
                _Languages = value;
            }
        }

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
            if (Result.Fields !=null)  
                this.Fields = Result.Fields;
            
            this.ProfileAsDictionary = Result.Profiles[i];
            this.Languages = Result.Languages;
            this.UserId = GetIntValueFromDictionary("UserId").Value;
            this.ObjectId = GetGuidValueFromDictionary("ObjectId").Value;
            this.InsertDt = GetDateTimeValueFromDictionary("InsertDt").Value;
            this.LastUpdate = GetDateTimeValueFromDictionary("LastUpdate").Value;
            this.DomainId = GetIntValueFromDictionary("DomainId").Value;
            this.Domain_ExternalId = GetStringValueFromDictionary("Domain_ExternalId");
            this.Domain = GetStringValueFromDictionary("Domain");
            this.ClassTypeId = GetIntValueFromDictionary("ClassTypeId").Value;
            this.ClassType_ExternalId = GetStringValueFromDictionary("ClassType_ExternalId");
            this.ClassType = GetStringValueFromDictionary("ClassType");
            this.ObjectStateId = GetIntValueFromDictionary("ObjectStateId").Value;
            this.ObjectState_ExternalId = GetStringValueFromDictionary("ObjectState_ExternalId");
            this.ObjectState = GetStringValueFromDictionary("ObjectState");
            if (ProfileAsDictionary != null)
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


        private object GetValueFromDictionary(string Key, bool raiseExceptionIfNotExist)
        {
            if (string.IsNullOrWhiteSpace(Key)) throw new ArgumentNullException("Key");
            if (this.ProfileAsDictionary == null || !this.ProfileAsDictionary.ContainsKey(Key))
                if (!raiseExceptionIfNotExist)
                    return null;
                else
                    throw new Exception(string.Format("Key {0} not found", Key));
            return this.ProfileAsDictionary[Key];
        }


        private object GetValueFromDictionary(string Key)
        {
            return GetValueFromDictionary(Key, false);
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
                return Result.ToLocalTime();
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
            {
                if (string.IsNullOrWhiteSpace(Field.DbFieldName))
                    Field.DbFieldName = GetDbFieldCodeByFieldId(Field.Id);
                return Field;
            }
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
            if (Field.GenericValue != null)
            {
                return Field.GenericValue;
            }
            return GetValueFromDictionary(Field.DbFieldName);
        }

        public bool? GetBoolValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Boolean);
            if (Field.GenericValue != null)
            {
                bool num;
                if (bool.TryParse(Field.GenericValue.ToString(), out num))
                    return num;
            }
            return GetBoolValueFromDictionary(Field.DbFieldName);
        }

        public DateTime? GetDateTimeValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.DateTime);
            if (Field.GenericValue != null)
            {
                DateTime num;
                if (DateTime.TryParse(Field.GenericValue.ToString(), out num))
                    return num;
            }
            return GetDateTimeValueFromDictionary(Field.DbFieldName);
        }

		public Guid? GetStorageIdValue_By_ByteArrayFieldExternalId(string externalId)
        {
			var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.ByteArray);
            if (Field.GenericValue != null)
            {
                Guid num;
                if (Guid.TryParse(Field.GenericValue.ToString(), out num))
                    return num;
            }
			return GetGuidValueFromDictionary(Field.DbFieldName);
        }

        public DateTime? GetDateValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Date);
            if (Field.GenericValue != null)
            {
                DateTime num;
                if (DateTime.TryParse(Field.GenericValue.ToString(), out num))
                    return num;
            }
            var Value = GetDateTimeValueFromDictionary(Field.DbFieldName);
            if (Value.HasValue)
                return Value.Value.Date;
            else
                return null;
        }

        public decimal? GetDecimalValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Decimal);
            if (Field.GenericValue != null)
            {
                decimal num;
                if (decimal.TryParse(Field.GenericValue.ToString(), out num))
                    return num;
            }
            return GetDecimalValueFromDictionary(Field.DbFieldName);
        }

        public Guid? GetGuidValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Guid);
            if (Field.GenericValue != null)
            {
                Guid num;
                if (Guid.TryParse(Field.GenericValue.ToString(), out num))
                    return num;
            }
            return GetGuidValueFromDictionary(Field.DbFieldName);
        }

        public int? GetIntValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.Int);
            if (Field.GenericValue != null) 
            {
                int num;
                if (int.TryParse(Field.GenericValue.ToString(), out num))
                    return num;
            }
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
            if (Field == null || Language == null) throw new Exception("Field or Language not found");
            var FieldCode = string.Format("{0}_{1}", Field.DbFieldName, Language.Id);

            var ritorno = GetStringValueFromDictionary(FieldCode);
            if (!string.IsNullOrWhiteSpace(ritorno)) return ritorno;
            return GetMultilanguageFieldValueById(Field.Id, Language.Id);
        }

        public string GetMultilanguageFieldValueById(int id, int languegeId)
        {
            var Field = GetField_By_Id(id, Enums.EFieldType.MultiLanguage);
            var FieldCode = GetDbFieldCodeByFieldIdLanguageId(id, languegeId);
            if (Field.GenericValue != null)
            {
                var fieldContent =JsonConvert.DeserializeObject<List<DocsMarshal.Entities.FieldValueLang>>(Field.GenericValue.ToString());
                var fielfValue = fieldContent.FirstOrDefault(x => x.Id == languegeId);
                if (fielfValue != null && !string.IsNullOrWhiteSpace(fielfValue.Value)) return fielfValue.Value;
            }
            return GetStringValueFromDictionary(FieldCode);
        }

        public string GetStringValue_By_ExternalId(string externalId)
        {
            var Field = GetField_By_ExternalId(externalId, Enums.EFieldType.String);
            if (Field.GenericValue != null)
                return Field.GenericValue.ToString();
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