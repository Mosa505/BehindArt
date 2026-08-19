using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Entitiyes
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Username { get; set; }

        [MaxLength(256)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Save> Saves { get; set; } = new List<Save>();
    }
}
