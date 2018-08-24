using System;
namespace DocsMarshal.Entities
{
    public class FieldValue
    {
        public FieldValue()
        {
            
        }
        public string ExternalID { get; set; }
        public string Value { get; set; }
        public Enums.EFieldType ValueType { get; set; }
        public string ValueFormat { get; set; }
        public string ValueCultureInfoName { get; set; }
    }


    public class FieldValueLang
    {
        public string Code{get;set;}
        public string Name { get; set; }
        public int Id { get; set; }   
        public string Value { get; set; }   
    }

    public class FieldValueInt: FieldValue
    {
        public FieldValueInt(string externalId, int? value, string valueCultureInfoName)
        {
            ValueType = Enums.EFieldType.Int;
            ExternalID = externalId;
            if (value.HasValue)
                Value = value.Value.ToString();
            else
                Value = string.Empty;
            ValueCultureInfoName = valueCultureInfoName;
        }
    }

    public class FieldValueString : FieldValue
    {
        public FieldValueString(string externalId, string value)
        {
            ValueType = Enums.EFieldType.String;
            ExternalID = externalId;
            Value = value;
        }
    }

    public class FieldValueDecimal : FieldValue
    {
        public FieldValueDecimal(string externalId, decimal? value, string valueCultureInfoName)
        {
            ValueType = Enums.EFieldType.Decimal;
            ExternalID = externalId;
            if (value.HasValue)
                Value = value.Value.ToString();
            else
                Value = string.Empty;
            ValueCultureInfoName = valueCultureInfoName;
        }
    }

    public class FieldValueDate : FieldValue
    {
        public FieldValueDate(string externalId, DateTime? value, string valueFormat)
        {
            ValueType = Enums.EFieldType.Date;
            ExternalID = externalId;
            if (value.HasValue)
                Value = value.Value.ToString();
            else
                Value = string.Empty;
            ValueFormat = valueFormat;
        }
    }

    public class FieldValueDateTime : FieldValue
    {
        public FieldValueDateTime(string externalId, DateTime? value, string valueFormat)
        {
            ValueType = Enums.EFieldType.DateTime;
            ExternalID = externalId;
            if (value.HasValue)
                Value = value.Value.ToString();
            else
                Value = string.Empty;
            ValueFormat = valueFormat;
        }
    }

    public class FieldValueByteArray : FieldValue
    {
        public FieldValueByteArray(string externalId, byte[] value)
        {
            ValueType = Enums.EFieldType.ByteArray;
            ExternalID = externalId;
            if (value == null || value.Length == 0)
                Value = string.Empty;
            else
                Value = System.Convert.ToBase64String(value);
            
        }
    }

    public class FieldValueBoolean : FieldValue
    {
        public FieldValueBoolean(string externalId, Boolean? value)
        {
            ValueType = Enums.EFieldType.Boolean;
            ExternalID = externalId;
            if (!value.HasValue)
                Value = string.Empty;
            else
                Value = value.ToString();

        }
    }

    public class FieldValueGuid : FieldValue
    {
        public FieldValueGuid(string externalId, Guid? value)
        {
            ValueType = Enums.EFieldType.Guid;
            ExternalID = externalId;
            if (!value.HasValue)
                Value = string.Empty;
            else
                Value = value.ToString();

        }
    }

   
}
