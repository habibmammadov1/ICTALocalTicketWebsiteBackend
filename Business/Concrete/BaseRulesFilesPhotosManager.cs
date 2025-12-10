using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using Data.Concrete;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BaseRulesFilesPhotosManager : IBaseRulesFilesPhotosService
    {
        private readonly IBaseRulesFilesPhotosDal _baseRulesFilesPhotosDal;
        public BaseRulesFilesPhotosManager(IBaseRulesFilesPhotosDal baseRulesFilesPhotosDal)
        {
            _baseRulesFilesPhotosDal = baseRulesFilesPhotosDal;
        }

        public async Task<IDataResult<BaseRulesFilesPhotos>> AddAsync(BaseRulesFilesPhotos baseRulesFilesPhotos)
        {
            if (baseRulesFilesPhotos == null)
                return new ErrorDataResult<BaseRulesFilesPhotos>("BaseRulesFilesPhotos object cannot be null.");

            await _baseRulesFilesPhotosDal.AddAsync(baseRulesFilesPhotos);
            return new SuccessDataResult<BaseRulesFilesPhotos>(baseRulesFilesPhotos);
        }

        public Task<IDataResult<List<BaseRulesFilesPhotos>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<List<BaseRulesFilesPhotos>>> GetBaseRulePhotosIsTempTrueAsync()
        {
            return new SuccessDataResult<List<BaseRulesFilesPhotos>>(
                await _baseRulesFilesPhotosDal.GetAllAsync(brfp => brfp.IsTemporary == true && brfp.FileOrPhoto == 2)
            );
        }

        public Task<IDataResult<List<BaseRulesFilesPhotos>>> GetByBaseRuleIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<BaseRulesFilesPhotos>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<BaseRulesFilesPhotos>(await _baseRulesFilesPhotosDal.GetAsync(brfp => brfp.Id == id));
        }

        public Task<IDataResult<List<BaseRulesFilesPhotos>>> GetByNewsIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> RemoveAllAsync(int id)
        {
            List<BaseRulesFilesPhotos> baseRulesFilesPhotos = _baseRulesFilesPhotosDal.GetAll(brfp => brfp.RuleItemId == id);
            if (baseRulesFilesPhotos != null || baseRulesFilesPhotos.Any())
            {
                await _baseRulesFilesPhotosDal.RemoveAsync(baseRulesFilesPhotos);
            }

            return new SuccessResult("All related BaseRulesFilesPhotos removed successfully.");
        }

        public async Task<IResult> RemoveAsync(int id)
        {
            BaseRulesFilesPhotos deletedEntity = GetByIdAsync(id).Result.Data;

            if (deletedEntity == null) { 
                return new ErrorResult("BaseRulesFilesPhotos not found.");
            }

            await _baseRulesFilesPhotosDal.RemoveAsync(deletedEntity);
            return new SuccessResult("BaseRulesFilesPhotos removed successfully.");
        }

        public async Task<IDataResult<BaseRulesFilesPhotos>> UpdateAsync(BaseRulesFilesPhotos baseRulesFilesPhotos)
        {
            if (baseRulesFilesPhotos == null)
                return new ErrorDataResult<BaseRulesFilesPhotos>("BaseRulesFilesPhotos object cannot be null.");

            await _baseRulesFilesPhotosDal.UpdateAsync(baseRulesFilesPhotos);
            return new SuccessDataResult<BaseRulesFilesPhotos>(baseRulesFilesPhotos);
        }
    }
}
