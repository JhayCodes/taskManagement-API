using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using taskManager.Data;
using taskManager.Models;
using AutoMapper;
using taskManager.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;


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
            try
            {
                if (task.Id == 0)
                {
                    //Create
                    task.created_at = DateTime.Now;
                    task.updated_at = null;
                    task.status = "pending";

                    var finalResult = _mapper.Map<TaskList>(task);

                    await _context.TaskLists.AddAsync(finalResult);
                    await _context.SaveChangesAsync();
                    return new JsonResult(new { success = "List Created Successfully" })
                    {
                        StatusCode = 201
                    };
                }
                else
                {
                    //Edit
                    var taskInDb = await _context.TaskLists.FindAsync(task.Id);

                    if (taskInDb == null)
                        return new JsonResult(new { error = "List not found" })
                        {
                            StatusCode = 400
                        };

                    //  
                    taskInDb.updated_at = DateTime.Now;
                    taskInDb = _mapper.Map<TaskList>(task);
                    await _context.SaveChangesAsync();
                    return new JsonResult(new { success = "List Updated Successfully" })
                    {
                        StatusCode = 204
                    };
                }


            }
            catch (Exception)
            {
                return new JsonResult(new { error = "Error Creating data" })
                {
                    StatusCode = 500
                };

            }
        }

        //Get 
        [HttpGet("{id:int}")]
        public async Task<JsonResult> Get(int id)
        {
            try
            {
                var result = await _context.TaskLists.FindAsync(id);

                if (result == null)
                    return new JsonResult(new { error = "List not found" })
                    {
                        StatusCode = 400
                    };

                result.created_at.ToString("yyyy-MM-dd HH:mm:ss");

                var resultDTO = _mapper.Map<GetListDTO>(result);

                return new JsonResult(Ok(resultDTO));
            }
            catch
            {
                return new JsonResult(new { error = "Error Creating data" })
                {
                    StatusCode = 500
                };
            }

        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                //find task to delete by id
                var result = _context.TaskLists.Find(id);

                if (result == null)
                    return new JsonResult(new { error = "List not found" })
                    {
                        StatusCode = 400
                    };

                _context.TaskLists.Remove(result);
                await _context.SaveChangesAsync();

                 return new JsonResult(new { success = "List Deleted Successfully" })
                    {
                        StatusCode = 204
                    };
            }
            catch
            {
                return new JsonResult(new { error = "Error Creating data" })
                {
                    StatusCode = 500
                };
            }

        }

        //Get all
        // [Authorize]
        [HttpGet("/GetAll")]
        public async Task<JsonResult> GetAll(string? searchStatus)
        {
            try
            {
                var results = await _context.TaskLists.ToListAsync();

                if (searchStatus != null)
                {
                    searchStatus.ToLower();
                    string[] statusList = { "pending", "in progress", "completed" };

                    for (int i = 0; i < statusList.Length; i++)
                    {
                        if (String.Equals(searchStatus, statusList[i]))
                        {
                            break;
                        }
                    }

                    results = results.Where(x => x.status == searchStatus).ToList();


                }
                var resultDTO = _mapper.Map<IEnumerable<GetListDTO>>(results);
                return new JsonResult(resultDTO);
            }
            catch (Exception)
            {

                return new JsonResult(new { error = "Error retrieving data" })
                {
                    StatusCode = 500
                };
            }
        }


    }
}