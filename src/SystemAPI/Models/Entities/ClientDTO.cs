using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SystemAPI.Models.Entities
{
    public class ClientDTO
    { 
        public string Name { get; set; }

        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public int Age { get; set; }

        public int CityId { get; set; }
    }
}
