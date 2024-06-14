using System.ComponentModel.DataAnnotations;
using SystemAPI.Models.Entities;
using System.Collections.Generic;

namespace SystemAPI.Models.Entities
{
    public class City
    {
        public int City_Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}
