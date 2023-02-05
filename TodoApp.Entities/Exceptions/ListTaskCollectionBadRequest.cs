namespace TodoApp.Entities.Exceptions
{
    public class ListTaskCollectionBadRequest : BadRequestException
    {
        public ListTaskCollectionBadRequest() : base("List Task Collection sent from a client is null.")
        {
        }
    }
}