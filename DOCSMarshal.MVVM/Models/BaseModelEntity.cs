using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DocsMarshal.Entities;
using SQLite;

namespace DocsMarshal.MVVM.Models
{
    public class BaseModelEntity:INotifyPropertyChanged
    {
        public BaseModelEntity()
        {
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

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


        public void LoadStandardFieldFromProfileSearchResult(DocsMarshal.Entities.Interfaces.IProfile profile)
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
