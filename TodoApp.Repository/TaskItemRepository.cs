using System;
using TodoApp.Contracts;
using TodoApp.Entities.Models;

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

        public TaskItem? GetTaskItemForListTask(Guid listTaskId, Guid id, bool trackChanges)
        {
            return FindByCondition(t => t.ListTaskId.Equals(listTaskId) && t.Id.Equals(id), trackChanges)
                .FirstOrDefault();
        }

        public IEnumerable<TaskItem> GetTaskItems(Guid listTaskId, bool trackChanges)
        {
            return FindByCondition(t => t.ListTaskId.Equals(listTaskId), trackChanges)
                .OrderBy(t => t.DueDate)
                .ToList();
        }
    }
}

