using System;
using TodoApp.Contracts;

namespace TodoApp.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IListTaskRepository> _listTaskRepository;
        private readonly Lazy<ITaskItemRepository> _taskItemRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _listTaskRepository = new Lazy<IListTaskRepository>(() => new ListTaskRepository(repositoryContext));
            _taskItemRepository = new Lazy<ITaskItemRepository>(() => new TaskItemRepository(repositoryContext));
        }

        public IListTaskRepository ListTask => _listTaskRepository.Value;

        public ITaskItemRepository TaskItem => _taskItemRepository.Value;

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}

