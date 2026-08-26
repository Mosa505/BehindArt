using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Domain.Entitiyes
{
    public class User : IdentityUser<int>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Save> Saves { get; set; } = new List<Save>();
    }
}
