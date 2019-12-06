using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities
{
    public class SearchIntParameter: SearchParameter
    {
        public new Enums.ESearchNumericCondition Condition { get; set; }
        public int? Value { get; set; }
        public int? Value2 { get; set; }
        public List<int> Values { get; set; }
        public SearchIntParameter()
        {
        }
    }
}
