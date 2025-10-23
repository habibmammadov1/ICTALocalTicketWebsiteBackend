using Business.Abstract;
using Core.Utilities.Filters;
using DTOs.Concrete.Novelty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICTAWebsiteAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost("add")]
        [ServiceFilter(typeof(SetAuthorFilter))]
        public async Task<IActionResult> AddAsync([FromForm] NoveltyAddDTO noveltyAddDTO)
        {
            var result = await _newsService.AddAsync(noveltyAddDTO);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _newsService.GetAllAsync();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _newsService.GetByIdAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var result = await _newsService.RemoveAsync(id);

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
            var result = await _newsService.UpdateAsync(noveltyUpdateDTO);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        //[HttpPost("upload-image")]
    }
}
