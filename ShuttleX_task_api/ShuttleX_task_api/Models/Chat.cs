using ShuttleX_task_api.Models.Base;

namespace ShuttleX_task_api.Models
{
    public class Chat : BaseEntity
    {
        public required string Name { get; set; }
        public required Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public ICollection<Message> Messages { get; set; }
    }

}
