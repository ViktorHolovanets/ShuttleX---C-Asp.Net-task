using ShuttleX_task_api.Models;

namespace ShuttleX_task_api.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(AppDb context) : base(context){}
    }
}
