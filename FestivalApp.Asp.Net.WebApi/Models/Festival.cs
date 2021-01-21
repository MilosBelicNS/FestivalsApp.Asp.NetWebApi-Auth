using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestivalApp.Asp.Net.WebApi.Models
{
    public class Festival
    {

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
       [Range(1, 999999)]
        public decimal TicketPrice { get; set; }

        [Required]
        [Range(1951, 2021)]
        public int FirstYear { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}