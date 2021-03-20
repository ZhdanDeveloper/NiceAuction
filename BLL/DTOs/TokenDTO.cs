using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
