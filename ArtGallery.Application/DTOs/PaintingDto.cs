using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.DTOs
{
    public class PaintingDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? Year { get; set; }
        public string ArtistName { get; set; } = string.Empty;
        public string EraName { get; set; } = string.Empty;
    }
}
