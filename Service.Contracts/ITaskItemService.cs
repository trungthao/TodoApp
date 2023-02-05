using System;
using TodoApp.Shared.DataTransferObjects;

namespace TodoApp.Service.Contracts
{
    public interface ITaskItemService
    {
        IEnumerable<TaskItemDto> GetTaskItems(Guid listTaskId, bool trackChanges);

        TaskItemDto CreateTaskItem(Guid listTaskId, TaskItemForCreationDto taskItem, bool trackChanges);

        TaskItemDto GetTaskItemForListTask(Guid listTaskId, Guid id, bool trackChanges);
    }
}

