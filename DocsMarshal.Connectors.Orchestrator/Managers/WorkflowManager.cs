using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Connectors.Entities;
using DocsMarshal.Connectors.Interfaces.Managers.Profile;
using Newtonsoft.Json;

namespace DocsMarshal.Connectors.Orchestrator.Managers
{
    public class WorkflowManager : DocsMarshal.Connectors.Interfaces.Managers.Workflow.IWorkflowManager
    {
        private Manager Orchestrator = null;
        public Interfaces.Managers.Workflow.IWorkflowEvents WfEvents { get; private set; }

        public WorkflowManager(DocsMarshal.Connectors.Orchestrator.Manager manager)
        {
            Orchestrator = manager;
            WfEvents = new WorkflowEventsManager(Orchestrator);
        }

        
        public void Dispose()
        {
            if (Orchestrator != null) Orchestrator = null;
            if(WfEvents != null)
            {
                WfEvents.Dispose();
                WfEvents = null;
            }
        }

        public async Task<GetActiveTasksResponse> GetActiveTasks()
        {
            var ritO = await Orchestrator.PostAsync<Root>("/DMWorkflow/GetActiveTasks", new { sessionID = Orchestrator.SessionId });
            if (ritO != null && ritO.Result != null)
                if (ritO.Result.Tasks == null)
                    ritO.Result.Tasks = new System.Collections.Generic.List<DocsMarshal.Connectors.Entities.DmTask>();
            return ritO.Result;
        }

        public async Task<GetVariablesByIdTaskResponse> GetVariablesByIdTask(Guid idTask)
        {
            var ritO = await Orchestrator.PostAsync<RootVariables>("/DMWorkflow/GetVariablesByIdTask", new { sessionID = Orchestrator.SessionId, idTask = idTask });
            if (ritO != null && ritO.Result != null)
            {
                if (ritO.Result.Variables == null)
                    ritO.Result.Variables = new System.Collections.Generic.List<DocsMarshal.Connectors.Entities.DmTaskVariable>();
                foreach (var variable in ritO.Result.Variables)
                    if (variable.FieldType == Entities.Enums.EFieldType.Guid)
                    {
                        if (variable.DefaultValue is string defaultValueStr && Guid.TryParse(defaultValueStr, out Guid defaultValueGuid))
                            variable.DefaultValue = defaultValueGuid;
                        if (variable.Value is string valueStr && Guid.TryParse(valueStr, out Guid valueGuid))
                            variable.Value = valueGuid;
                    }
            }
            return ritO.Result;
        }

        public async Task<BaseReturnEntity> SetVariableValueByIdTaskVariable(string idTask, Parameter variable)
        {
            var ritO = await Orchestrator.PostAsync<RootBase>("/DMWorkflow/SetVariableValueByIdTaskVariable", new { sessionID = Orchestrator.SessionId, idTask = idTask, variable = variable });
            return ritO.Result;
        }

        public async Task<BaseReturnEntity> TaskComplete(string idTask, string outcome)
        {
            var ritO = await Orchestrator.PostAsync<RootBase>("/DMWorkflow/TaskComplete", new { sessionID = Orchestrator.SessionId, idTask = idTask, outcome = outcome });
            return ritO.Result;
        }

        public async Task<BaseReturnEntity> TaskTakeInChargeByIdTask(string idTask)
        {
            var ritO = await Orchestrator.PostAsync<RootBase>("/DMWorkflow/TaskTakeInChargeByIdTask", new { sessionID = Orchestrator.SessionId, idTask = idTask });
            return ritO.Result;
        }

        public async Task<BaseReturnEntity> TaskUndoTakeInChargeByIdTask(string idTask)
        {
            var ritO = await Orchestrator.PostAsync<RootBase>("/DMWorkflow/TaskUndoTakeInChargeByIdTask", new { sessionID = Orchestrator.SessionId, idTask = idTask });
            return ritO.Result;
        }

        public async Task<List<Outcome>> GetOutcomesByIdTask(Guid idTask)
        {
            var ritO = await Orchestrator.PostAsync<BaseJsonResult<List<Outcome>>>("/Workflow/GetTaskOutcomes", new { sessionID = Orchestrator.SessionId, idTask = idTask });
            if (ritO.Error)
                throw new Exception(ritO.ErrorDescription);
            return ritO.Data;
        }

        private class RootVariables
        {
            public DocsMarshal.Connectors.Entities.GetVariablesByIdTaskResponse Result { get; set; }
        }

        private class RootBase
        {
            public DocsMarshal.Connectors.Entities.BaseReturnEntity Result { get; set; }
        }

        private class Root
        {
            public DocsMarshal.Connectors.Entities.GetActiveTasksResponse Result { get; set; }
        }
    }
}
