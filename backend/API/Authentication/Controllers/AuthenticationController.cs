using API.Authentication.Exceptions;
using API.Authentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(Services.IAuthenticationService _authenticationService) : ControllerBase
    {
        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody] SignInRequest user)
        {
            try
            {
                var response = _authenticationService.Authenticate(user.UserEmail, user.PasswordHash);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password.");
            }
        }


        [HttpPost("sign-up")]
        public IActionResult SingUp([FromBody] SignInRequest user)
        {
            try
            {
                _authenticationService.SignUp(user.UserEmail, user.PasswordHash);
                var uri = Url.Action("GetUser", new { userEmail = user.UserEmail });

                return Created(uri, user);
            }
            catch (UserAlreadyExistsException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}