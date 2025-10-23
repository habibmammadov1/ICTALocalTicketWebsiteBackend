using Business.Abstract;
using Core.Utilities.Filters;
using DTOs.Concrete.Novelty;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICTAWebsiteAPI.Controllers
{
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
            var result = await _newsService.Add(noveltyAddDTO);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
