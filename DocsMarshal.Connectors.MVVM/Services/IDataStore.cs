using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsMarshal.MVVM.Services
{
    public interface IDataStore<T>
    {
        Task<T> Insert(T model, bool raiseWorkflowEvents);
        Task<T> Update(T model, bool raiseWorkflowEvents);
        Task Delete(T model, bool raiseWorkflowEvents);
        Task Clear();
    }
}
