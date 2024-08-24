using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace taskManager.DTOs
{
   
    //TaskList DTO
    public class CreateTaskListDTO 
    {        
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public Status Status { get; set; } = Status.pending;
    }

     public class TaskListDTO : CreateTaskListDTO
    {
        public int Id { get; set; }
 
    }


  
}