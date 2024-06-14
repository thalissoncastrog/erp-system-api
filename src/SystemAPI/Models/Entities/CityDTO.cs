using System.ComponentModel.DataAnnotations;

namespace SystemAPI.Models.Entities
{
    public class CityDTO
    {
        [Key]
        public int City_Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }
    }
}
