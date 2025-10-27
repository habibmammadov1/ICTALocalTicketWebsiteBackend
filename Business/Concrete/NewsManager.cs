using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using DTOs.Concrete;
using DTOs.Concrete.Novelty;
using Entities.Novelty;
//using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class NewsManager : INewsService
    {
        private readonly INewsDal _newsDal;
        private readonly IMapper _mapper;
        private readonly INewsFilesService _newsFilesService;

        public NewsManager(INewsDal newsDal, IMapper mapper, INewsFilesService newsFilesService)
        {
            _newsDal = newsDal;
            _mapper = mapper;
            _newsFilesService = newsFilesService;
        }

        public async Task<IDataResult<NoveltyAddDTO>> AddAsync(NoveltyAddDTO noveltyAddDTO)
        {
            News news = _mapper.Map<News>(noveltyAddDTO);

            if (noveltyAddDTO == null)
            {
                return new ErrorDataResult<NoveltyAddDTO>("Məlumat tapılmadı.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "news");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Process photos
            string? photoPath = null;

            if (noveltyAddDTO.CoverPhoto != null && noveltyAddDTO.CoverPhoto.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(noveltyAddDTO.CoverPhoto.FileName);
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await noveltyAddDTO.CoverPhoto.CopyToAsync(stream);
                }

                photoPath = "/uploads/news/" + uniqueFileName;
            }

            if (photoPath == null)
                return new ErrorDataResult<NoveltyAddDTO>("Şəkil yüklənmədi.");

            else
            {                
                news.CoverPhotoURL = photoPath;
            }

            // Now process files
            var uploadedPaths = new List<string>();
            string? relativePath = null;
            foreach (var file in noveltyAddDTO.Files)
            {
                
                if (file.Length > 0)
                {
                    // Unique file name to avoid collisions
                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save file to disk
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save relative path to DB
                    relativePath = Path.Combine("uploads/news/", uniqueFileName);
                }

                if (relativePath == null)
                {
                    return new ErrorDataResult<NoveltyAddDTO>("File problem");
                }

                else
                {
                    uploadedPaths.Add(relativePath);
                }
            }

            News newsForFiles = await _newsDal.AddWithReturnAsync(news);

            NewsFilesAddDTO newsFiles = new NewsFilesAddDTO();
            newsFiles.NewsId = newsForFiles.Id;
            foreach (var path in uploadedPaths)
            {
                newsFiles.FilePath = path;
                await _newsFilesService.AddAsync(newsFiles);
            }

            return new SuccessDataResult<NoveltyAddDTO>(noveltyAddDTO);
        }

        public async Task<IDataResult<List<News>>> GetAllAsync()
        {
            return new SuccessDataResult<List<News>>(await _newsDal.GetAllAsync());
        }

        public async Task<IDataResult<News>> GetByIdAsync(int id)
        {
            News news =  await _newsDal.GetAsync(n => n.Id == id);

            if (news == null)
                return new ErrorDataResult<News>("News not found");

            news.ViewCount += 1;
            return new SuccessDataResult<News>(news);
        }

        public async Task<IResult> RemoveAsync(int id)
        {
            var entityForDelete = await GetByIdAsync(id);

            if (entityForDelete.Data == null)
                return new ErrorResult("News not found");

            await _newsDal.RemoveAsync(entityForDelete.Data);
            return new SuccessResult("Deleted successfully");
        }

        public async Task<IDataResult<NoveltyUpdateDTO>> UpdateAsync(NoveltyUpdateDTO noveltyUpdateDTO)
        {
            if(noveltyUpdateDTO == null)
                throw new ArgumentNullException(nameof(noveltyUpdateDTO));

            News entityToUpdate = _mapper.Map<News>(noveltyUpdateDTO);
            await _newsDal.UpdateAsync(entityToUpdate);
            return new SuccessDataResult<NoveltyUpdateDTO>(noveltyUpdateDTO);

        }

        Task<Core.Utilities.Results.IResult> INewsService.RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
