using System;
namespace DocsMarshal.Entities
{
    public abstract class SearchParameter
    {
        public string FieldExternalId { get; set; }
        public virtual string Condition { get; set; }
        public SearchParameter()
        {
        }
    }
}
