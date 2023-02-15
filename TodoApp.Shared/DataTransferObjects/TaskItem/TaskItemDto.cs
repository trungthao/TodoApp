namespace TodoApp.Shared.DataTransferObjects
{
    public record TaskItemDto(Guid Id, string Name, DateTime DueDate);
}