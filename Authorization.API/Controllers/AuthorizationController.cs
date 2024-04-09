using Authorization.Domain.Services;
using Authorization.Entity.Dto.Response;
using Authorization.Infraestructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        private readonly IAuthorizationInfraestructure _authService;
        public AuthorizationController(IAuthorizationInfraestructure authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Get(string roleType)
        {
            if(!roleType.Equals("admin")  && !roleType.Equals("employee"))
            {
                throw new RoleNotFoundException(" Role not found");
            }
            JwtSecurityTokenResponseDto token =  _authService.GenerateJwtToken(roleType);
            return Ok(token);
        }
      


    }
}
