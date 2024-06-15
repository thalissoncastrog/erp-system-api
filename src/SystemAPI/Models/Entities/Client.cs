using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemAPI.Models.Entities;
using Newtonsoft.Json;

namespace SystemAPI.Models.Entities
{
    public class Client
    {
        [Column("client_id")]
        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        public int Age { get; set; }

        [Column("city_id")]
        public int CityId { get; set; }

        public City? City { get; set; }
    }
}
