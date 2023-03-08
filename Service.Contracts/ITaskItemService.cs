using TodoApp.Entities.LinkModels;
using TodoApp.Entities.Models;
using TodoApp.Shared.DataTransferObjects;
using TodoApp.Shared.RequestParameters;

namespace TodoApp.Service.Contracts
{
    public interface ITaskItemService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetTaskItemsAsync(Guid listTaskId, LinkParameters linkParams, bool trackChanges);

        Task<TaskItemDto> CreateTaskItemAsync(Guid listTaskId, TaskItemForCreationDto taskItem, bool trackChanges);

        Task<TaskItemDto> GetTaskItemForListTaskAsync(Guid listTaskId, Guid id, bool trackChanges);

        Task DeleteTaskItemForListTaskAsync(Guid listTaskId, Guid id, bool trackChanges);

        Task UpdateTaskItemForListTaskAsync(Guid listTaskId, Guid id,
            TaskItemForUpdateDto taskItemForUpdateDto,
            bool listTaskTrackChanges, bool taskItemTrackChanges
        );

        Task<(TaskItemForUpdateDto taskItemToPatch, TaskItem taskItemEntity)> GetTaskItemForPatchAsync(Guid listTaskId, Guid taskItemId, bool listTrackChanges, bool itemTrackChanges);

        Task SaveChangesForPatchAsync(TaskItemForUpdateDto taskItemForUpdate, TaskItem taskItemEntity);
    }
}

