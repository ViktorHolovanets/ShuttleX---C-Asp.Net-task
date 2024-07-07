using ShuttleX_task_api.Controllers;
using ShuttleX_task_api.Models;
using ShuttleX_task_api.Services.Interfaces.DB;

namespace UnitTestsController
{
    [TestClass]
    public class UserControllerTests : BaseAppControllerTests<User>
    {
        protected override User CreateTestEntity()
        {
            return new User { Name = "TestUser" };
        }
    }
}
