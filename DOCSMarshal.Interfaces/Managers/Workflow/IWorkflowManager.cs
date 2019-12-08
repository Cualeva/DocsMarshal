using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocsMarshal.Interfaces.Managers.Workflow
{
    public interface IWorkflowManager: IDisposable
    {
        Task<Entities.GetActiveTasksResponse> GetActiveTasks();
        Task<Entities.GetVariablesByIdTaskResponse> GetVariablesByIdTask(Guid idTask);
        Task<List<Entities.Outcome>> GetOutcomesByIdTask(Guid idTask);
        Task<Entities.BaseReturnEntity> SetVariableValueByIdTaskVariable(string idTask, Entities.Parameter variable);
        Task<Entities.BaseReturnEntity> TaskComplete(string idTask, string outcome);
        Task<Entities.BaseReturnEntity> TaskTakeInChargeByIdTask(string idTask);
        Task<Entities.BaseReturnEntity> TaskUndoTakeInChargeByIdTask(string idTask);
        IWorkflowEvents WfEvents { get; }
    }
}
