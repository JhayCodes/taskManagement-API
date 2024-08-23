

using Microsoft.AspNetCore.Mvc;
using taskManager.Data;
using taskManager.Models;

namespace taskManager.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class taskManagerController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public taskManagerController(TaskDbContext context)
        {
            _context = context;
        }

        //Create/Edit
        [HttpPost]
        public async Task<JsonResult> CreateEdit(TaskList task)
        {
            if (task.Id == 0)
            {
                task.created_at = DateTime.UtcNow;
                task.updated_at = DateTime.UtcNow;
                task.Status = Status.pending;
                _context.TaskLists.Add(task);
            }
            else
            {
                var existingTask = await _context.TaskLists.FindAsync(task.Id);

                if(existingTask == null)
                     return new JsonResult(NotFound());

                existingTask.Status = task.Status;
                existingTask.updated_at = DateTime.UtcNow;
                existingTask = task;
            }

            await _context.SaveChangesAsync();

            return new JsonResult(Ok(task));
        }
    
        //Get 
        [HttpGet]
        public async Task<JsonResult> Get(int id)
        {
            var result = await _context.TaskLists.FindAsync(id);

            if(result == null)
                return new JsonResult(NotFound());
            
            return new JsonResult(Ok(result));
        }

        //Delete
        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            //find task to delete by id
            var result = _context.TaskLists.Find(id);

            if(result == null)
              return new JsonResult(NotFound());

            _context.TaskLists.Remove(result);
            await _context.SaveChangesAsync();

            return new JsonResult(NoContent());
        }
    
        //Get all
        [HttpGet()]
        public async Task<JsonResult> GetAll()
        {
            var result =  _context.TaskLists.ToList();

            return new JsonResult(result);
        }
    }
}