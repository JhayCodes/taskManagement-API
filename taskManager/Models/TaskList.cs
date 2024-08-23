namespace taskManager.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } 
    }
}