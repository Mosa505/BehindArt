using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.DTOs
{
    public class CreatePaintingDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Story { get; set; }

        [Range(1, 2100)]
        public int? Year { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [Required]
        public int EraId { get; set; }



    }
}
