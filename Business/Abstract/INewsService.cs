using Core.Utilities.Results;
using DTOs.Concrete.Novelty;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INewsService
    {
        Task<IDataResult<List<News>>> GetAllAsync();
        Task<IDataResult<News>> GetByIdAsync(int id);
        Task<IDataResult<NoveltyAddDTO>> AddAsync(NoveltyAddDTO noveltyAddDTO);
        Task<IResult> RemoveAsync(int id);
    }
}
