using System;
namespace DocsMarshal.Connectors.Entities
{
    public class DmTask
    {
       
        public string CustomUrl { get; set; }        public Guid IdTask { get; set; }        public Guid IdProcess { get; set; }        public string ProcessName { get; set; }        public DateTime ProcessStart { get; set; }        public DateTime InsertDt { get; set; }        public string TaskName { get; set; }        public string TaskDescription { get; set; }        public bool KeepInCharge { get; set; }        public DateTime? KeepInChargedDt { get; set; }        public int? KeepInChargeIdentityId { get; set; }        public int? IndentityId { get; set; }        public string ExternalId { get; set; }        public int Priority { get; set; }        public Guid IdWorkflow { get; set; }        public string WorkflowName { get; set; }        public string IdentityName { get; set; }        public string KeepInChargeIdentityName { get; set; }        public DateTime? EndDt { get; set; }        public DateTime? WFServiceGetDt { get; set; }        public Guid? WFServiceInstanceId { get; set; }        public Enums.ETaskState TaskState { get; set; }        public Guid? IdRuntimeWorkflow { get; set; }        public string ErrorMessage { get; set; }        public int? DomainId { get; set; }        public string Domain { get; set; }        public int? LanguageId { get; set; }        public string Language { get; set; }        public string OutcomeName { get; set; }        public string OutcomeDescription { get; set; }        public string OutcomeValue { get; set; }        public string Notes { get; set; }        public DateTime? DeadLine { get; set; }        public string Tags { get; set; }
    }
}
