namespace TodoApp.Shared.DataTransferObjects
{
    public record ListTaskDto
    {
        public Guid Id { get; init; }

        public string? Name { get; init; }
    }
}

