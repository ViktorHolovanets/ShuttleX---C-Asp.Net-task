using ShuttleX_task_api.Models.Base;

namespace ShuttleX_task_api.Models
{
    public class Message: BaseEntity
    {
        public required string Content { get; set; }
        public Guid ChatId { get; set; }
        public required Chat Chat { get; set; }
        public required Guid UserId { get; set; }
    }
}
