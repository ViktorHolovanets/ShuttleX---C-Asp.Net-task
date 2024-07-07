using Microsoft.AspNetCore.Mvc;
using ShuttleX_task_api.Helpers.Classes;
using ShuttleX_task_api.Models;
using ShuttleX_task_api.Services.Interfaces.DB;

namespace ShuttleX_task_api.Controllers
{
    [ApiController]
    [Route("api/chats")]
    public class ChatController : BaseAppController<Chat>
    {

        public ChatController(IBaseService<Chat> chatService):base(chatService)
        {
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> DeleteAsync(Guid id)
        {
            return BadRequest("This method is not available.");
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteAsyncWithRequest(Guid id, [FromBody] DeleteChatRequest request)
        {
            var chat = await _service.GetByIdAsync(id);
            if (chat == null || chat.CreatedByUserId != request.UserId)
            {
                return Forbid("There are no permissions to do the operation.");
            }
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
