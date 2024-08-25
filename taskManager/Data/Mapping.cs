using AutoMapper;
using taskManager.DTOs;
using taskManager.Models;

namespace taskManager
{
     public class Mapping : Profile
     {
        public Mapping() {
            CreateMap<TaskList, GetListDTO>().ReverseMap();
            CreateMap<TaskList, CreateEditDTO>().ReverseMap();
            
        }
     }
}