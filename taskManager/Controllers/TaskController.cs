using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskManager.Data;
using taskManager.Models;
using AutoMapper;

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
            { //Create
                task.created_at = DateTime.Now;
                task.Status = Status.pending;
                _context.TaskLists.Add(task);
            }
            else
            {
                //Edit
                var existingTask = await _context.TaskLists.FindAsync(task.Id);

                if(existingTask == null)
                     return new JsonResult(NotFound());

                existingTask.Status = task.Status;
                existingTask.updated_at = DateTime.Now;
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
        // [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var results = await _context.TaskLists.ToListAsync();
           
            return new JsonResult(results);
        }

        //Filter by Status
        [HttpGet]
        public async Task<IEnumerable<TaskList>> FilterByStatus(string searchStatus)
        {
            IQueryable<TaskList> query = _context.TaskLists;
            searchStatus.ToLower();
            int statusID = 0;
            if(searchStatus != null)
            {
                string[] statusList = {"pending","in progress", "completed"};
                for(int i = 0; i < statusList.Length; i++){
                    if(String.Equals(searchStatus, statusList[i]))
                    {
                        statusID = i;
                        break;
                    }
                } 
                
                
                query = query.Where(e => e.Status == (Status)statusID); 
               
          
            }
            
            return await query.ToListAsync();
        }


    }
}