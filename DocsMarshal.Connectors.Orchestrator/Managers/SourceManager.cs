using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Connectors.Entities;
using Newtonsoft.Json;

namespace DocsMarshal.Connectors.Orchestrator.Managers
{
    public class SourceManager : Interfaces.Managers.Sources.ISource
    {
        private Manager Orchestrator;
        public SourceManager(DocsMarshal.Connectors.Orchestrator.Manager orchestrator)
        {
            Orchestrator = orchestrator;
        }

        public void Dispose()
        {
            if (Orchestrator != null)
                Orchestrator = null;
        }

        public async Task<SourceDataResult> GetDataResultByExternalId(string externalId, string domainExternalId, string classtypeExternalId, string objectStateExternalId, int? languageId, List<FieldValue> fields, List<SearchParameter> filters)
        {
            if (String.IsNullOrWhiteSpace(externalId))
                throw new ArgumentNullException(nameof(externalId));
            var rit = await Orchestrator.PostAsync<SourceDataResult>("/Sources/GetDataBySourceExternalId", new
            {
                SessionID = Orchestrator.SessionId,
                LoadDependencies = false,
                SourceExternalId = externalId,
                DomainExternalId = domainExternalId,
                LanguageId = languageId,
                ClassTypeExternalId = classtypeExternalId,
                ObjectStateExternalId = objectStateExternalId,
                Where = filters,
                Fields = fields
            });
            if (rit.Error)
                throw new Exception(rit.ErrorDescription);
            return rit;
        }
    }
}
