using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Entities.Interfaces
{
    public interface IProfile
    {
        Guid ObjectId { get; }
        DateTime LastUpdate { get; }
        DateTime InsertDt { get; }

        int ObjectStateId { get; }
        string ObjectState { get; }
        string ObjectState_ExternalId { get; }

        int DomainId { get; }
        string Domain { get; }
        string Domain_ExternalId { get; }

        int ClassTypeId { get; }
        string ClassType { get; }
        string ClassType_ExternalId { get; }

        int? LanguageId { get; }
        string LanguageCode { get; }

        int UserId { get; }
        int DeleteStatus { get; }

        #region CampiProtocollo
        Guid? IdRegister { get;  }
        string ProtocolCode { get; }
        int? ProtocolYear { get; }
        int? ProtocolNumber { get; }
        DateTime? ProtocolInsertDt { get; }
        int? ProtocolDomainId { get; }
        #endregion

        object GetFieldValue_By_ExternalId(string ExternalId);
        string GetStringValue_By_ExternalId(string externalId);
        Guid? GetGuidValue_By_ExternalId(string externalId);
        Decimal? GetDecimalValue_By_ExternalId(string externalId);
        Double? GetDoubleValue_By_ExternalId(string externalId);
        int? GetIntValue_By_ExternalId(string externalId);
        bool? GetBoolValue_By_ExternalId(string externalId);
        DateTime? GetDateTimeValue_By_ExternalId(string externalId);
        DateTime? GetDateValue_By_ExternalId(string externalId);
        string GetMultilanguageFieldValueByExternalId(string externalId, string lang);
        string GetMultilanguageFieldValueById(int id, int languegeId);

        Dictionary<string, string> GetMultilanguageFieldValuesByExternalId(string externalId);
        Dictionary<int, string> GetMultilanguageFieldValuesById(int id);

        //List<KeyValuePair<string, string>> ToKeyValuePairs();
        //List<KeyValuePair<string, string>> ToKeyValuePairs(int? languageId);
        //List<KeyValuePair<string, string>> ToKeyValuePairs(int? languageId, bool useExternalIdIfExist);
        Dictionary<string, object> ToDictionary();
        //Dictionary<string, object> ToDictionary(bool useExternalIdIfExist);
        //Dictionary<string, object> ToDictionary(int? languageId);
        //Dictionary<string, object> ToDictionary(int? languageId, bool useExternalIdIfExist);
    }
}
