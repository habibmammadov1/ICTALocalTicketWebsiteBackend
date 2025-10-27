using Core.Utilities.Results;
using DTOs.Concrete;
using DTOs.Concrete.Novelty;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INewsFilesService
    {
        Task<IDataResult<List<NewsFiles>>> GetAllAsync();
        Task<IDataResult<List<NewsFiles>>> GetByNewsIdAsync(int id);
        Task<IDataResult<NewsFiles>> GetByIdAsync(int id);
        Task<IDataResult<NewsFilesAddDTO>> AddAsync(NewsFilesAddDTO noveltyAddDTO);
        Task<IResult> RemoveAsync(int id);
        Task<IDataResult<NewsFiles>> UpdateAsync(NewsFiles noveltyUpdateDTO);
    }
}
