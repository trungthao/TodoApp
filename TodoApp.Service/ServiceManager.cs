using System;
using AutoMapper;
using TodoApp.Contracts;
using TodoApp.Service.Contracts;

namespace TodoApp.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IListTaskService> _listTaskService;
        private readonly Lazy<ITaskItemService> _taskItemService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _listTaskService = new Lazy<IListTaskService>(() => new ListTaskService(repositoryManager, logger, mapper));
            _taskItemService = new Lazy<ITaskItemService>(() => new TaskItemService(repositoryManager, logger, mapper));
        }

        public IListTaskService ListTaskService => _listTaskService.Value;

        public ITaskItemService TaskItemService => _taskItemService.Value;
    }
}

