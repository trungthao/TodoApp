using AutoMapper;
using TodoApp.Contracts;
using TodoApp.Entities.Exceptions;
using TodoApp.Service.Contracts;
using TodoApp.Shared.DataTransferObjects;
using TodoApp.Entities.Models;
using TodoApp.Shared.RequestParameters;
using TodoApp.Entities.LinkModels;
using Service.Contracts;

namespace TodoApp.Service
{
    public class TaskItemService : ITaskItemService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly ITaskItemLinks _taskItemLinks;

        public TaskItemService(IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper,
            ITaskItemLinks taskItemLinks)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
            _taskItemLinks = taskItemLinks;
        }

        public async Task<TaskItemDto> CreateTaskItemAsync(Guid listTaskId, TaskItemForCreationDto taskItemForCreationDto, bool trackChanges)
        {
            await CheckIfListTaskExistsAsync(listTaskId, trackChanges);
            var taskItemEntity = _mapper.Map<TaskItem>(taskItemForCreationDto);
            _repositoryManager.TaskItem.CreateTaskItem(listTaskId, taskItemEntity);
            await _repositoryManager.SaveAsync();

            var taskItemToReturn = _mapper.Map<TaskItemDto>(taskItemEntity);

            return taskItemToReturn;
        }

        public async Task DeleteTaskItemForListTaskAsync(Guid listTaskId, Guid id, bool trackChanges)
        {
            await CheckIfListTaskExistsAsync(listTaskId, trackChanges);

            var taskItem = await GetTaskItemAndCheckIfExistsAsync(listTaskId, id, trackChanges);

            _repositoryManager.TaskItem.DeleteTaskItem(taskItem);
            await _repositoryManager.SaveAsync();
        }

        public async Task<TaskItemDto> GetTaskItemForListTaskAsync(Guid listTaskId, Guid id, bool trackChanges)
        {
            await CheckIfListTaskExistsAsync(listTaskId, trackChanges);

            var taskItem = GetTaskItemAndCheckIfExistsAsync(listTaskId, id, trackChanges);

            var taskItemDto = _mapper.Map<TaskItemDto>(taskItem);
            return taskItemDto;
        }

        public async Task<(TaskItemForUpdateDto taskItemToPatch, TaskItem taskItemEntity)>
            GetTaskItemForPatchAsync(Guid listTaskId, Guid taskItemId, bool listTrackChanges, bool itemTrackChanges)
        {
            await CheckIfListTaskExistsAsync(listTaskId, listTrackChanges);

            var taskItemEntity = await GetTaskItemAndCheckIfExistsAsync(listTaskId, taskItemId, itemTrackChanges);

            var taskItemToPatch = _mapper.Map<TaskItemForUpdateDto>(taskItemEntity);

            return (taskItemToPatch, taskItemEntity);
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetTaskItemsAsync(Guid listTaskId, LinkParameters linkParams, bool trackChanges)
        {
            await CheckIfListTaskExistsAsync(listTaskId, trackChanges);

            if (!linkParams.TaskItemParameters.ValidDateRange)
                throw new DateRangeBadRequestException();

            var taskItemsWithMetaData = await _repositoryManager.TaskItem.GetTaskItemsAsync(listTaskId, linkParams.TaskItemParameters, trackChanges);
            var taskItemDto = _mapper.Map<IEnumerable<TaskItem>, IEnumerable<TaskItemDto>>(taskItemsWithMetaData);
            var links = _taskItemLinks.TryGenerateLinks(taskItemDto, linkParams.TaskItemParameters.Fields, listTaskId, linkParams.Context);
            return (linkResponse: links, metaData: taskItemsWithMetaData.MetaData);
        }

        public async Task SaveChangesForPatchAsync(TaskItemForUpdateDto taskItemForUpdate, TaskItem taskItemEntity)
        {
            _mapper.Map(taskItemForUpdate, taskItemEntity);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateTaskItemForListTaskAsync(Guid listTaskId, Guid id, TaskItemForUpdateDto taskItemForUpdateDto, bool listTaskTrackChanges, bool taskItemTrackChanges)
        {
            await CheckIfListTaskExistsAsync(listTaskId, listTaskTrackChanges);

            var taskItemEntity = await GetTaskItemAndCheckIfExistsAsync(listTaskId, id, taskItemTrackChanges);

            _mapper.Map(taskItemForUpdateDto, taskItemEntity);
            await _repositoryManager.SaveAsync();
        }

        private async Task CheckIfListTaskExistsAsync(Guid listtaskId, bool trackChanges)
        {
            var listTask = await _repositoryManager.ListTask.GetListTaskAsync(listtaskId, trackChanges);
            if (listTask == null)
                throw new ListTaskNotFoundException(listtaskId);
        }

        private async Task<TaskItem> GetTaskItemAndCheckIfExistsAsync(Guid listTaskId, Guid id, bool trackChanges)
        {
            var taskItem = await _repositoryManager.TaskItem.GetTaskItemForListTaskAsync(listTaskId, id, trackChanges);
            if (taskItem == null)
                throw new TaskItemNotFoundException(listTaskId, id);

            return taskItem;
        }
    }
}

