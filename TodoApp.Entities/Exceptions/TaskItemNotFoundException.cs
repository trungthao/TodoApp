using System;
namespace TodoApp.Entities.Exceptions
{
    public class TaskItemNotFoundException : NotFoundException
    {
        public TaskItemNotFoundException(Guid listTaskId, Guid id)
            : base($"The task with id {id} and listTaskId {listTaskId} does not exist.")
        {
        }
    }
}