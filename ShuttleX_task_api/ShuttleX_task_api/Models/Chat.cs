using My_one_day_life_api.Models.Base;

namespace My_one_day_life_api.Models
{
    public class Chat: BaseEntity
    {       
        public string Name { get; set; }
        public Guid CreatedByUserId { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
