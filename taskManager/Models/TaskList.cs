namespace taskManager.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Status Status { get; set; }  = Status.pending;
        public DateTime created_at { get; set; } 
        public DateTime? updated_at { get; set; } 
    }
}