using AutoMapper;
using Business.Abstract;
using Business.Helpers;
using Core.Utilities.Results;
using Data.Abstract;
using Data.Concrete;
using DTOs.Concrete;
using DTOs.Concrete.Novelty;
using Entities.Novelty;
//using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Business.Concrete
{
    public class NoveltyManager : INoveltyService
    {
        private readonly IBaseNoveltyDal _baseNoveltyDal;
        private readonly IMapper _mapper;
        private readonly INoveltyFilesService _noveltyFilesService;
        private readonly INoveltyLikeDal _noveltyLikeDal;

        public NoveltyManager(
            IBaseNoveltyDal baseNoveltyDal, 
            IMapper mapper, 
            INoveltyFilesService noveltyFilesService, 
            INoveltyLikeDal noveltyLikeDal)
        {
            _baseNoveltyDal = baseNoveltyDal;
            _mapper = mapper;
            _noveltyFilesService = noveltyFilesService;
            _noveltyLikeDal = noveltyLikeDal;
        }

        public async Task<IResult> ActiveDeactiveAsync(int id, int status)
        {
            // If status is 0 then deactivate, if 1 then activate
            BaseNovelty baseNovelty = await _baseNoveltyDal.GetAsync(n => n.Id == id);

            if (baseNovelty == null)
                return new ErrorResult("News not found");

            baseNovelty.Status = status;
            await _baseNoveltyDal.UpdateAsync(baseNovelty);
            return new SuccessResult("Status updated successfully");
        }

        public async Task<IDataResult<NoveltyAddDTO>> AddAsync(NoveltyAddDTO noveltyAddDTO)
        {
            BaseNovelty baseNovelty = _mapper.Map<BaseNovelty>(noveltyAddDTO);

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
                baseNovelty.CoverPhotoURL = photoPath;
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

            BaseNovelty baseNoveltyForFiles = await _baseNoveltyDal.AddWithReturnAsync(baseNovelty);

            NoveltyFilesAddDTO newsFiles = new NoveltyFilesAddDTO();
            newsFiles.NoveltyItemId = baseNoveltyForFiles.Id;
            foreach (var path in uploadedPaths)
            {
                newsFiles.FilePath = path;
                await _noveltyFilesService.AddAsync(newsFiles);
            }

            return new SuccessDataResult<NoveltyAddDTO>(noveltyAddDTO);
        }

        public async Task<IDataResult<List<BaseNovelty>>> GetAllAsync()
        {
            return new SuccessDataResult<List<BaseNovelty>>(await _baseNoveltyDal.GetAllAsync());
        }

        public async Task<IDataResult<BaseNovelty>> GetByIdAsync(int id)
        {
            /*
             Burda viewcountla bagli kodlar yazacayiq.
             Eger biz bunu admin panelden cagiririqsa, icrease olmamalidir. Yeni rolu yoxlamaliyiq. Eger s admin, admin ve s. dirse,
             onda sayilmir. Eks halda increase ele.
             Eyni zamanda yarim saat erzinde 10 defe de get metodu cagirsa, bu 1 defe artacaq.
             */

            BaseNovelty baseNovelty =  await _baseNoveltyDal.GetAsync(n => n.Id == id);

            if (baseNovelty == null)
                return new ErrorDataResult<BaseNovelty>("News not found");

            return new SuccessDataResult<BaseNovelty>(baseNovelty);
        }

        public async Task<IResult> LikeActiveDeactiveAsync(int id, int likeActiveDeactive, string whoLikes)
        {
            // 0 - like deaktive, 1 - like aktive

            BaseNovelty baseNovelty = await _baseNoveltyDal.GetAsync(n => n.Id == id);

            if (baseNovelty == null)
                return new ErrorResult("News not found");

            if (likeActiveDeactive == 1)
            {
                var existingLike =
                    await _noveltyLikeDal.GetAsync(nl => nl.NoveltyItemId == id && nl.WhoLikes == whoLikes && nl.LikeStatus == 0);

                if (existingLike != null)
                {
                    existingLike.LikeStatus = 1;
                    existingLike.DislikeStatus = 0;

                    await _noveltyLikeDal.UpdateAsync(existingLike);
                }

                else
                {
                    NoveltyLike noveltyLike = new NoveltyLike
                    {
                        NoveltyItemId = id,
                        LikeStatus = likeActiveDeactive,
                        DislikeStatus = 0,
                        WhoLikes = whoLikes
                    };

                    await _noveltyLikeDal.AddAsync(noveltyLike);
                }                             
            }

            else
            {
                var existingLike =
                    await _noveltyLikeDal.GetAsync(nl => nl.NoveltyItemId == id && nl.WhoLikes == whoLikes && nl.LikeStatus == 1);

                if (existingLike == null) {
                    return new ErrorResult("There is not such record");
                }

                existingLike.LikeStatus = 0;

                await _noveltyLikeDal.UpdateAsync(existingLike);
            }            

            return new SuccessResult("Liked succesfully");
        }
        public async Task<IResult> DislikeActiveDeactiveAsync(int id, int dislikeActiveDeactive, string whoLikes)
        {
            BaseNovelty baseNovelty = await _baseNoveltyDal.GetAsync(n => n.Id == id);

            if (baseNovelty == null)
                return new ErrorResult("News not found");

            if (dislikeActiveDeactive == 1)
            {
                var existingDislike =
                    await _noveltyLikeDal.GetAsync(nl => nl.NoveltyItemId == id && nl.WhoLikes == whoLikes && nl.DislikeStatus == 0);

                if (existingDislike != null)
                {
                    existingDislike.LikeStatus = 0;
                    existingDislike.DislikeStatus = 1;

                    await _noveltyLikeDal.UpdateAsync(existingDislike);
                }

                else
                {
                    NoveltyLike noveltyDislike = new NoveltyLike
                    {
                        NoveltyItemId = id,
                        LikeStatus = 0,
                        DislikeStatus = 1,
                        WhoLikes = whoLikes
                    };

                    await _noveltyLikeDal.AddAsync(noveltyDislike);
                }
            }

            else
            {
                var existingDislike =
                    await _noveltyLikeDal.GetAsync(nl => nl.NoveltyItemId == id && nl.WhoLikes == whoLikes && nl.DislikeStatus == 1);

                if (existingDislike == null)
                {
                    return new ErrorResult("There is not such record");
                }

                existingDislike.DislikeStatus = 0;

                await _noveltyLikeDal.UpdateAsync(existingDislike);
            }

            return new SuccessResult("Disliked succesfully");
        }

        public async Task<IResult> RemoveAsync(int id)
        {
            var entityForDelete = await GetByIdAsync(id);

            if (entityForDelete.Data == null)
                return new ErrorResult("News not found");

            await _baseNoveltyDal.RemoveAsync(entityForDelete.Data);
            return new SuccessResult("Deleted successfully");
        }

        public async Task<IDataResult<NoveltyUpdateDTO>> UpdateAsync(NoveltyUpdateDTO noveltyUpdateDTO)
        {
            if(noveltyUpdateDTO == null)
                throw new ArgumentNullException(nameof(noveltyUpdateDTO));

            BaseNovelty entityToUpdate = _mapper.Map<BaseNovelty>(noveltyUpdateDTO);
            await _baseNoveltyDal.UpdateAsync(entityToUpdate);
            return new SuccessDataResult<NoveltyUpdateDTO>(noveltyUpdateDTO);

        }

        public async Task<IDataResult<List<BaseNovelty>>> GetByNoveltyIdAsync(int noveltyId)
        {
            return new SuccessDataResult<List<BaseNovelty>>(await _baseNoveltyDal.GetAllAsync(n => n.NoveltyId == noveltyId));
        }
    }
}
