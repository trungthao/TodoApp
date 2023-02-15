using System.ComponentModel.DataAnnotations;

namespace TodoApp.Shared.DataTransferObjects
{
    public record ListTaskForUpdateDto : ListTaskForManipulcationDto
    {
        public IEnumerable<TaskItemForUpdateDto>? Tasks { get; init; }
    }
}