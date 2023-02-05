using System;
using TodoApp.Contracts;
using TodoApp.Entities.Models;

namespace TodoApp.Repository
{
    public class ListTaskRepository : RepositoryBase<ListTask>, IListTaskRepository
    {
        public ListTaskRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateListTask(ListTask listTask)
        {
            Create(listTask);
        }

        public IEnumerable<ListTask> GetAllListTask(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(l => l.Name)
                .ToList();
        }

        public ListTask? GetListTask(Guid listTaskId, bool trackChanges)
        {
            return FindByCondition(lt => lt.Id == listTaskId, trackChanges)
                .SingleOrDefault();
        }

        public IEnumerable<ListTask> GetListTaskByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToList();
        }
    }
}

