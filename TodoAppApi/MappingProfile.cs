using System;
using TodoApp.Entities.Models;
using TodoApp.Shared;
using AutoMapper;
using TodoApp.Shared.DataTransferObjects;

namespace TodoApp.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ListTask, ListTaskDto>();
            CreateMap<TaskItem, TaskItemDto>();
            CreateMap<ListTaskForCreationDto, ListTask>();
            CreateMap<ListTaskForUpdateDto, ListTask>();

            CreateMap<TaskItem, TaskItemDto>();
            CreateMap<TaskItemForCreationDto, TaskItem>();
            CreateMap<TaskItemForUpdateDto, TaskItem>().ReverseMap();
        }
    }
}

