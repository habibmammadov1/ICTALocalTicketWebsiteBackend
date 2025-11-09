using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
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

        public Task<IDataResult<BaseRulesFilesPhotos>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<List<BaseRulesFilesPhotos>>> GetByNewsIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<BaseRulesFilesPhotos>> UpdateAsync(BaseRulesFilesPhotos baseRulesFilesPhotos)
        {
            throw new NotImplementedException();
        }
    }
}
