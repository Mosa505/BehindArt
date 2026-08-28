using BehindArt.Domain.Entitiyes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehindArt.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user, IList<string> roles);
    }
}
