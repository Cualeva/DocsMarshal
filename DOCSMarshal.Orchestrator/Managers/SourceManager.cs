using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Entities;
using Newtonsoft.Json;

namespace DocsMarshal.Orchestrator.Managers
{
    public class SourceManager : Interfaces.Managers.Sources.ISource
    {
        private Manager Orchestrator;
        public SourceManager(DocsMarshal.Orchestrator.Manager orchestrator)
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
            using (var client = new HttpClient())
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(externalId))
                        throw new ArgumentNullException(nameof(externalId));
                    string url = string.Format("{0}/Sources/GetDataBySourceExternalId", Orchestrator.DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new
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
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = await response.Content.ReadAsStringAsync();
                    var ritO = JsonConvert.DeserializeObject<SourceDataResult>(rit);
                    if (ritO.Error)
                        throw new Exception(ritO.ErrorDescription);
                    return ritO;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
