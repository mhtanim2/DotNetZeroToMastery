using AutoMapper;
using PandaIT.Dto;
using PandaIT.Models;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PandaIT.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Department, DepartmentDto>();
        }
    }
}
