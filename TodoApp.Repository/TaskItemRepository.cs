using System;
using Microsoft.EntityFrameworkCore;
using TodoApp.Contracts;
using TodoApp.Entities.Models;
using TodoApp.Repository.Extensions;
using TodoApp.Shared.RequestParameters;

namespace TodoApp.Repository
{
    public class TaskItemRepository : RepositoryBase<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateTaskItem(Guid listTaskId, TaskItem taskItem)
        {
            taskItem.ListTaskId = listTaskId;
            Create(taskItem);
        }

        public void DeleteTaskItem(TaskItem taskItem)
        {
            Delete(taskItem);
        }

        public async Task<TaskItem?> GetTaskItemForListTaskAsync(Guid listTaskId, Guid id, bool trackChanges)
        {
            return await FindByCondition(t => t.ListTaskId.Equals(listTaskId) && t.Id.Equals(id), trackChanges)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<TaskItem>> GetTaskItemsAsync(Guid listTaskId, TaskItemParameters taskItemParameters, bool trackChanges)
        {
            var taskItems = await FindByCondition(t => t.ListTaskId.Equals(listTaskId), trackChanges)
                .FilterTaskItems(taskItemParameters.FromDate, taskItemParameters.ToDate)
                .Search(taskItemParameters.SearchTerm)
                .OrderBy(t => t.DueDate)
                .Skip((taskItemParameters.PageNumber - 1) * taskItemParameters.PageSize)
                .Take(taskItemParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(t => t.ListTaskId.Equals(listTaskId), trackChanges).CountAsync();

            return new PagedList<TaskItem>(taskItems, count, taskItemParameters.PageNumber, taskItemParameters.PageSize);
        }
    }
}

