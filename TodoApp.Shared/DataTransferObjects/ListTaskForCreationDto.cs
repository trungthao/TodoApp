namespace TodoApp.Shared.DataTransferObjects
{
    public record ListTaskForCreationDto(string Name, IEnumerable<TaskItemForCreationDto> Tasks);
}