using Microsoft.AspNetCore.Mvc;
using ShuttleX_task_api.Helpers.Classes;
using ShuttleX_task_api.Services.Interfaces.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShuttleX_task_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseAppController<TEntity> : ControllerBase
    {
        protected readonly IBaseService<TEntity> _service;

        public BaseAppController(IBaseService<TEntity> service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public virtual async Task<ActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<TEntity> items = await _service.GetAllAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving all items."); // 500 Internal Server Error
            }
        }

        [HttpGet("pagination")]
        public virtual async Task<ActionResult> GetAllAsync([FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<TEntity> items = await _service.GetAllAsync(pagination);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving items with pagination."); // 500 Internal Server Error
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                TEntity item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound(); // 404 Not Found
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving item by id."); // 500 Internal Server Error
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> CreateAsync([FromBody] TEntity entity)
        {
            try
            {
                TEntity createdEntity = await _service.AddAsync(entity);
                return StatusCode(201, createdEntity); // 201 Created
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating entity."); // 500 Internal Server Error
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> UpdateAsync(Guid id, [FromBody] TEntity entity)
        {
            try
            {
                await _service.UpdateAsync(entity);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating entity."); // 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error deleting entity."); // 500 Internal Server Error
            }
        }
    }
}
