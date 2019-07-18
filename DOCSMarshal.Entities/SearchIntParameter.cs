using System;
namespace DocsMarshal.Entities
{
    public class SearchIntParameter: SearchParameter
    {
        public new Enums.ESearchNumericCondition Condition { get; set; }
        public int? Value { get; set; }
        public int? Value2 { get; set; }
        public SearchIntParameter()
        {
        }
    }
}
