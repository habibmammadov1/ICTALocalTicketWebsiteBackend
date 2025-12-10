using Core.Utilities.Results;
using DTOs.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITeamMemberService
    {
        Task<IDataResult<List<TeamMember>>> GetAllTeamMembersAsync();
        Task<IDataResult<TeamMember>> GetTeamMemberByIdAsync(int id);
        Task<IDataResult<TeamMembersAddDTO>> AddTeamMemberAsync(TeamMembersAddDTO teamMembersAddDTO);
        Task<IDataResult<TeamMembersUpdateDTO>> UpdateTeamMemberAsync(TeamMembersUpdateDTO teamMembersUpdateDTO);
        Task<IResult> DeleteTeamMemberAsync(int id);
    }
}
