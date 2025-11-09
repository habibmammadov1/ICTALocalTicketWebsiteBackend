using Business.Abstract;
using DTOs.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ICTAWebsiteAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseRulesController : ControllerBase
    {
        private readonly IBaseRulesService _baseRulesService;

        public BaseRulesController(IBaseRulesService baseRulesService)
        {
            _baseRulesService = baseRulesService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBaseRule([FromForm] BaseRulesAddDTO baseRulesAddDTO)
        {
            string? idString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (idString == null) {
                return Unauthorized();
            }

            int userId = int.Parse(idString);

            var result = await _baseRulesService.AddBaseRuleAsync(baseRulesAddDTO, userId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllBaseRules()
        {
            var result = await _baseRulesService.GetAllBaseRulesAsync();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetBaseRuleById(int id)
        {
            var result = await _baseRulesService.GetBaseRuleByIdAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBaseRule(int id)
        {
            var result = await _baseRulesService.DeleteBaseRuleAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBaseRule([FromForm] BaseRulesUpdateDTO baseRulesUpdateDTO)
        {
            string? idString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (idString == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(idString);

            var result = await _baseRulesService.UpdateBaseRuleAsync(baseRulesUpdateDTO, userId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("activedeactive/{id}/{activeDeactive}")]
        public async Task<IActionResult> ActiveDeactiveRule(int id, int activeDeactive)
        {
            var result = await _baseRulesService.ActiveDeactiveRuleAsync(id, activeDeactive);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbybaseruleid/{id}")]
        public async Task<IActionResult> GetBaseRuleByBaseRuleId(int id)
        {
            var result = await _baseRulesService.GetBaseRuleByBaseRuleIdAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
