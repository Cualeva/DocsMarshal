using System;
namespace DocsMarshal.Connectors.Entities
{
    public class GetProfileDocument
    {
        public string   sessionId         {   get;set;    }
        public Guid     objectid          {   get;set;    }
        public int      fieldId           {   get;set;    }
        public string   fieldExternalId   {   get;set;    }
    }
}
