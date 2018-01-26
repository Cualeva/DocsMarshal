using System;
namespace DocsMarshal.Entities
{
    public class GetProfileDocument
    {
        public string GetProfileDocumentUrl(string baseDocsMarshalPortalUrl)
        {
            return string.Format("{0}/DMDocuments/GetProfileDocumentByObjectIdFieldExternalId", baseDocsMarshalPortalUrl);
        }

        public string   sessionId         {   get;set;    }
        public Guid     objectid          {   get;set;    }
        public int      fieldId           {   get;set;    }
        public string   fieldExternalId   {   get;set;    }
    }
}
