using System.ComponentModel.DataAnnotations;

namespace taskManager.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string status { get; set; } 
        public DateTime created_at { get; set; } 
        public DateTime? updated_at { get; set; }  
    }

}