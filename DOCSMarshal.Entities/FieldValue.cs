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
        public Enums.EFieldType FieldType { get; set; }
        public string ValueFormat { get; set; }
        public string ValueCultureInfoName { get; set; }
    }
}
