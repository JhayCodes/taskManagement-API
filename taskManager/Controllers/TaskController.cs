using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using taskManager.Data;
using taskManager.Models;
using AutoMapper;
using taskManager.DTOs;


namespace taskManager.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class taskManagerController : ControllerBase
    {
        private readonly TaskDbContext _context;
        private readonly IMapper _mapper;

        public taskManagerController(TaskDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Create/Edit
        [HttpPost]
        public async Task<JsonResult> CreateEdit(CreateEditDTO task)
        {
            if (task.Id == 0)
            { //Create
                task.created_at = DateTime.Now;
                task.updated_at = null;
                task.status = "pending";

                var finalResult = _mapper.Map<TaskList>(task);

                await _context.TaskLists.AddAsync(finalResult);
            }
            else
            {
                //Edit
                var taskInDb = await _context.TaskLists.FindAsync(task.Id);

                if(taskInDb == null)
                     return new JsonResult(NotFound());

                //  
                taskInDb.updated_at = DateTime.Now;
                taskInDb = _mapper.Map<TaskList>(task);
            }

             await _context.SaveChangesAsync();

            return new JsonResult(Ok(_mapper.Map<TaskList>(task)));
        }
    
        //Get 
        [HttpGet]
        public async Task<JsonResult> Get(int id)
        {
            var result = await _context.TaskLists.FindAsync(id);

            if(result == null)
                return new JsonResult(NotFound());
            
            result.created_at.ToString("yyyy-MM-dd HH:mm:ss");

            var resultDTO = _mapper.Map<GetListDTO>(result);

            return new JsonResult(Ok(resultDTO));
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
        [HttpGet("/GetAll")]
        public async Task<JsonResult> GetAll(string? searchStatus)
        {   
            var results = await _context.TaskLists.ToListAsync();
            
            if(searchStatus != null)
            {
                searchStatus.ToLower();
                string[] statusList = {"pending","in progress", "completed"};

                for(int i = 0; i < statusList.Length; i++){
                    if(String.Equals(searchStatus, statusList[i]))
                    {
                        break;
                    }
                } 

                results = results.Where(x => x.status == searchStatus).ToList();
                
               
            }
            var resultDTO = _mapper.Map<IEnumerable<GetListDTO>>(results);
            return new JsonResult(resultDTO);
        }


    }
}