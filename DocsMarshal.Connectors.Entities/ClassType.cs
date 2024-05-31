using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Entities
{
    public class ClassType
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }
        public string BackGroundColor { get; set; }
        public bool? CheckAddOrExclPermissions { get; set; }
        public string CustomUrlArchiveProfile { get; set; }
        public string CustomUrlOpenProfile { get; set; }
        public int? DefaultFieldForFile { get; set; }
        public string DefaultGridSettings { get; set; }
        public int? DefaultStateId { get; set; }
        public int? ExpirationDateFieldId { get; set; }
        public Guid? IdRegister { get; set; }
        public bool LogWhenDelete { get; set; }
        public bool LogWhenInsert { get; set; }
        public bool LogWhenInsertDocument { get; set; }
        public bool LogWhenIsRelatedOfAnEmail { get; set; }
        public bool LogWhenIsRelatedOfAnWorkflow { get; set; }
        public bool LogWhenProfileRequested { get; set; }
        public bool LogWhenUpdate { get; set; }
        public bool LogWhenUpdateDocument { get; set; }
        public int? ObjectStateIdExpiratedProfile { get; set; }
        public bool? ProfileWithLanguage { get; set; }
        public string Tags { get; set; }
        public bool? VisibleInArchive { get; set; }
        public bool? VisibleInSearch { get; set; }

        public ClassType()
        {

        }
    }
}