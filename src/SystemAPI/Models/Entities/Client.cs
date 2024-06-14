using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemAPI.Models.Entities;
using Newtonsoft.Json;

namespace SystemAPI.Models.Entities
{
    public class Client
    {
        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age { get; set; }

        public int CityId { get; set; }

        public City? City { get; set; }
    }
}
