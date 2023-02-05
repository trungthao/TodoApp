using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Entities.Models;

namespace TodoApp.Repository.Configuration
{
    public class ListTaskConfiguration : IEntityTypeConfiguration<ListTask>
    {
        public void Configure(EntityTypeBuilder<ListTask> builder)
        {
            builder.HasData(
                new ListTask
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "Công việc hôm nay"
                },
                new ListTask
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "Công việc của tôi"
                }
            );
        }
    }
}

