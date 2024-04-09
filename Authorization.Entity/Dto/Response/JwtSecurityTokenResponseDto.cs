using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Entity.Dto.Response
{
    public class JwtSecurityTokenResponseDto
    {
        public string Token { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime Expires { get; set; }
        public string TokenType { get; set; }
    }
}
