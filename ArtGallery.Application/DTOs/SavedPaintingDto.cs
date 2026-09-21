using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.DTOs
{
    public class SavedPaintingDto
    {
        public int PaintingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string ArtistName { get; set; } = string.Empty;
        public string EraName { get; set; } = string.Empty;
        public DateTime SavedAt { get; set; }
    }
}
