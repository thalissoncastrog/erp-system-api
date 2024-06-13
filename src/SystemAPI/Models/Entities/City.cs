using System.ComponentModel.DataAnnotations;
using SystemAPI.Models.Entities;
using System.Collections.Generic;

namespace SystemAPI.Models.Entities
{
    public class City
    {
        [Key]
        public int City_Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        public ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}
