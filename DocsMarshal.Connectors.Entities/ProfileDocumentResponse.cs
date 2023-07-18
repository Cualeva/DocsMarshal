using System;
namespace DocsMarshal.Connectors.Entities
{
    public class ProfileDocumentResponse
    {
        public bool HasError                { get; set; }
        public string Error                 { get; set; }
        public ProfileDocument Document     { get; set; }
    }
}
