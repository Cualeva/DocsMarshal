using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.MVVM.Managers
{
    public abstract class OfflineManager
    {
        protected abstract SQLite.SQLiteAsyncConnection GetConnection();
        protected abstract DocsMarshal.Interfaces.IManager GetOrchestrator();
        protected abstract bool IsDeviceConnectedToInternet();

        private bool IsRunning { get; set; }

        public async Task Run()
        {
            if (IsRunning)
                return;
            try
            {
                var connection = GetConnection();
                var table = connection.Table<Models.QueuedRemoteOperation>();
                while (true)
                {
                    IsRunning = true;
                    if (IsDeviceConnectedToInternet())
                    {
                        var operations = await table.ToListAsync();
                        foreach (var operation in operations.OrderBy(x => x.Sequence))
                        {
                            try
                            {
                                switch ((Models.QueuedRemoteOperation.ERemoteOperationType)operation.Type)
                                {
                                    case Models.QueuedRemoteOperation.ERemoteOperationType.ProfileInsert:
                                        await ManageProfileInsert(operation);
                                        break;
                                    case Models.QueuedRemoteOperation.ERemoteOperationType.ProfileUpdate:
                                        await ManageProfileUpdate(operation);
                                        break;
                                    case Models.QueuedRemoteOperation.ERemoteOperationType.ProfileDelete:
                                        await ManageProfileDelete(operation);
                                        break;
                                    case Models.QueuedRemoteOperation.ERemoteOperationType.TaskVariableSetValue:
                                        await ManageTaskVariableSetValue(operation);
                                        break;
                                    case Models.QueuedRemoteOperation.ERemoteOperationType.TaskClose:
                                        await ManageTaskClose(operation);
                                        break;
                                    default:
                                        break;
                                }
                                await connection.DeleteAsync(operation);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                        break; // break
                    }

                    await Task.Delay(TimeSpan.FromSeconds(20));
                }
            }
            finally
            {
                IsRunning = false;
            }
        }

        private async Task QueueOperation(Models.QueuedRemoteOperation.ERemoteOperationType type, object payload)
        {
            var item = new Models.QueuedRemoteOperation
            {
                Type = (int)type,
                SerializedPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload)
            };
            var connection = GetConnection();
            var rowsAdded = await connection.InsertAsync(item);
            if (rowsAdded != 1)
                throw new Exception($"Could not queue operation: {rowsAdded}");

            if (!IsRunning)
                _ = Run();
        }

        public async Task QueueProfileInsert(DocsMarshal.Entities.ProfileForInsert profileForInsert)
        {
            await QueueOperation(Models.QueuedRemoteOperation.ERemoteOperationType.ProfileInsert, profileForInsert);
        }

        public async Task QueueProfileUpdate(DocsMarshal.Entities.ProfileForUpdate profileForUpdate)
        {
            await QueueOperation(Models.QueuedRemoteOperation.ERemoteOperationType.ProfileUpdate, profileForUpdate);
        }

        public async Task QueueProfileDelete(Guid objectId)
        {
            await QueueOperation(Models.QueuedRemoteOperation.ERemoteOperationType.ProfileDelete, objectId);
        }

        public async Task QueueTaskVariableSetValue(DocsMarshal.Entities.DmTaskVariable variable)
        {
            throw new NotImplementedException();
        }

        public async Task QueueTaskClose(Guid taskId, string outcomeValue)
        {
            throw new NotImplementedException();
        }

        private async Task ManageProfileInsert(Models.QueuedRemoteOperation operation)
        {
            var orchestrator = GetOrchestrator();
            var profileForInsert = Newtonsoft.Json.JsonConvert.DeserializeObject<DocsMarshal.Entities.ProfileForInsert>(operation.SerializedPayload);
            var inserted = await orchestrator.Profile.Archive.Insert(profileForInsert);
            if (inserted.HasError)
                throw new Exception(inserted.Error);
        }

        private async Task ManageProfileUpdate(Models.QueuedRemoteOperation operation)
        {
            var orchestrator = GetOrchestrator();
            var profileForUpdate = Newtonsoft.Json.JsonConvert.DeserializeObject<DocsMarshal.Entities.ProfileForUpdate>(operation.SerializedPayload);
            var updated = await orchestrator.Profile.Archive.Update(profileForUpdate);
            if (updated.HasError)
                throw new Exception(updated.Error);
        }

        private async Task ManageProfileDelete(Models.QueuedRemoteOperation operation)
        {
            throw new NotImplementedException();
        }

        private async Task ManageTaskVariableSetValue(Models.QueuedRemoteOperation operation)
        {
            throw new NotImplementedException();
        }

        private async Task ManageTaskClose(Models.QueuedRemoteOperation operation)
        {
            throw new NotImplementedException();
        }
    }
}
