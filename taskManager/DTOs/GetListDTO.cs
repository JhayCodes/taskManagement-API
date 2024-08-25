using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace taskManager.DTOs
{
    public class GetListDTO
    {
        public string title { get; set; }
      
        public string description { get; set; }

        public string status { get; set; }
        public DateTime? updated_at { get; set; }
        
    }


}