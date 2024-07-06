using Microsoft.AspNetCore.Mvc;
using ShuttleX_task_api.Models;
using ShuttleX_task_api.Services.Interfaces.DB;

namespace ShuttleX_task_api.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    public class UserController : BaseAppController<Message>
    {
        public UserController(IBaseService<Message> service) : base(service)
        {
        }
    }
}
