using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Entitiyes
{
    public class Painting
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public required string Title { get; set; }

        public string? Description { get; set; }

        public string? Story { get; set; } 

        public int? Year { get; set; } 

        [MaxLength(500)]
        public string? ImageUrl { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int Views { get; set; } = 0;

        
        public int ArtistId { get; set; }
        public Artist Artist { get; set; } = null!;

        
        public int EraId { get; set; }
        public Era Era { get; set; } = null!;

        
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Save> Saves { get; set; } = new List<Save>();
    }
}
