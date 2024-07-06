using ShuttleX_task_api.Models;

namespace ShuttleX_task_api.Repositories
{
    public class ChatRepository : BaseRepository<Chat>
    {
        public ChatRepository(AppDb context) : base(context){}
    }
}
