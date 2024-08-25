using System.ComponentModel.DataAnnotations;
using taskManager.Models;



public class User
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

    // public List<TaskList> TaskLists{ get; set; }
}
