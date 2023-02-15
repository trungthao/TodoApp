using System.ComponentModel.DataAnnotations;

namespace TodoApp.Shared.DataTransferObjects
{
    public abstract record ListTaskForManipulcationDto
    {
        [Required(ErrorMessage = "ListTask name is a required field.")]
        public string? Name { get; init; }
    };
}