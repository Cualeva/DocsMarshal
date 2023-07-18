using System;
namespace DocsMarshal.Connectors.Entities
{
    public class ProfileDeleted
    {
        public ProfileDeleted()
        {
        }

        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
