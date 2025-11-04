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
    public interface INoveltyFilesService
    {
        Task<IDataResult<List<NoveltyFile>>> GetAllAsync();
        Task<IDataResult<List<NoveltyFile>>> GetByNewsIdAsync(int id);
        Task<IDataResult<NoveltyFile>> GetByIdAsync(int id);
        Task<IDataResult<NoveltyFilesAddDTO>> AddAsync(NoveltyFilesAddDTO noveltyAddDTO);
        Task<IResult> RemoveAsync(int id);
        Task<IDataResult<NoveltyFile>> UpdateAsync(NoveltyFile noveltyUpdateDTO);
    }
}
