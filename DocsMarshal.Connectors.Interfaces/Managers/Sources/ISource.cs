﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Sources
{
    public interface ISource : IDisposable
    {
        Task<Entities.SourceDataResult> GetDataResultByExternalId(string externalId, string domainExternalId, string classtypeExternalId, string objectStateExternalId, int? languageId, List<Entities.FieldValue> fields, List<DocsMarshal.Connectors.Entities.SearchParameter> filters);
    }
}
