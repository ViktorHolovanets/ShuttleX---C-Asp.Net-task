using My_one_day_life_api.Models;

namespace My_one_day_life_api.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(AppDb context) : base(context){}
    }
}
