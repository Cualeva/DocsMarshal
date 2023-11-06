using System;
namespace DocsMarshal.Connectors.Entities
{
    public abstract class SearchParameter
    {
        public string FieldExternalId { get; set; }
        public string DynAssExternalIdsPath { get; set; }
    }
}
