using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemAPI.Models.Entities;
using Newtonsoft.Json;

namespace SystemAPI.Models.Entities
{
    public class Client
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

        [ForeignKey("CityId")]
        public City? City { get; set; }
    }
}
