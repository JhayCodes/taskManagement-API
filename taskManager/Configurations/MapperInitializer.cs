using AutoMapper;
using taskManager.Models;
using taskManager.DTOs;

namespace taskManager.configurations
{
    public class MapperInitializer :Profile{

        public MapperInitializer() 
        {
            CreateMap<TaskList, TaskListDTO >().Reverse();
            CreateMap<TaskList, CreateTaskListDTO >().Reverse();
        }
    }
}