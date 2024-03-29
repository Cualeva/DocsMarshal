﻿using DocsMarshal.Connectors.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocsMarshal.Connectors.Entities
{
    public class ProfileSearchResult
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
        public List<Field> Fields { get; set; }
        public List<Dictionary<string,object>> Profiles { get; set; }
        public List<Languages> Languages { get; set; }

        public List<IProfile> GetResultAsProfiles()
        {
            var Result = new List<IProfile>();

            if (Profiles == null)
                throw new ArgumentNullException("ProfileSearchResult.Profiles");
            if (Fields == null)
                throw new ArgumentNullException("ProfileSearchResult.Fields");

            for (var i = 0; i < Profiles.Count; ++i)
                Result.Add(new Profile(this, i));

            return Result;
        }

        #region Deprecated
        public Guid? GetGuidValueFromProfileByExternalId(int index, string externalid)
        {
            if (Profiles == null || index >= Profiles.Count) throw new IndexOutOfRangeException("Index");
            var profilo = Profiles[index];
            var campo = Fields.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ExternalId) && string.Equals(x.ExternalId, externalid, StringComparison.OrdinalIgnoreCase));
            string fieldName = string.Empty;
            if (campo == null)
            {
                // verifico se esiste una proprietà con lo stesso nome
                if (profilo.ContainsKey(externalid))
                    fieldName = externalid;
                else
                    throw new KeyNotFoundException();
            }
            else
            {
                fieldName = campo.DbFieldName;
                if (campo.FieldType != Enums.EFieldType.Guid) throw new InvalidCastException(string.Format("Field {0} is not a guid field. ({1})", campo.Name, campo.FieldType.ToString()));
            }

            if (profilo.ContainsKey(fieldName))
            {
                Guid valore = Guid.Empty;
                if (profilo[fieldName] != null && Guid.TryParse(profilo[fieldName].ToString(), out valore))
                    return valore;
                else
                    return null;
            }
            else
                return null;
        }

        public int? GetIntValueFromProfileByExternalId(int index, string externalid)
        {
            if (Profiles == null || index >= Profiles.Count) throw new IndexOutOfRangeException("Index");
            var profilo = Profiles[index];
            var campo = Fields.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ExternalId) && string.Equals(x.ExternalId, externalid, StringComparison.OrdinalIgnoreCase));
            string fieldName = string.Empty;
            if (campo == null)
            {
                // verifico se esiste una proprietà con lo stesso nome
                if (profilo.ContainsKey(externalid))
                    fieldName = externalid;
                else
                    throw new KeyNotFoundException();
            }
            else
            {
                fieldName = campo.DbFieldName;
                if (campo.FieldType != Enums.EFieldType.Int) throw new InvalidCastException(string.Format("Field {0} is not a int field. ({1})", campo.Name, campo.FieldType.ToString()));
            }
            if (profilo.ContainsKey(fieldName))
            {
                int valore = 0;
                if (profilo[fieldName] != null && int.TryParse(profilo[fieldName].ToString(), out valore))
                    return valore;
                else
                    return null;
            }
            else
                return null;
        }

        public double? GetDoubleValueFromProfileByExternalId(int index, string externalid)
        {
            if (Profiles == null || index >= Profiles.Count) throw new IndexOutOfRangeException("Index");
            var profilo = Profiles[index];
            var campo = Fields.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ExternalId) && string.Equals(x.ExternalId, externalid, StringComparison.OrdinalIgnoreCase));
            string fieldName = string.Empty;
            if (campo == null)
            {
                // verifico se esiste una proprietà con lo stesso nome
                if (profilo.ContainsKey(externalid))
                    fieldName = externalid;
                else
                    throw new KeyNotFoundException();
            }
            else
            {
                fieldName = campo.DbFieldName;
                if (campo.FieldType != Enums.EFieldType.Decimal) throw new InvalidCastException(string.Format("Field {0} is not a decimal field. ({1})", campo.Name, campo.FieldType.ToString()));
            }
            if (profilo.ContainsKey(fieldName))
            {
                double valore = 0;
                if (profilo[fieldName] != null && double.TryParse(profilo[fieldName].ToString(), out valore))
                    return valore;
                else
                    return null;
            }
            else
                return null;
        }

        public DateTime? GetDateTimeValueFromProfileByExternalId(int index, string externalid)
        {
            if (Profiles == null || index >= Profiles.Count) throw new IndexOutOfRangeException("Index");
            var profilo = Profiles[index];
            var campo = Fields.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ExternalId) && string.Equals(x.ExternalId, externalid, StringComparison.OrdinalIgnoreCase));
            string fieldName = string.Empty;
            if (campo == null)
            {
                // verifico se esiste una proprietà con lo stesso nome
                if (profilo.ContainsKey(externalid))
                    fieldName = externalid;
                else
                    throw new KeyNotFoundException();
            }
            else
            {
                fieldName = campo.DbFieldName;
                if (campo.FieldType != Enums.EFieldType.DateTime && campo.FieldType != Enums.EFieldType.Date) throw new InvalidCastException(string.Format("Field {0} is not a dateTime or date field. ({1})", campo.Name, campo.FieldType.ToString()));
            }

            if (profilo.ContainsKey(fieldName))
            {
                DateTime valore = DateTime.MinValue;
                if (profilo[fieldName] != null && DateTime.TryParse(profilo[fieldName].ToString(), out valore))
                    return valore;
                else
                    return null;
            }
            else
                return null;
        }



        public string GetStringValueFromMultilanguageFieldProfileByExternalId(int index, string externalid, string languageCode)
        {
            if (Profiles == null || index >= Profiles.Count) throw new IndexOutOfRangeException("Index");
            var profilo = Profiles[index];
            var campo = Fields.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ExternalId) && string.Equals(x.ExternalId, externalid, StringComparison.OrdinalIgnoreCase));
            if (campo == null) throw new KeyNotFoundException();
            if (campo.FieldType != Enums.EFieldType.MultiLanguage) throw new InvalidCastException(string.Format("Field {0} is not a multilanguage field. ({1})", campo.Name, campo.FieldType.ToString()));
            // recupero il nome della key multilanguage
            var lingua = Languages.FirstOrDefault(x => string.Equals(x.Code, languageCode, StringComparison.OrdinalIgnoreCase));
            if (lingua == null) throw new KeyNotFoundException("Language not found");
            string DbFieldName = string.Format("C{0}_{1}", campo.Id, lingua.Id);
            if (profilo.ContainsKey(DbFieldName))
                if (profilo[DbFieldName] != null)
                    return profilo[DbFieldName].ToString();
            return null;
        }

        public string GetStringValueFromProfileByExternalId(int index, string externalid)
        {
            var campo = Fields.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ExternalId) && string.Equals(x.ExternalId, externalid, StringComparison.OrdinalIgnoreCase));
            var profilo = Profiles[index];
            string fieldName = string.Empty;
            if (campo == null)
            {
                // verifico se esiste una proprietà con lo stesso nome
                if (profilo.ContainsKey(externalid))
                    fieldName = externalid;
                else
                    throw new KeyNotFoundException();
            }
            else
            {
                fieldName = campo.DbFieldName;
                if (campo.FieldType != Enums.EFieldType.String) throw new InvalidCastException(string.Format("Field {0} is not a string field. ({1})", campo.Name, campo.FieldType.ToString()));
            }
            if (profilo.ContainsKey(fieldName))
                return (profilo[fieldName] == null) ? null : profilo[fieldName].ToString();
            else
                return string.Empty;
        }

        public bool GetBooleanValueFromProfileByExternalIdManabeNullValue(int index, string externalid, bool valueIfNullValue)
        {
            var valore = GetBooleanValueFromProfileByExternalId(index, externalid);
            if (valore == null)
                return valueIfNullValue;
            else
                return valore.Value;
        }


        public bool? GetBooleanValueFromProfileByExternalId(int index, string externalid)
        {
            var campo = Fields.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.ExternalId) && string.Equals(x.ExternalId, externalid, StringComparison.OrdinalIgnoreCase));
            if (campo == null) throw new KeyNotFoundException();
            if (campo.FieldType != Enums.EFieldType.Boolean) throw new InvalidCastException(string.Format("Field {0} is not a boolan field. ({1})", campo.Name, campo.FieldType.ToString()));
            var profilo = Profiles[index];
            if (profilo.ContainsKey(campo.DbFieldName))
            {
                if (profilo[campo.DbFieldName] != null)
                    return Convert.ToBoolean(profilo[campo.DbFieldName]);
                else
                    return null;
            }
            else
                return null;
        }
        #endregion
    }
}