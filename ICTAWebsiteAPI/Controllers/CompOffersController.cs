using Business.Abstract;
using DTOs.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICTAWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompOffersController : ControllerBase
    {
        private readonly ICompOfferService _compOfferService;

        public CompOffersController(ICompOfferService compOfferService)
        {
            _compOfferService = compOfferService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _compOfferService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _compOfferService.GetByIdAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CompOfferAddDTO compOfferAddDTO)
        {
            var result = await _compOfferService.AddCompOfferAsync(compOfferAddDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addAnonym")]
        public async Task<IActionResult> AddAnoynm([FromForm] CompOfferAnonymAddDTO compOfferAddDTO)
        {
            var result = await _compOfferService.AddCompOfferAnonymAsync(compOfferAddDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
