using Business.Abstract;
using Business.Concrete;
using DTOs.Concrete;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICTAWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILdapConnection1 _ldapConnection1;
        private readonly IGraphService _graphService;

        public AuthController(IAuthService authService, ILdapConnection1 ldapConnection1, IGraphService graphService)
        {
            _authService = authService;
            _ldapConnection1 = ldapConnection1;
            _graphService = graphService;
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

            if(!result.Success)
                return BadRequest(result.Message);

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

        [HttpGet("getallfromAD")]
        public async Task<IActionResult> GetAllFromAD()
        {
            var result = await _ldapConnection1.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpPost("/loginAD")]
        public async Task<IActionResult> LoginAD([FromForm] AuthLoginDTO loginDTO)
        {
            if (loginDTO == null)
                return BadRequest("Invalid request data.");

            bool isAuthenticated = await _ldapConnection1.AuthenticateUser(loginDTO.Username, loginDTO.Password);

            if (!isAuthenticated)
                return Unauthorized("LDAP authentication failed.");

            return Ok(isAuthenticated);
        }

        [HttpGet("getMeetingsFromOutlook")]
        public async Task<IActionResult> getMeetingsFromOutlook()
        {
            var meetings = _graphService.GetRoomAppointments2("meetingroom1s@icta.az");
            foreach (var m in meetings)
            {
                Console.WriteLine(
                    m.Title + " " +
                    m.OrganizerId + " " +
                    m.StartDate.ToString("dd.MM.yyyy") + " " +
                    m.StartTime.ToString(@"hh\:mm\:ss") + " " +
                    m.Location + " " +
                    m.Description
                );
            }

            return Ok("result");
        }
    }
}