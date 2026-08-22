using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.DTOs
{
    public class CreateArtistDto
    {

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Biography { get; set; }

        [Range(1, 2100)]
        public int? BirthYear { get; set; }

        [Range(1, 2100)]
        public int? DeathYear { get; set; }
    }
}
