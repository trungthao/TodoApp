using System;
using TodoApp.Entities.Models;

namespace TodoApp.Contracts
{
    public interface ITaskItemRepository
    {
        IEnumerable<TaskItem> GetTaskItems(Guid listTaskId, bool trackChanges);

        TaskItem? GetTaskItemForListTask(Guid listTaskId, Guid id, bool trackChanges);

        void CreateTaskItem(Guid listTaskId, TaskItem taskItem);

        void DeleteTaskItem(TaskItem taskItem);
    }
}

