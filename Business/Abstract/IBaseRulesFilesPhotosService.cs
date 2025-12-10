using Core.Utilities.Results;
using DTOs.Concrete;
using Entities;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBaseRulesFilesPhotosService
    {
        Task<IDataResult<List<BaseRulesFilesPhotos>>> GetAllAsync();
        Task<IDataResult<List<BaseRulesFilesPhotos>>> GetByBaseRuleIdAsync(int id);
        Task<IDataResult<BaseRulesFilesPhotos>> GetByIdAsync(int id);
        Task<IDataResult<List<BaseRulesFilesPhotos>>> GetBaseRulePhotosIsTempTrueAsync();
        Task<IDataResult<BaseRulesFilesPhotos>> AddAsync(BaseRulesFilesPhotos baseRulesFilesPhotos);
        Task<IResult> RemoveAsync(int id);
        Task<IResult> RemoveAllAsync(int id);
        Task<IDataResult<BaseRulesFilesPhotos>> UpdateAsync(BaseRulesFilesPhotos baseRulesFilesPhotos);
    }
}
