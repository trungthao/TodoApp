using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Entities.Models
{
    public class TaskItem
    {
        [Column("TaskId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống.")]
        public string? Name { get; set; }

        public DateTime DueDate { get; set; }

        [ForeignKey(nameof(ListTask))]
        public Guid ListTaskId { get; set; }

        public ListTask? ListTask { get; set; }
    }
}

