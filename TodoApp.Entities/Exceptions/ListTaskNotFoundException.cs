namespace TodoApp.Entities.Exceptions
{
    public class ListTaskNotFoundException : NotFoundException
    {
        public ListTaskNotFoundException(Guid id) 
            : base($"The list task with id {id} does not exist.")
        {
        }
    }
}