using ShuttleX_task_api.Models.Base;

namespace ShuttleX_task_api.Models
{
    public class Message : BaseEntity
    {
        public required string Content { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public required Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
    }
}
