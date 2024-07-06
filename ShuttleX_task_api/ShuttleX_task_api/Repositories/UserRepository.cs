using ShuttleX_task_api.Models;

namespace ShuttleX_task_api.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(AppDb context) : base(context)
        {
            // 
        }
    }
}
