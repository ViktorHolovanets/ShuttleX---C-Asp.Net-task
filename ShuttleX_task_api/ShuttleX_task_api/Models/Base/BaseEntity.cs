using System.ComponentModel.DataAnnotations;

namespace My_one_day_life_api.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
