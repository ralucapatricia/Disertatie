using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<TokenGenerator> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new TokenGenerator
            {
            })
            .ToArray();
        }

        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Email))
                return BadRequest("Email is required.");

            var tokenGenerator = new TokenGenerator();
            var token = tokenGenerator.GenerateToken(loginRequest.Email);

            return Ok(new { token });
        }

    }
}
