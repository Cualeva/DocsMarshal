using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Entities
{
    public class FileValue
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
