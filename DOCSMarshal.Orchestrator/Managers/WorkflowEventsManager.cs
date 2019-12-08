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
    public class WorkflowEventsManager : DocsMarshal.Interfaces.Managers.Workflow.IWorkflowEvents
    {
        private DocsMarshal.Orchestrator.Manager Orchestrator;

        public WorkflowEventsManager(DocsMarshal.Orchestrator.Manager orchestrator)
        {
            Orchestrator = orchestrator;
        }

        public void Dispose()
        {
            if (Orchestrator != null)
                Orchestrator = null;
        }

        public async Task<Guid> StartManualEventByExternalId(string workflowEventExternalId, List<Parameter> parameters)
        {
            return await StartManualEventInternal(workflowEventExternalId, null, null, parameters);
        }

        public async Task<Guid> StartManualEventByExternalId(string workflowEventExternalId, Guid objectId, List<Parameter> parameters)
        {
            if (objectId == Guid.Empty)
                throw new ArgumentNullException(nameof(objectId));
            return await StartManualEventInternal(workflowEventExternalId, objectId, null, parameters);
        }

        public async Task<Guid> StartManualEventByExternalId(string workflowEventExternalId, List<Guid> objectIds, List<Parameter> parameters)
        {
            if (objectIds == null || objectIds.Count == 0)
                throw new ArgumentNullException(nameof(objectIds));
            return await StartManualEventInternal(workflowEventExternalId, null, objectIds, parameters);
        }

        private async Task<Guid> StartManualEventInternal(string workflowEventExternalId, Guid? objectId, List<Guid> objectIds, List<Parameter> parameters)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(workflowEventExternalId))
                        throw new ArgumentNullException(nameof(workflowEventExternalId));
                    string url = string.Format("{0}/Workflow/StartManualEvent", Orchestrator.DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new
                    {
                        SessionID = Orchestrator.SessionId,
                        ExternalIdWorkflowEvent = workflowEventExternalId,
                        ObjectId = objectId,
                        ObjectIds = objectIds,
                        Parameters = parameters
                    });
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = await response.Content.ReadAsStringAsync();
                    var ritO = JsonConvert.DeserializeObject<BaseJsonResult<Guid>>(rit);
                    if (ritO.Error)
                        throw new Exception(ritO.ErrorDescription);
                    return ritO.Data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
