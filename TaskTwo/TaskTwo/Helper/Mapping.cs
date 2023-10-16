using AutoMapper;
using TaskTwo.Dto;
using TaskTwo.Models;

namespace TaskTwo.Helper
{
    public class Mapping: Profile
    {
        public Mapping() {
            CreateMap<Tasks,TasksDto>();
            CreateMap<SubTask,SubTaskDto>();
        }
    }
}
