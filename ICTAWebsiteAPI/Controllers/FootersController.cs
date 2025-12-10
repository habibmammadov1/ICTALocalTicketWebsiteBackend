using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICTAWebsiteAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FootersController : ControllerBase
    {
        private readonly IFooterService _footerService;

        public FootersController(IFooterService footerService)
        {
            _footerService = footerService;
        }

        [HttpGet("getFooter")]
        public IActionResult GetFooter()
        {
            var result = _footerService.getFooter();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("updateFooter")]
        public IActionResult UpdateFooter(Footer footer)
        {
            var result = _footerService.FooterUpdate(footer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
