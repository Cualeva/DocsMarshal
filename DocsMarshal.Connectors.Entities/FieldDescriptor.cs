using System.Collections.Generic;

namespace DocsMarshal.Connectors.Entities
{
    public class FieldDescriptor
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public string PresenterType { get; set; }
        public bool AllowNull { get; set; }
        public int? StringSize { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        public bool VisibleInSearchView { get; set; }
        public string VisibilityFilter { get; set; }
        public string Formula { get; set; }
        public bool? ApplyFormulaOnlyIfNull { get; set; }
        public List<ConfigurationItem> PresenterTypeProperties { get; set; }
        public List<ConfigurationItem> LanguagesLabels { get; set; }
        public bool? NetworkCredentialsForValue { get; set; } //If true, the value is protected by an external entity, like windows grants, and field value requires network credentials to be used
        public string StoragePrefix { get; set; }
        public string StorageProviderId { get; set; }

        public FieldDescriptor()
        {

        }
    }

    public class FieldDescriptorInClass : FieldDescriptor
    {
        public bool IsDefaultFileField { get; set; }
        public int ClassTypeId { get; set; }
    }
}