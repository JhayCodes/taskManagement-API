using Microsoft.EntityFrameworkCore;
using taskManager.Models;

namespace taskManager.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) 
        { }
        
        public DbSet<TaskList> TaskLists { get; set;}
    }
}