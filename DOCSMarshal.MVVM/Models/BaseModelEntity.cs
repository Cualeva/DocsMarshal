using System;
using DocsMarshal.Entities;
using SQLite;

namespace DOCSMarshal.MVVM.Models
{
    public class BaseModelEntity
    {
        public BaseModelEntity()
        {
        }

        [PrimaryKey]
        public Guid Objectid { get; set; }
        public int DomainId { get; set; }
        public int ObjectStateId { get; set; }
        public int ClassTypeId { get; set; }
        public string DomainExternalId { get; set; }
        public string ClassTypeExternalId { get; set; }
        public string ObjectStateExternalId { get; set; }
        public DateTime InsertDt { get; set; }
        public DateTime LastUpdate { get; set; }


        internal void LoadStandardFieldFromProfileSearchResult(DocsMarshal.Entities.Interfaces.IProfile profile)
        {
            this.Objectid = profile.ObjectId;
            this.DomainId = profile.DomainId;
            this.DomainExternalId = profile.Domain_ExternalId;
            this.ClassTypeId = profile.ClassTypeId;
            this.ObjectStateId = profile.ObjectStateId;
            this.ObjectStateExternalId = profile.ObjectState_ExternalId;
            this.ClassTypeExternalId = profile.ClassType_ExternalId;
            this.LastUpdate = profile.LastUpdate;
            this.InsertDt = profile.InsertDt;
        }


        internal void LoadStandardFieldFromProfileSearchResult(int i, ProfileSearchResult risultato)
        {
            Objectid = risultato.GetGuidValueFromProfileByExternalId(i, "ObjectId").Value;
            DomainId = risultato.GetIntValueFromProfileByExternalId(i, "DomainId").Value;
            ClassTypeId = risultato.GetIntValueFromProfileByExternalId(i, "ClassTypeId").Value;
            ObjectStateId = risultato.GetIntValueFromProfileByExternalId(i, "ObjectStateId").Value;
            ClassTypeExternalId = risultato.GetStringValueFromProfileByExternalId(i, "ClassType_ExternalId");
            LastUpdate = risultato.GetDateTimeValueFromProfileByExternalId(i, "LastUpdate").Value;
            InsertDt = risultato.GetDateTimeValueFromProfileByExternalId(i, "InsertDt").Value;
            DomainExternalId = risultato.GetStringValueFromProfileByExternalId(i, "Domain_ExternalId");
            ObjectStateExternalId = risultato.GetStringValueFromProfileByExternalId(i, "ObjectState_ExternalId");
        }
    }
}
