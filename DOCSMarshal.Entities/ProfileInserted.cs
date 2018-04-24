using System;
namespace DocsMarshal.Entities
{
    public class ProfileInserted
    {
        private ProfileInserted()
        {
        }

        public ProfileInserted(DocsMarshal.Entities.Profile profile):base()
        {
            Profile = profile;
        }

        public bool HasError { get; set; }
        public string Error { get; set; }
        public DocsMarshal.Entities.Profile Profile { get; set; }
    }
}
