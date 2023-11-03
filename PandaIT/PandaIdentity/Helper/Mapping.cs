using AutoMapper;
using PandaIdentity.Dto;
using PandaIdentity.Models;
namespace PandaIT.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MyTaskCreateDto,MyTask>().ReverseMap();
            CreateMap<MySubTaskCreateDto,MySubTask>().ReverseMap();
            CreateMap<MyTaskDto,MyTask>().ReverseMap();
//            CreateMap<MySubTaskDto,MySubTask>().ReverseMap();
            CreateMap<MySubTaskCreateDto,MySubTask>().ReverseMap();
            CreateMap<MySubTask, MySubTaskDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority)).ReverseMap();

        }
    }
}
