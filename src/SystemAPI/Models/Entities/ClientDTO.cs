using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SystemAPI.Models.Entities
{
    public class ClientDTO
    {
        [Key]
        [Column("client_id")]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1)]
        public string Gender { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public int Age { get; set; }

        [Column("city_id")]
        public int CityId { get; set; }
    }
}
