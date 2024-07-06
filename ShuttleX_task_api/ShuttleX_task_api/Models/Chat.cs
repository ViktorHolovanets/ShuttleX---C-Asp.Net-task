using ShuttleX_task_api.Models.Base;

namespace ShuttleX_task_api.Models
{
    public class Chat: BaseEntity
    {       
        public string Name { get; set; }
        public Guid CreatedByUserId { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
