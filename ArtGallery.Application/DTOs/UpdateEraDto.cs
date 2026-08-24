using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.DTOs
{
    public class UpdateEraDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public int StartYear { get; set; } 

        [Required]
        public int EndYear { get; set; }


    }
}
