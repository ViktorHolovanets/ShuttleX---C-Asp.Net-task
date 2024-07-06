using Microsoft.AspNetCore.Mvc;
using ShuttleX_task_api.Helpers.Classes;
using ShuttleX_task_api.Services.Interfaces.DB;

namespace ShuttleX_task_api.Controllers
{
    [ApiController]
    public abstract class  BaseAppController<TEntity> : ControllerBase
    {
        protected readonly IBaseService<TEntity> _service;
        public BaseAppController(IBaseService<TEntity> service)
        {
            _service = service;
        }

        [HttpGet("pagination")]
        public virtual async Task<ActionResult> GetAllAsync([FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<TEntity> items = await _service.GetAllAsync(pagination);
                return Ok(GetPresentCollection(items));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                TEntity item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(GetPresent(item));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] TEntity entity)
        {
            try
            {
                TEntity createdEntity = await _service.AddAsync(entity);
                return StatusCode(StatusCodes.Status201Created, GetPresent(createdEntity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] TEntity entity)
        {
            try
            {
                await _service.UpdateAsync(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        protected virtual object GetPresent(TEntity entity) => entity;
        protected virtual IEnumerable<object> GetPresentCollection(IEnumerable<TEntity> entity)
        {
            return entity.Select(obj => GetPresent(obj));
        }

    }
}
