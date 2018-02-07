using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Entities
{
    class Profile : Interfaces.IProfile
    {
        public string ClassType { get; private set; }
        public int ClassTypeId { get; private set; }
        public string ClassType_ExternalId { get; private set; }
        public int DeleteStatus { get; private set; }
        public string Domain { get; private set; }
        public int DomainId { get; private set; }
        public string Domain_ExternalId { get; private set; }
        public Guid? IdRegister { get; private set; }
        public DateTime InsertDt { get; private set; }
        public string LanguageCode { get; private set; }
        public int? LanguageId { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public Guid ObjectId { get; private set; }
        public string ObjectState { get; private set; }
        public int ObjectStateId { get; private set; }
        public string ObjectState_ExternalId { get; private set; }
        public string ProtocolCode { get; private set; }
        public int? ProtocolDomainId { get; private set; }
        public DateTime? ProtocolInsertDt { get; private set; }
        public int? ProtocolNumber { get; private set; }
        public int? ProtocolYear { get; private set; }
        public int UserId { get; private set; }

        public List<Field> Fields { get; private set; }
        public Dictionary<string, object> ProfileAsDictionary { get; private set; }

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

            this.ObjectId = GetValueFromDictionary<Guid>("ObjectId");
            this.DomainId = GetValueFromDictionary<int>("DomainId");
            this.Domain_ExternalId = GetValueFromDictionary<string>("Domain_ExternalId");
            this.Domain = GetValueFromDictionary<string>("Domain");
            this.ClassTypeId = GetValueFromDictionary<int>("ClassTypeId");
            this.ClassType_ExternalId = GetValueFromDictionary<string>("ClassType_ExternalId");
            this.ClassType = GetValueFromDictionary<string>("ClassType");
            this.ObjectStateId = GetValueFromDictionary<int>("ObjectStateId");
            this.ObjectState_ExternalId = GetValueFromDictionary<string>("ObjectState_ExternalId");
            this.ObjectState = GetValueFromDictionary<string>("ObjectState");
            this.LanguageId = GetValueFromDictionary<int>("LanguageId");
            this.LanguageCode = GetValueFromDictionary<string>("LanguageCode");
            this.IdRegister = GetValueFromDictionary<Guid?>("IdRegister");
            this.ProtocolCode = GetValueFromDictionary<string>("ProtocolCode");
            this.ProtocolDomainId = GetValueFromDictionary<int?>("ProtocolDomainId");
            this.ProtocolInsertDt = GetValueFromDictionary<DateTime?>("ProtocolInsertDt");
            this.ProtocolNumber = GetValueFromDictionary<int?>("ProtocolNumber");
            this.ProtocolYear = GetValueFromDictionary<int?>("ProtocolYear");
            this.UserId = GetValueFromDictionary<int>("UserId");
            this.DeleteStatus = GetValueFromDictionary<int>("DeleteStatus");
        }

        private object GetValueFromDictionary(string Key)
        {
            if (this.ProfileAsDictionary == null)
                throw new ArgumentNullException("Profile.ProfileAsDictionary");
            if (!this.ProfileAsDictionary.ContainsKey(Key))
                throw new KeyNotFoundException(string.Format("Field key {0}", Key));
            var Value = this.ProfileAsDictionary[Key];
            return Value;
        }

        private T GetValueFromDictionary<T>(string Key)
        {
            return (T)GetValueFromDictionary(Key);
        }

        public object GetFieldValue_By_ExternalId(string ExternalId)
        {
            if (string.IsNullOrWhiteSpace(ExternalId))
                throw new ArgumentNullException("ExternalId");
            if (this.Fields == null)
                throw new ArgumentNullException("Profile.Fields");
            if (this.ProfileAsDictionary == null)
                throw new ArgumentNullException("Profile.ProfileAsDictionary");
            var Field = Fields.FirstOrDefault(x => string.Equals(ExternalId, x.ExternalId, StringComparison.CurrentCultureIgnoreCase));
            if (Field == null)
                throw new KeyNotFoundException(string.Format("Field with ExternalId \"{0}\"", ExternalId));
            var DbFieldCode = GetDbFieldCodeByFieldId(Field.Id);
            return GetValueFromDictionary(DbFieldCode);
        }

        public bool? GetBoolValue_By_ExternalId(string externalId)
        {
            return (bool?)GetFieldValue_By_ExternalId(externalId);
        }

        public DateTime? GetDateTimeValue_By_ExternalId(string externalId)
        {
            return (DateTime?)GetFieldValue_By_ExternalId(externalId);
        }

        public DateTime? GetDateValue_By_ExternalId(string externalId)
        {
            return (DateTime?)GetFieldValue_By_ExternalId(externalId);
        }

        public decimal? GetDecimalValue_By_ExternalId(string externalId)
        {
            return (decimal?)GetFieldValue_By_ExternalId(externalId);
        }

        public Guid? GetGuidValue_By_ExternalId(string externalId)
        {
            return (Guid?)GetFieldValue_By_ExternalId(externalId);
        }

        public int? GetIntValue_By_ExternalId(string externalId)
        {
            return (int?)GetFieldValue_By_ExternalId(externalId);
        }

        public string GetMultilanguageFieldValueByExternalId(string externalId, string lang)
        {
            throw new NotImplementedException("ML");
        }

        public string GetMultilanguageFieldValueById(int id, int languegeId)
        {
            throw new NotImplementedException("ML");
        }

        public string GetStringValue_By_ExternalId(string externalId)
        {
            return (string)GetFieldValue_By_ExternalId(externalId);
        }

        public static string GetDbFieldCodeByFieldId(int FieldId)
        {
            return "C" + FieldId;
        }
    }
}
