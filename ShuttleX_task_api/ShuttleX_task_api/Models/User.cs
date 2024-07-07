using Microsoft.EntityFrameworkCore;
using ShuttleX_task_api.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShuttleX_task_api.Models
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<Chat>? CreatedChats { get; set; }
        public ICollection<Message>? CreatedMessages { get; set; }
    }
}
