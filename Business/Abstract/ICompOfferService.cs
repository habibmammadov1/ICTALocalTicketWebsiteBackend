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
    public interface ICompOfferService
    {
        Task<IDataResult<CompOfferAddDTO>> AddCompOfferAsync(CompOfferAddDTO compOfferAddDTO);
        Task<IDataResult<CompOfferAnonymAddDTO>> AddCompOfferAnonymAsync(CompOfferAnonymAddDTO compOfferAddDTO);
        Task<IDataResult<CompOffer>> GetByIdAsync(int id);
        Task<IDataResult<List<CompOffer>>> GetAllAsync();
        Task<IResult> RemoveAsync(int id);
    }
}
