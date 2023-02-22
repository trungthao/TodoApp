using System;
using TodoApp.Entities.Models;
using TodoApp.Shared.RequestParameters;

namespace TodoApp.Contracts
{
    public interface ITaskItemRepository
    {
        Task<PagedList<TaskItem>> GetTaskItemsAsync(Guid listTaskId, TaskItemParameters taskItemParameters, bool trackChanges);

        Task<TaskItem?> GetTaskItemForListTaskAsync(Guid listTaskId, Guid id, bool trackChanges);

        void CreateTaskItem(Guid listTaskId, TaskItem taskItem);

        void DeleteTaskItem(TaskItem taskItem);
    }
}

