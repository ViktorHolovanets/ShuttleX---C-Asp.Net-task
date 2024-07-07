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
            IEnumerable<TEntity> items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("pagination")]
        public virtual async Task<ActionResult> GetAllAsync([FromQuery] PaginationInfo pagination)
        {
            IEnumerable<TEntity> items = await _service.GetAllAsync(pagination);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetByIdAsync(Guid id)
        {
            TEntity item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public virtual async Task<ActionResult> CreateAsync([FromBody] TEntity entity)
        {
            TEntity createdEntity = await _service.AddAsync(entity);
            return StatusCode(201, createdEntity);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> UpdateAsync(Guid id, [FromBody] TEntity entity)
        {
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
