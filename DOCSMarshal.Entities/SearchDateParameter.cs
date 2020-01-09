using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Entities
{
    public class SearchDateParameter : SearchParameter
    {
        public Enums.ESearchDateCondition Condition { get; set; }
        public DateTime? Value { get; set; }
        public DateTime? Value2 { get; set; }
        public List<DateTime> Values { get; set; }
    }
}
