using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Entitiyes
{
    public  class Era
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; } 

        public string? Description { get; set; }

        public int StartYear { get; set; }
        public int EndYear { get; set; }

        public ICollection<Painting> Paintings { get; set; } = new List<Painting>();
    }
}
