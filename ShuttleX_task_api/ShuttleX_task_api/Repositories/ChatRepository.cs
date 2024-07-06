using My_one_day_life_api.Models;

namespace My_one_day_life_api.Repositories
{
    public class ChatRepository : BaseRepository<Chat>
    {
        public ChatRepository(AppDb context) : base(context){}
    }
}
