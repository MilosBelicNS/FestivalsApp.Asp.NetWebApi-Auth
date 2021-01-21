using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestivalApp.Asp.Net.WebApi.Models
{
    public class Place
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength =2)]
        public string Name { get; set; }

        [Required]
        [Range(1, 99999)]
        public int Zip { get; set; }
    }
}