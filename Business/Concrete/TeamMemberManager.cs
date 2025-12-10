using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using Data.Concrete;
using DTOs.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TeamMemberManager : ITeamMemberService
    {
        private readonly ITeamMemberDal _teamMemberDal;
        private readonly IMapper _mapper;

        public TeamMemberManager(ITeamMemberDal teamMemberDal, IMapper mapper)
        {
            _teamMemberDal = teamMemberDal;
            _mapper = mapper;
        }

        public async Task<IDataResult<TeamMembersAddDTO>> AddTeamMemberAsync(TeamMembersAddDTO teamMembersAddDTO)
        {
            if (teamMembersAddDTO == null)
            {
                return new ErrorDataResult<TeamMembersAddDTO>("Məlumat tapılmadı.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string? photoPath = null;

            if (teamMembersAddDTO.Photo != null && teamMembersAddDTO.Photo.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(teamMembersAddDTO.Photo.FileName);
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await teamMembersAddDTO.Photo.CopyToAsync(stream);
                }

                photoPath = "/uploads/" + uniqueFileName;
            }

            if (photoPath == null)
                return new ErrorDataResult<TeamMembersAddDTO>("Şəkil yüklənmədi.");

            else
            {
                TeamMember teamMemberToAdd = _mapper.Map<TeamMember>(teamMembersAddDTO);
                teamMemberToAdd.PhotoPath = photoPath;
                await _teamMemberDal.AddAsync(teamMemberToAdd);
            }
            
            return new SuccessDataResult<TeamMembersAddDTO>(teamMembersAddDTO, "Uğurlu");
        }

        public async Task<IResult> DeleteTeamMemberAsync(int id)
        {
            var entityForDelete = await GetTeamMemberByIdAsync(id);

            if (entityForDelete.Data == null)
                return new ErrorResult("Team member not found");

            var photoPathFromDb = entityForDelete.Data.PhotoPath;
            if (!string.IsNullOrWhiteSpace(photoPathFromDb))
            {
                var fileName = Path.GetFileName(photoPathFromDb);
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }

            await _teamMemberDal.RemoveAsync(entityForDelete.Data);
            return new SuccessResult("Deleted successfully");
        }


        public async Task<IDataResult<List<TeamMember>>> GetAllTeamMembersAsync()
        {
            return new SuccessDataResult<List<TeamMember>>(await _teamMemberDal.GetAllAsync());
        }

        public async Task<IDataResult<TeamMember>> GetTeamMemberByIdAsync(int id)
        {
            var data = await _teamMemberDal.GetAsync(e => e.Id == id);
            return new SuccessDataResult<TeamMember>(data);   
        }

        public async Task<IDataResult<TeamMembersUpdateDTO>> UpdateTeamMemberAsync(TeamMembersUpdateDTO teamMembersUpdateDTO)
        {
            if (teamMembersUpdateDTO == null)
            {
                return new ErrorDataResult<TeamMembersUpdateDTO>("Məlumat tapılmadı.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string? photoPath = null;

            if (teamMembersUpdateDTO.Photo != null && teamMembersUpdateDTO.Photo.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(teamMembersUpdateDTO.Photo.FileName);
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await teamMembersUpdateDTO.Photo.CopyToAsync(stream);
                }

                photoPath = "/uploads/" + uniqueFileName;
            }

            if (photoPath == null)
                return new ErrorDataResult<TeamMembersUpdateDTO>("Şəkil yüklənmədi.");

            else
            {
                TeamMember teamMemberToUpdate = _mapper.Map<TeamMember>(teamMembersUpdateDTO);
                teamMemberToUpdate.PhotoPath = photoPath;
                await _teamMemberDal.UpdateAsync(teamMemberToUpdate);
            }

            return new SuccessDataResult<TeamMembersUpdateDTO>(teamMembersUpdateDTO);
        }
    }
}
