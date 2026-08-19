using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Entitiyes
{
    public class Artist
    {
        public int Id { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; }

        public string? Biography { get; set; }

        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }

        public ICollection<Painting> Paintings { get; set; } = new List<Painting>();
    }
}

