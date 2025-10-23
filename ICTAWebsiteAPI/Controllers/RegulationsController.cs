using Business.Abstract;
using DTOs.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICTAWebsiteAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegulationsController : ControllerBase
    {
        private readonly IRegulationsService _regulationsService;
        private readonly IWebHostEnvironment _env;
        public RegulationsController(IRegulationsService regulationsService, IWebHostEnvironment env)
        {
            _regulationsService = regulationsService;
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UpdateAsync([FromForm] RegulationsAddDTO regulationsAddDTO)
        {
            var result = await _regulationsService.updateRegulationsAsync(regulationsAddDTO);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getRegulations")]
        public async Task<IActionResult> GetRegulations(int id)
        {
            string rootPath = _env.WebRootPath;
            var result = await _regulationsService.getRegulationsAsync(id, rootPath);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
