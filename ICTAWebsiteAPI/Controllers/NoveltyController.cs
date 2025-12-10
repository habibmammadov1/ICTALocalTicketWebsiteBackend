using Business.Abstract;
using Core.Utilities.Filters;
using DTOs.Concrete.Novelty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ICTAWebsiteAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoveltyController : ControllerBase
    {
        private readonly INoveltyService _noveltyService;

        public NoveltyController(INoveltyService noveltyService)
        {
            _noveltyService = noveltyService;
        }

        [HttpPost("add")]
        [ServiceFilter(typeof(SetAuthorFilter))]
        public async Task<IActionResult> AddAsync([FromForm] NoveltyAddDTO noveltyAddDTO)
        {
            var result = await _noveltyService.AddAsync(noveltyAddDTO);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _noveltyService.GetAllAsync();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _noveltyService.GetByIdAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var result = await _noveltyService.RemoveAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [ServiceFilter(typeof(SetAuthorFilter))]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromForm] NoveltyUpdateDTO noveltyUpdateDTO)
        {
            var result = await _noveltyService.UpdateAsync(noveltyUpdateDTO);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        //[HttpPost("upload-image")]

        [HttpPost("like/{id}/{likeActiveDeactive}")]
        public async Task<IActionResult> LikeActiveDeactiveAsync(int id, int likeActiveDeactive)
        {
            string ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                ?? HttpContext.Connection.RemoteIpAddress?.ToString(); // which ip likes the novelty

            if (ip == null)
                return BadRequest("Error");

            var result = await _noveltyService.LikeActiveDeactiveAsync(id, likeActiveDeactive, ip);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("dislike/{id}/{dislikeActiveDeactive}")]
        public async Task<IActionResult> DislikeActiveDeactiveAsync(int id, int dislikeActiveDeactive)
        {
            string ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                ?? HttpContext.Connection.RemoteIpAddress?.ToString(); // which ip likes the novelty

            if (ip == null)
                return BadRequest("Error");

            var result = await _noveltyService.DislikeActiveDeactiveAsync(id, dislikeActiveDeactive, ip);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbynoveltyid/{noveltyId}")]
        public async Task<IActionResult> GetByNoveltyIdAsync(int noveltyId)
        {
            var result = await _noveltyService.GetByNoveltyIdAsync(noveltyId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("activedeactive/{id}/{status}")]
        public async Task<IActionResult> ActiveDeactiveAsync(int id, int status)
        {
            var result = await _noveltyService.ActiveDeactiveAsync(id, status);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
