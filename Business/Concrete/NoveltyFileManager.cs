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
    public class NoveltyFileManager : INoveltyFilesService
    {
        private readonly INoveltyFilesDal _noveltyFilesDal;
        private readonly IMapper _mapper;

        public NoveltyFileManager(INoveltyFilesDal noveltyFilesDal, IMapper mapper)
        {
            _noveltyFilesDal = noveltyFilesDal;
            _mapper = mapper;
        }

        public async Task<IDataResult<NoveltyFilesAddDTO>> AddAsync(NoveltyFilesAddDTO noveltyAddDTO)
        {
            await _noveltyFilesDal.AddAsync(_mapper.Map<NoveltyFile>(noveltyAddDTO));
            return new SuccessDataResult<NoveltyFilesAddDTO>(noveltyAddDTO, "News file added successfully.");
        }

        public async Task<IDataResult<List<NoveltyFile>>> GetAllAsync()
        {
            return new SuccessDataResult<List<NoveltyFile>>(await _noveltyFilesDal.GetAllAsync(), "News files retrieved successfully.");
        }

        public async Task<IDataResult<NoveltyFile>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<NoveltyFile>(await _noveltyFilesDal.GetAsync(nf => nf.Id == id), "News file retrieved successfully.");    
        }

        public async Task<IDataResult<List<NoveltyFile>>> GetByNewsIdAsync(int id)
        {
            return new SuccessDataResult<List<NoveltyFile>>(await _noveltyFilesDal.GetAllAsync(nf => nf.NoveltyItemId == id), "News files for the specified news retrieved successfully.");
        }

        public async Task<IResult> RemoveAsync(int id)
        {
            NoveltyFile newsFiles = await _noveltyFilesDal.GetAsync(nf => nf.Id == id);
            await _noveltyFilesDal.RemoveAsync(newsFiles);
            return new SuccessResult("Deleted");
        }

        public async Task<IDataResult<NoveltyFile>> UpdateAsync(NoveltyFile noveltyUpdateDTO)
        {
            await _noveltyFilesDal.UpdateAsync(noveltyUpdateDTO);
            return new SuccessDataResult<NoveltyFile>(noveltyUpdateDTO, "News file updated successfully."); 
        }
    }
}
