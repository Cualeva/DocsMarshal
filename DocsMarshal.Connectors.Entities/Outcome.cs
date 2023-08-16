using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Entities
{
    public class Outcome
    {
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public Guid IdTask { get; set; }
        public Guid IdOutcome { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public string DynamicRequiredVariables { get; set; }
        public Guid IdProcess { get; set; }
        public bool IgnoreRequiredOperations { get; set; }
        public bool IgnoreRequiredVariables { get; set; }
        public int? Priority { get; set; }
        public bool? ProcessNoteMandatory { get; set; }
        public bool? RequestConfirm { get; set; }
        public string RequestConfirmText { get; set; }
        public bool? RequestProcessNote { get; set; }
        public string Value { get; set; }
        public string VisibilityGrants { get; set; }
    }
}
