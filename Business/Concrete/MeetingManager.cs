using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using DTOs.Concrete;
using DTOs.Concrete.Novelty;
using Entities;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MeetingManager : IMeetingService
    {
        private readonly IMeetingDal _meetingDal;
        private readonly IMapper _mapper;

        public MeetingManager(IMeetingDal meetingDal, IMapper mapper)
        {
            _meetingDal = meetingDal;
            _mapper = mapper;
        }

        public async Task<IDataResult<MeetingAddDTO>> AddAsync(MeetingAddDTO meetingAddDTO)
        {
            Meeting meeting = _mapper.Map<Meeting>(meetingAddDTO);

            if (meetingAddDTO == null)
            {
                return new ErrorDataResult<MeetingAddDTO>("Məlumat tapılmadı.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "meetings");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Process photos
            string? photoPath = null;

            if (meetingAddDTO.FormFile != null && meetingAddDTO.FormFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(meetingAddDTO.FormFile.FileName);
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await meetingAddDTO.FormFile.CopyToAsync(stream);
                }

                photoPath = "/uploads/meetings/" + uniqueFileName;
            }

            if (photoPath == null)
                return new ErrorDataResult<MeetingAddDTO>("Şəkil yüklənmədi.");

            else
            {
                meeting.FilePath = photoPath;
            }

            await _meetingDal.AddAsync(meeting);
            return new SuccessDataResult<MeetingAddDTO>(meetingAddDTO);
        }

        public async Task<IDataResult<List<Meeting>>> GetAllAsync()
        {
            return new SuccessDataResult<List<Meeting>>(await _meetingDal.GetAllAsync());
        }

        public async Task<IDataResult<Meeting>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<Meeting>(await _meetingDal.GetAsync(m => m.Id == id));
        }

        public async Task<IResult> RemoveAsync(int id)
        {
            Meeting meeting = await _meetingDal.GetAsync(m => m.Id == id);

            if (meeting == null)
            {
                return new ErrorResult("Məlumat tapılmadı.");
            }

            await _meetingDal.RemoveAsync(meeting);
            return new SuccessResult("Uğurla silindi");
        }

        public async Task<IDataResult<MeetingUpdateDTO>> UpdateAsync(MeetingUpdateDTO meetingUpdateDTO)
        {
            Meeting meeting = _mapper.Map<Meeting>(meetingUpdateDTO);

            if (meetingUpdateDTO == null)
            {
                return new ErrorDataResult<MeetingUpdateDTO>("Məlumat tapılmadı.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "meetings");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Process photos
            string? photoPath = null;

            if (meetingUpdateDTO.FormFile != null && meetingUpdateDTO.FormFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(meetingUpdateDTO.FormFile.FileName);
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await meetingUpdateDTO.FormFile.CopyToAsync(stream);
                }

                photoPath = "/uploads/meetings/" + uniqueFileName;
            }

            if (photoPath == null)
                return new ErrorDataResult<MeetingUpdateDTO>("Şəkil yüklənmədi.");

            else
            {
                meeting.FilePath = photoPath;
            }

            await _meetingDal.UpdateAsync(meeting);
            return new SuccessDataResult<MeetingUpdateDTO>(meetingUpdateDTO);
        }
    }
}
