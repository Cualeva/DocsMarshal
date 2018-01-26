using System;
namespace DocsMarshal.Entities
{
    public class SearchGuidParameter: SearchParameter
    {
        public new Enums.ESearchStringCondition Condition { get; set; }
        public Guid? Value { get; set; }
        public Guid? Value2 { get; set; }
        public SearchGuidParameter()
        {
        }
    }
}
