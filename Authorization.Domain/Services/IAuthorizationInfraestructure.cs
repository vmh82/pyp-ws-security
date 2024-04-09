using Authorization.Entity.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Domain.Services
{
    public interface IAuthorizationInfraestructure
    {
        JwtSecurityTokenResponseDto GenerateJwtToken(string roleType);
    }
}
