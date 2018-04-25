using System;
namespace DocsMarshal.Entities
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
