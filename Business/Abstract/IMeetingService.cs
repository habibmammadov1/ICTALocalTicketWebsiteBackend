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
    public interface IMeetingService
    {
        Task<IDataResult<MeetingAddDTO>> AddAsync(MeetingAddDTO meetingAddDTO);
        Task<IDataResult<MeetingUpdateDTO>> UpdateAsync(MeetingUpdateDTO meetingUpdateDTO);
        Task<IDataResult<List<Meeting>>> GetAllAsync();
        Task<IDataResult<Meeting>> GetByIdAsync(int id);
        Task<IResult> RemoveAsync(int id);
    }
}
