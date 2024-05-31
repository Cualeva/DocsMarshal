using System.Runtime.Serialization;

namespace DocsMarshal.Connectors.Entities.Enums
{
    [DataContract]
    public enum EExceptionReason
    {
        [EnumMember]
        Generic = 0,
        [EnumMember]
        DomainNotFound = 1,
        [EnumMember]
        ClassTypeNotFound = 2,
        [EnumMember]
        ObjectStateNotFound = 3,
        [EnumMember]
        CanNotInsertInDomain = 4,
        [EnumMember]
        CanNotInsertInClassType = 5,
        [EnumMember]
        CanNotInsertWithObjectState = 6,
        [EnumMember]
        CanNotInsertInDomainClassType = 7,
        [EnumMember]
        CanNotInsertInClassTypeObjectState = 8,
        [EnumMember]
        CanNotInsertInDomainClassTypeObjectState = 9,
        [EnumMember]
        GenericInsertError = 10,
        [EnumMember]
        LogonException = 11,
        [EnumMember]
        ObjectNotFound = 12,
        [EnumMember]
        CanNotDeleteInDomainClassTypeObjectState = 13,
        [EnumMember]
        CanNotUpdateInDomainClassTypeObjectState = 14,
        [EnumMember]
        CanNotUpdateDocumentInDomainClassTypeObjectState = 15,
        [EnumMember]
        LanguageIsNull = 16,
        [EnumMember]
        CanNotDeleteProfileContainsProtocolCode = 17,
        [EnumMember]
        DatabaseVersionIsTooOld = 18,
        [EnumMember]
        CanNotShareInDomainClassTypeObjectState = 19,
        [EnumMember]
        CanNotUpdateProfileInObjectState = 20,
        [EnumMember]
        CanNotDeleteProfileInObjectState = 21,
        [EnumMember]
        CanNotDeleteProfileInProfileGrant = 22,
        [EnumMember]
        CanNotUpdateProfileInProfileGrant = 23,
        [EnumMember]
        CanNotShareProfileInProfileGrant = 24,
    }
}
