using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Entities.Models
{
    public class ListTask
    {
        [Column("ListId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống.")]
        [MaxLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
        public string? Name { get; set; }

        public ICollection<TaskItem>? Tasks { get; set; }
    }
}

