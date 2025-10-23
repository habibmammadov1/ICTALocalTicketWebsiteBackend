using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
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
            await _newDal.AddAsync(_mapper.Map<News>(noveltyAddDTO));
            return new SuccessDataResult<NoveltyAddDTO>(noveltyAddDTO);
        }

        public async Task<IDataResult<List<News>>> GetAllAsync()
        {
            return new SuccessDataResult<List<News>>(await _newDal.GetAllAsync());
        }

        public async Task<IDataResult<News>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<News>(await _newDal.GetAsync(n => n.Id == id));
        }

        public Task<IResult> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
