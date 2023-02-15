using System;
using TodoApp.Entities.Models;
using TodoApp.Shared.DataTransferObjects;

namespace TodoApp.Service.Contracts
{
    public interface ITaskItemService
    {
        IEnumerable<TaskItemDto> GetTaskItems(Guid listTaskId, bool trackChanges);

        TaskItemDto CreateTaskItem(Guid listTaskId, TaskItemForCreationDto taskItem, bool trackChanges);

        TaskItemDto GetTaskItemForListTask(Guid listTaskId, Guid id, bool trackChanges);

        void DeleteTaskItemForListTask(Guid listTaskId, Guid id, bool trackChanges);

        void UpdateTaskItemForListTask(Guid listTaskId, Guid id,
            TaskItemForUpdateDto taskItemForUpdateDto,
            bool listTaskTrackChanges, bool taskItemTrackChanges
        );

        (TaskItemForUpdateDto taskItemToPatch, TaskItem taskItemEntity) GetTaskItemForPatch(Guid listTaskId, Guid taskItemId, bool listTrackChanges, bool itemTrackChanges);

        void SaveChangesForPatch(TaskItemForUpdateDto taskItemForUpdate, TaskItem taskItemEntity);
    }
}

