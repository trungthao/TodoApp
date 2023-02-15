using System.ComponentModel.DataAnnotations;

namespace TodoApp.Shared.DataTransferObjects
{
    public abstract record TaskItemForManipulationDto
    {
        [Required(ErrorMessage = "Task name is a required field.")]
        public string? Name { get; init; }

        public DateTime? DueDate { get; init; }
    }
}