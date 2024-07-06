using My_one_day_life_api.Models.Base;

namespace My_one_day_life_api.Models
{
    public class Message: BaseEntity
    {
        public required string Content { get; set; }
        public Guid ChatId { get; set; }
        public required Chat Chat { get; set; }
        public required Guid UserId { get; set; }
    }
}
