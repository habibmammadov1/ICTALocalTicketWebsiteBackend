using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using DTOs.Concrete;
using DTOs.Concrete.Novelty;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NewsManager : INewsService
    {
        private readonly INewsDal _newDal;
        private readonly IMapper _mapper;

        public NewsManager(INewsDal newDal, IMapper mapper)
        {
            _newDal = newDal;
            _mapper = mapper;
        }

        public async Task<IDataResult<NoveltyAddDTO>> AddAsync(NoveltyAddDTO noveltyAddDTO)
        {
            if (noveltyAddDTO == null)
            {
                return new ErrorDataResult<NoveltyAddDTO>("Məlumat tapılmadı.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "news");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string? photoPath = null;

            await _newDal.AddAsync(_mapper.Map<News>(noveltyAddDTO));
            return new SuccessDataResult<NoveltyAddDTO>(noveltyAddDTO);
        }

        public async Task<IDataResult<List<News>>> GetAllAsync()
        {
            return new SuccessDataResult<List<News>>(await _newDal.GetAllAsync());
        }

        public async Task<IDataResult<News>> GetByIdAsync(int id)
        {
            News news =  await _newDal.GetAsync(n => n.Id == id);

            if (news == null)
                return new ErrorDataResult<News>("News not found");

            return new SuccessDataResult<News>(news);
        }

        public async Task<IResult> RemoveAsync(int id)
        {
            var entityForDelete = await GetByIdAsync(id);

            if (entityForDelete.Data == null)
                return new ErrorResult("News not found");

            await _newDal.RemoveAsync(entityForDelete.Data);
            return new SuccessResult("Deleted successfully");
        }

        public async Task<IDataResult<NoveltyUpdateDTO>> UpdateAsync(NoveltyUpdateDTO noveltyUpdateDTO)
        {
            if(noveltyUpdateDTO == null)
                throw new ArgumentNullException(nameof(noveltyUpdateDTO));

            News entityToUpdate = _mapper.Map<News>(noveltyUpdateDTO);
            await _newDal.UpdateAsync(entityToUpdate);
            return new SuccessDataResult<NoveltyUpdateDTO>(noveltyUpdateDTO);

        }
    }
}
