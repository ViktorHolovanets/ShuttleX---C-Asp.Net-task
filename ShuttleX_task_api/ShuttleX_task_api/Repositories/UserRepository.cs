using My_one_day_life_api.Models;

namespace My_one_day_life_api.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(AppDb context) : base(context)
        {
        }
    }
}
