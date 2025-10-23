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
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMembersController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        [HttpGet("getAllTeamMembers")]
        public async Task<IActionResult> GetAll() { 
            var result = await _teamMemberService.GetAllTeamMembersAsync();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getTeamMemberById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _teamMemberService.GetTeamMemberByIdAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addTeamMember")]
        public async Task<IActionResult> Add([FromForm] TeamMembersAddDTO teamMembersAddDTO)
        {
            var result = await _teamMemberService.AddTeamMemberAsync(teamMembersAddDTO);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("updateTeamMember")]
        public async Task<IActionResult> Update([FromForm] TeamMembersUpdateDTO teamMembersUpdateDTO)
        {
            var result = await _teamMemberService.UpdateTeamMemberAsync(teamMembersUpdateDTO);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("deleteTeamMember")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _teamMemberService.DeleteTeamMemberAsync(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
