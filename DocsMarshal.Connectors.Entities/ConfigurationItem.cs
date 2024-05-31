using System;
using System.Collections.Generic;

namespace DocsMarshal.Connectors.Entities
{
    public class ConfigurationItem
    {
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public object Value { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Guid Id { get; set; }
        public bool ValueIsNullable { get; set; }
        public List<ConfigurationItem> ConfigurationItemList { get; set; }
        public List<ConfigurationItemComboElement> ItemSource { get; set; }

        public ConfigurationItem()
        {
        }
    }
    public class ConfigurationItemComboElement
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}