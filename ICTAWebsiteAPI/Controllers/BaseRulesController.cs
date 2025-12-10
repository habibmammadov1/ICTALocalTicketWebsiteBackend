using Business.Abstract;
using Data;
using DTOs.Concrete;
using Entities.Novelty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ICTAWebsiteAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseRulesController : ControllerBase
    {
        private readonly IBaseRulesService _baseRulesService;
        private readonly IBaseRulesFilesPhotosService _baseRulesFilesPhotosService;

        public BaseRulesController(IBaseRulesService baseRulesService, IBaseRulesFilesPhotosService baseRulesFilesPhotosService)
        {
            _baseRulesService = baseRulesService;
            _baseRulesFilesPhotosService = baseRulesFilesPhotosService;
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

            foreach (var path in result.Data.UnusedImagePaths)
            {
                DeleteImageFromDisk(path);
            }

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

        [HttpPost("ckeditor")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadForCk([FromForm] BaseRulesSinglePhotoAddDTO baseRulesSinglePhotoAddDTO, [FromForm] int baseRuleItemId)
        {
            if (baseRulesSinglePhotoAddDTO.Photo == null || baseRulesSinglePhotoAddDTO.Photo.Length == 0)
                return BadRequest("No file.");

            var now = DateTime.UtcNow;
            var uploadRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "baseRules");

            if (!Directory.Exists(uploadRoot))
                Directory.CreateDirectory(uploadRoot);

            var safeName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                           + Path.GetExtension(baseRulesSinglePhotoAddDTO.Photo.FileName).ToLowerInvariant();

            var fullPath = Path.Combine(uploadRoot, safeName);

            await using (var stream = System.IO.File.Create(fullPath))
            {
                await baseRulesSinglePhotoAddDTO.Photo.CopyToAsync(stream);
            }

            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var publicUrl = $"{baseUrl}/uploads/baseRules/{safeName}";

            var img = new BaseRulesFilesPhotos
            {
                FilePath = publicUrl,
                FileOrPhoto = 2, // photo   
                RuleItemId = baseRuleItemId,
                IsTemporary = true
            };

            await _baseRulesFilesPhotosService.AddAsync(img);

            return Ok(new { url = publicUrl });
        }

        private void DeleteImageFromDisk(string relativePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "baseRules", relativePath.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }

    }
}
