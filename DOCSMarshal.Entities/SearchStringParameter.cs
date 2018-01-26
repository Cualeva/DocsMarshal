using System;
namespace DocsMarshal.Entities
{
    public class SearchStringParameter: SearchParameter
    {
        public new Enums.ESearchStringCondition Condition { get; set; }
        public string Value { get; set; }
        public SearchStringParameter()
        {
        }
    }
}
