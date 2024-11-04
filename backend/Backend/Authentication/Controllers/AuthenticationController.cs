using API.Authentication.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(Services.IAuthenticationService _authenticationService) : ControllerBase
    {
        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody] Models.SignInRequest request)
        {
            try
            {
                var response = _authenticationService.Authenticate(request.Email, request.Password);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password.");
            }
        }


        [HttpPost("sign-up")]
        public IActionResult SingUp([FromBody] Models.User user)
        {
            try
            {
                _authenticationService.SignUp(user.Email, user.PasswordHash);
                var uri = Url.Action("GetUser", new { email = user.Email });

                return Created(uri, user);
            }
            catch (UserAlreadyExistsException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}