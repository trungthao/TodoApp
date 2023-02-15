using System.ComponentModel.DataAnnotations;

namespace TodoApp.Shared.DataTransferObjects
{
    public record ListTaskForCreationDto : ListTaskForManipulcationDto
    {
        public IEnumerable<TaskItemForCreationDto>? Tasks { get; init; }
    }
}