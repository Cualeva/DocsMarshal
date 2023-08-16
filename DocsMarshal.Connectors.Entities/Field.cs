using System;
namespace DocsMarshal.Connectors.Entities
{
    public class Field
    {
        public Field()
        {
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string ExternalId { get; set; }
        public string DbFieldName { get; set; }
        public Enums.EFieldType FieldType { get; set; }
        public object GenericValue { get; set; }
        public int? MaxSize { get; set; }
        public bool AllowNull { get; set; }
    }
}
