using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace taskManager.DTOs
{
     public class CreateEditDTO
    {
        [JsonIgnore]
        
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description {get; set; }
        public string status   {get; set; }
        [JsonIgnore]
        public DateTime created_at { get; set; }
        [JsonIgnore]
        public DateTime? updated_at { get; set; }

    }


} 
 
