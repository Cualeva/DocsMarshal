using System;
using System.Collections.Generic;

namespace DocsMarshal.Entities
{
    public class ProfileSearch
    {
        public string SearchUrl(string docsMarshalBaseUrl)
        {
            return string.Format("{0}/DMSearch/Execute", docsMarshalBaseUrl);
        }

        public string sessionID { get; set; }
        public string domainExternalId { get; set; }
        public string classTypeExternalId { get; set; }
        public string objectStatusExternalId { get; set; }
        public List<string> fieldsToSelect { get; set; }
        public List<SearchParameter> parameters { get; set; }
        public ProfileSearch()
        {
            fieldsToSelect = new List<string>();
            parameters = new List<SearchParameter>();
        }

    }
}
