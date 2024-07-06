using Microsoft.AspNetCore.Mvc;
using ShuttleX_task_api.Models;
using ShuttleX_task_api.Services.Interfaces.DB;

namespace ShuttleX_task_api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : BaseAppController<User>
    {
        public UserController(IBaseService<User> service) : base(service)
        {
        }
    }
}
