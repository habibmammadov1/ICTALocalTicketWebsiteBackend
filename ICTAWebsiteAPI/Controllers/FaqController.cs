using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Entities;

namespace ICTAWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

       

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _faqService.GetByIdAsync(id);

            if (result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add(Faq faq)
        {
            await _faqService.AddAsync(faq);
            return Ok(faq);
        }
    }
}

