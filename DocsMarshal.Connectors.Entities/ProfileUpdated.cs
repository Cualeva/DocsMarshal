using System;
namespace DocsMarshal.Connectors.Entities
{
    public class ProfileUpdated
    {
        public ProfileUpdated()
        {
        }

        public ProfileUpdated(DocsMarshal.Connectors.Entities.Profile profile):base()
        {
            Profile = profile;
        }

        public bool HasError { get; set; }
        public string Error { get; set; }
        public DocsMarshal.Connectors.Entities.Profile Profile { get; set; }
    }
}
