using System;
using Microsoft.EntityFrameworkCore;
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

        public void DeleteListTask(ListTask listTask)
        {
            Delete(listTask);
        }

        public async Task<IEnumerable<ListTask>> GetAllListTaskAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(l => l.Name)
                .ToListAsync();
        }

        public async Task<ListTask?> GetListTaskAsync(Guid listTaskId, bool trackChanges)
        {
            return await FindByCondition(lt => lt.Id == listTaskId, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ListTask>> GetListTaskByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();
        }
    }
}

