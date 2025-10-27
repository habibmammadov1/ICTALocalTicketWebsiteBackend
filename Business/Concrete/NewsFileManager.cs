using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using DTOs.Concrete;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NewsFileManager : INewsFilesService
    {
        private readonly INewsFilesDal _newsFilesDal;
        private readonly IMapper _mapper;

        public NewsFileManager(INewsFilesDal newsFilesDal, IMapper mapper)
        {
            _newsFilesDal = newsFilesDal;
            _mapper = mapper;
        }

        public async Task<IDataResult<NewsFilesAddDTO>> AddAsync(NewsFilesAddDTO noveltyAddDTO)
        {
            await _newsFilesDal.AddAsync(_mapper.Map<NewsFiles>(noveltyAddDTO));
            return new SuccessDataResult<NewsFilesAddDTO>(noveltyAddDTO, "News file added successfully.");
        }

        public async Task<IDataResult<List<NewsFiles>>> GetAllAsync()
        {
            return new SuccessDataResult<List<NewsFiles>>(await _newsFilesDal.GetAllAsync(), "News files retrieved successfully.");
        }

        public async Task<IDataResult<NewsFiles>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<NewsFiles>(await _newsFilesDal.GetAsync(nf => nf.Id == id), "News file retrieved successfully.");    
        }

        public async Task<IDataResult<List<NewsFiles>>> GetByNewsIdAsync(int id)
        {
            return new SuccessDataResult<List<NewsFiles>>(await _newsFilesDal.GetAllAsync(nf => nf.NewsId == id), "News files for the specified news retrieved successfully.");
        }

        public async Task<IResult> RemoveAsync(int id)
        {
            NewsFiles newsFiles = await _newsFilesDal.GetAsync(nf => nf.Id == id);
            await _newsFilesDal.RemoveAsync(newsFiles);
            return new SuccessResult("Deleted");
        }

        public async Task<IDataResult<NewsFiles>> UpdateAsync(NewsFiles noveltyUpdateDTO)
        {
            await _newsFilesDal.UpdateAsync(noveltyUpdateDTO);
            return new SuccessDataResult<NewsFiles>(noveltyUpdateDTO, "News file updated successfully."); 
        }
    }
}
