﻿using System.ComponentModel.DataAnnotations;

namespace SystemAPI.Models.Entities
{
    public class CityDTO
    {
        public int City_Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }
    }
}
