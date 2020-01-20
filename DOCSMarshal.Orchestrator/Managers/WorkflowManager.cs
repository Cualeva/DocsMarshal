using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocsMarshal.Entities;
using DocsMarshal.Interfaces.Managers.Profile;
using Newtonsoft.Json;

namespace DocsMarshal.Orchestrator.Managers
{
    public class WorkflowManager : DocsMarshal.Interfaces.Managers.Workflow.IWorkflowManager
    {
        private Manager Orchestrator = null;
        public Interfaces.Managers.Workflow.IWorkflowEvents WfEvents { get; private set; }

        public WorkflowManager(DocsMarshal.Orchestrator.Manager manager)
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
            using (var client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}/DMWorkflow/GetActiveTasks", Orchestrator.DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new { sessionID = Orchestrator.SessionId});
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = response.Content.ReadAsStringAsync().Result;
                    var ritO = JsonConvert.DeserializeObject<Root>(rit);
                    if (ritO != null && ritO.Result != null)
                    {
                        if (ritO.Result.Tasks == null) ritO.Result.Tasks = new System.Collections.Generic.List<DocsMarshal.Entities.DmTask>();
                    }
                    return ritO.Result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<GetVariablesByIdTaskResponse> GetVariablesByIdTask(Guid idTask)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}/DMWorkflow/GetVariablesByIdTask", Orchestrator.DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new { sessionID = Orchestrator.SessionId, idTask=idTask });
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = response.Content.ReadAsStringAsync().Result;
                    var ritO = JsonConvert.DeserializeObject<RootVariables>(rit);
                    if (ritO != null && ritO.Result != null)
                    {
                        if (ritO.Result.Variables == null)
                            ritO.Result.Variables = new System.Collections.Generic.List<DocsMarshal.Entities.DmTaskVariable>();
                        foreach(var variable in ritO.Result.Variables)
                            if(variable.FieldType == Entities.Enums.EFieldType.Guid)
                            {
                                if (variable.DefaultValue is string defaultValueStr && Guid.TryParse(defaultValueStr, out Guid defaultValueGuid))
                                    variable.DefaultValue = defaultValueGuid;
                                if (variable.Value is string valueStr && Guid.TryParse(valueStr, out Guid valueGuid))
                                    variable.Value = valueGuid;
                            }
                    }
                    return ritO.Result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<BaseReturnEntity> SetVariableValueByIdTaskVariable(string idTask, Parameter variable)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}/DMWorkflow/SetVariableValueByIdTaskVariable", Orchestrator.DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new { sessionID = Orchestrator.SessionId, idTask = idTask, variable=variable });
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = response.Content.ReadAsStringAsync().Result;
                    var ritO = JsonConvert.DeserializeObject<RootBase>(rit);
                    return ritO.Result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<BaseReturnEntity> TaskComplete(string idTask, string outcome)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}/DMWorkflow/TaskComplete", Orchestrator.DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new { sessionID = Orchestrator.SessionId, idTask = idTask, outcome = outcome });
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = response.Content.ReadAsStringAsync().Result;
                    var ritO = JsonConvert.DeserializeObject<RootBase>(rit);
                    return ritO.Result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<BaseReturnEntity> TaskTakeInChargeByIdTask(string idTask)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}/DMWorkflow/TaskTakeInChargeByIdTask", Orchestrator.DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new { sessionID = Orchestrator.SessionId, idTask = idTask});
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = response.Content.ReadAsStringAsync().Result;
                    var ritO = JsonConvert.DeserializeObject<RootBase>(rit);
                    return ritO.Result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<BaseReturnEntity> TaskUndoTakeInChargeByIdTask(string idTask)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}/DMWorkflow/TaskUndoTakeInChargeByIdTask", Orchestrator.DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new { sessionID = Orchestrator.SessionId, idTask = idTask });
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = response.Content.ReadAsStringAsync().Result;
                    var ritO = JsonConvert.DeserializeObject<RootBase>(rit);
                    return ritO.Result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<Outcome>> GetOutcomesByIdTask(Guid idTask)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = string.Format("{0}/Workflow/GetTaskOutcomes", Orchestrator.DocsMarshalUrl);
                    var serializedItem = JsonConvert.SerializeObject(new { sessionID = Orchestrator.SessionId, idTask = idTask });
                    var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                    string rit = await response.Content.ReadAsStringAsync();
                    var ritO = JsonConvert.DeserializeObject<BaseJsonResult<List<Outcome>>>(rit);
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

        private class RootVariables
        {
            public DocsMarshal.Entities.GetVariablesByIdTaskResponse Result { get; set; }
        }

        private class RootBase
        {
            public DocsMarshal.Entities.BaseReturnEntity Result { get; set; }
        }

        private class Root
        {
            public DocsMarshal.Entities.GetActiveTasksResponse Result { get; set; }
        }
    }
}
