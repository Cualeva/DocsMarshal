using System;
namespace DocsMarshal.Entities
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
        public Enums.EFieldType FieldType { get; set; }
        public string DbFieldName { get; set; }
        public int LanguageId { get; set; }
    }
}
