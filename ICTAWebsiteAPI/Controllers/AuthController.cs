using Business.Abstract;
using DTOs.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICTAWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("/register")]
        public IActionResult registerAuth([FromBody] AuthRegisterDTO authRegisterDTO)
        {
            if (authRegisterDTO == null)
                return BadRequest("Invalid request data.");

            var result = _authService.registerAuth(authRegisterDTO);
            return Ok(result.Data);
        }

        [HttpPost("/login")]
        public IActionResult loginAuth([FromBody] AuthLoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest("Invalid request data.");

            var result = _authService.loginAuth2(loginDTO);

            return Ok(result.Data);
        }

        [HttpGet("verify-email")]
        public IActionResult VerifyEmail([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token tapılmadı.");

            var result = _authService.verifyEmail(token);
            return Ok(result.Message);
        }
    }
}