using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.Connectors.Interfaces.Managers.Workflow
{
    public interface IWorkflowEvents : IDisposable
    {
        Task<Guid> StartManualEventByExternalId(string workflowEventExternalId, List<DocsMarshal.Connectors.Entities.Parameter> parameters);
        Task<Guid> StartManualEventByExternalId(string workflowEventExternalId, Guid objectId, List<DocsMarshal.Connectors.Entities.Parameter> parameters);
        Task<Guid> StartManualEventByExternalId(string workflowEventExternalId, List<Guid> objectIds, List<DocsMarshal.Connectors.Entities.Parameter> parameters);
    }
}
