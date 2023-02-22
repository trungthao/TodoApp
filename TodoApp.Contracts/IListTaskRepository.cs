using System;
using TodoApp.Entities.Models;

namespace TodoApp.Contracts
{
    public interface IListTaskRepository
    {
        Task<IEnumerable<ListTask>> GetAllListTaskAsync(bool trackChanges);

        Task<IEnumerable<ListTask>> GetListTaskByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

        Task<ListTask?> GetListTaskAsync(Guid listTaskId, bool trackChanges);

        void CreateListTask(ListTask listTask);

        void DeleteListTask(ListTask listTask);
    }
}

