using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Entities
{
    public class SourceDataResult : BaseJsonResult
    {
        public List<SourceColumnModel> Columns { get; set; }
        public List<Dictionary<string, object>> Rows { get; set; }
        public List<int> DependsOnFieldIds { get; set; }
    }

    public class SourceColumnModel
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set; }
        public Guid SourceFieldId { get; set; }
    }
}
