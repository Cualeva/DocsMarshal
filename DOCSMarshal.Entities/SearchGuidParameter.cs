using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities
{
    public class SearchGuidParameter: SearchParameter
    {
        public new Enums.ESearchStringCondition Condition { get; set; }
        public Guid? Value { get; set; }
        public Guid? Value2 { get; set; }
        public List<Guid> Values { get; set; }
        public SearchGuidParameter()
        {
        }
    }
}
