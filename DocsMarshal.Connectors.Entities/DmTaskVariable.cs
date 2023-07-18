using System;
namespace DocsMarshal.Connectors.Entities
{
    public class DmTaskVariable
    {
        public string IdVariable { get; set; }        public bool? AllowNull { get; set; }        public string Description { get; set; }        public string ExternalId { get; set; }        public Enums.EFieldType FieldType { get; set; }        public DocsMarshal.Connectors.Entities.Enums.EPresenterType PresenterType { get; set; }        public string Name { get; set; }        public int? Precision { get; set; }        public int? Scale { get; set; }        public int? StringSize { get; set; }        public string PresenterSerializedProperties { get; set; }        public object DefaultValue { get; set; }        public object Value { get; set; }        public string ActivityInstanceId { get; set; }
    }
}
