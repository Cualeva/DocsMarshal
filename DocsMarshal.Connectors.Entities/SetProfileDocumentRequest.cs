using System;
namespace DocsMarshal.Connectors.Entities
{
    public class SetProfileDocumentRequest
    {
        public Guid? SessionId { get; set; }
        public Guid ObjectId { get; set; }
        public int? FieldId { get; set; }
        public string FieldExternalId { get; set; }
        public string FileName { get; set; }
        public string FileContentBase64 { get; set; }
        public bool? RaiseWorkflowEvents { get; set; }
    }
}
