using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.MVVM.Models
{
    public class QueuedRemoteOperation
    {
        [PrimaryKey, AutoIncrement]
        public long Sequence { get; set; }
        public DateTime InsertDt { get; set; }
        public int Type { get; set; }
        public string SerializedPayload { get; set; }

        public enum ERemoteOperationType
        {
            ProfileInsert,
            ProfileUpdate,
            ProfileDelete,
            TaskVariableSetValue,
            TaskClose
        }
    }
}
