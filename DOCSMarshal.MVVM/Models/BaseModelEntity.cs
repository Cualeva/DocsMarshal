using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DocsMarshal.Entities;
using SQLite;
using System.Reflection;
using System.Linq;

namespace DocsMarshal.MVVM.Models
{
    public class BaseModelEntity:INotifyPropertyChanged
    {
       
        public BaseModelEntity()
        {

        }

        public BaseModelEntity(BaseModelEntity fromOtherEntity)
        {
            this.Objectid = fromOtherEntity.Objectid;
            this.DomainId = fromOtherEntity.DomainId;
            this.DomainExternalId = fromOtherEntity.DomainExternalId;
            this.ClassTypeId = fromOtherEntity.ClassTypeId;
            this.ObjectStateId = fromOtherEntity.ObjectStateId;
            this.ObjectStateExternalId = fromOtherEntity.ObjectStateExternalId;
            this.ClassTypeExternalId = fromOtherEntity.ClassTypeExternalId;
            this.LastUpdate = fromOtherEntity.LastUpdate;
            this.InsertDt = fromOtherEntity.InsertDt;
            this.LanguageCode = fromOtherEntity.LanguageCode;
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
        public string LanguageCode { get; set; }
        public string ProtocolCode { get; set; }

        // the profile only exists in the local DB and has yet to be saved by an OfflineManager or some other mean
        public bool _IsLocal { get; set; }

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
            this.LanguageCode = profile.LanguageCode;
            this.ProtocolCode = profile.ProtocolCode;
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

        internal void AddStdFieldToIProfileFor(DocsMarshal.Entities.Interfaces.IProfileFor profileFor, bool raiseWorkflowEvents)
        {
            profileFor.ClassTypeExternalID = ClassTypeExternalId;
            profileFor.DomainExternalID = DomainExternalId;
            profileFor.ObjectStateExternalID = ObjectStateExternalId;
            profileFor.LanguageCode = LanguageCode;
            profileFor.RaiseWorkflowEvents = raiseWorkflowEvents;
        }

        public DocsMarshal.Entities.ProfileForInsert ToProfileForInsert(bool raiseWorkflowEvents)
        {
            var ritorno = new DocsMarshal.Entities.ProfileForInsert();
            ritorno.ClassTypeExternalID = ClassTypeExternalId;
            AddStdFieldToIProfileFor(ritorno, raiseWorkflowEvents);
            return ritorno;
        }

        public DocsMarshal.Entities.ProfileForUpdate ToProfileForUpdate(bool raiseWorkflowEvents)
        {
            var ritorno = new DocsMarshal.Entities.ProfileForUpdate();
            ritorno.ObjectId = Objectid;
            AddStdFieldToIProfileFor(ritorno, raiseWorkflowEvents);
            return ritorno;
        }

        public static T Clone<T>(T source) where T : BaseModelEntity, new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var obj = new T();
            foreach (var prop in typeof(T).GetRuntimeProperties())
                if (prop.CanRead && prop.CanWrite)
                    prop.SetValue(obj, prop.GetValue(source));
            return obj;
        }
    }
}
