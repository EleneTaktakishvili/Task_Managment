
using AutoMapper;
using TaskManagment.Application.DTOs;
using TaskManagment.Domain.Entities;
using Task = TaskManagment.Domain.Entities.Task;

namespace TaskManagment.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task> ();
            CreateMap<User, UserDto>();
            CreateMap<UserTask, UserTaskDto>();
        }
    }
}
