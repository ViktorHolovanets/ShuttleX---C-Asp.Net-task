using Microsoft.AspNetCore.Mvc;
using My_one_day_life_api.Models;
using My_one_day_life_api.Services.Interfaces.DB;

namespace My_one_day_life_api.Controllers
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
