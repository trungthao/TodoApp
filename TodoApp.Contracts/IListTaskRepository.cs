using System;
using TodoApp.Entities.Models;

namespace TodoApp.Contracts
{
    public interface IListTaskRepository
    {
        IEnumerable<ListTask> GetAllListTask(bool trackChanges);

        IEnumerable<ListTask> GetListTaskByIds(IEnumerable<Guid> ids, bool trackChanges);

        ListTask? GetListTask(Guid listTaskId, bool trackChanges);

        void CreateListTask(ListTask listTask);

        void DeleteListTask(ListTask listTask);
    }
}

