using Microsoft.EntityFrameworkCore;
using My_one_day_life_api.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace My_one_day_life_api.Models
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
    }
}
