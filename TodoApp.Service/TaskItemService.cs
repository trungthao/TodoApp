using System.Text.RegularExpressions;
using System;
using AutoMapper;
using TodoApp.Contracts;
using TodoApp.Entities.Exceptions;
using TodoApp.Service.Contracts;
using TodoApp.Shared.DataTransferObjects;
using TodoApp.Entities.Models;

namespace TodoApp.Service
{
    public class TaskItemService : ITaskItemService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TaskItemService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public TaskItemDto CreateTaskItem(Guid listTaskId, TaskItemForCreationDto taskItemForCreationDto, bool trackChanges)
        {
            var listTask = _repositoryManager.ListTask.GetListTask(listTaskId, trackChanges);
            if (listTask == null)
                throw new ListTaskNotFoundException(listTaskId);

            var taskItemEntity = _mapper.Map<TaskItem>(taskItemForCreationDto);
            _repositoryManager.TaskItem.CreateTaskItem(listTaskId, taskItemEntity);
            _repositoryManager.Save();

            var taskItemToReturn = _mapper.Map<TaskItemDto>(taskItemEntity);

            return taskItemToReturn;
        }

        public TaskItemDto GetTaskItemForListTask(Guid listTaskId, Guid id, bool trackChanges)
        {
            var listTask = _repositoryManager.ListTask.GetListTask(listTaskId, trackChanges);
            if (listTask is null)
                throw new ListTaskNotFoundException(listTaskId);

            var taskItem = _repositoryManager.TaskItem.GetTaskItemForListTask(listTaskId, id, trackChanges);
            if (taskItem is null)
                throw new TaskItemNotFoundException(listTaskId, id);

            var taskItemDto = _mapper.Map<TaskItemDto>(taskItem);
            return taskItemDto;
        }

        public IEnumerable<TaskItemDto> GetTaskItems(Guid listTaskId, bool trackChanges)
        {
            var listTask = _repositoryManager.ListTask.GetListTask(listTaskId, trackChanges);
            if (listTask == null)
                throw new ListTaskNotFoundException(listTaskId);

            var taskItems = _repositoryManager.TaskItem.GetTaskItems(listTaskId, trackChanges);
            var taskItemDto = _mapper.Map<IEnumerable<TaskItem>, IEnumerable<TaskItemDto>>(taskItems);

            return taskItemDto;
        }
    }
}

