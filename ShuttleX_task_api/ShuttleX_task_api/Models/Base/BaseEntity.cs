using System.ComponentModel.DataAnnotations;

namespace ShuttleX_task_api.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
