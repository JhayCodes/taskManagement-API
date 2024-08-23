



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
        public JsonResult CreateEdit(TaskList task)
        {
            if (task.Id == 0)
            {
                _context.TaskLists.Add(task);
            }
            else
            {
                var taskInDb = _context.TaskLists.Find(task.Id);

                if(taskInDb == null)
                     return new JsonResult(NotFound());

                taskInDb.updated_at = DateTime.Now;
                taskInDb = task; 
            }

            _context.SaveChanges();

            return new JsonResult(Ok(task));
        }
    
        //Get 
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.TaskLists.Find(id);

            if(result == null)
                return new JsonResult(NotFound());
            
            return new JsonResult(Ok(result));
        }

        //Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.TaskLists.Find(id);

            if(result == null)
              return new JsonResult(NotFound());

            _context.TaskLists.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }
    
        //Get all
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.TaskLists.ToList();

            return new JsonResult(result);
        }
    }
}