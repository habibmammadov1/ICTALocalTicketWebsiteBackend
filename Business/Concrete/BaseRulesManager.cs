using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using Data.Concrete;
using DTOs.Concrete;
using DTOs.Concrete.Novelty;
using Entities;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BaseRulesManager : IBaseRulesService
    {
        private readonly IBaseRulesDal _baseRulesDal;
        private readonly IMapper _mapper;
        private readonly IBaseRulesFilesPhotosService _baseRulesFilesPhotosService;
        public BaseRulesManager(IBaseRulesDal baseRulesDal, IMapper mapper, IBaseRulesFilesPhotosService baseRulesFilesPhotosService)
        {
            _baseRulesDal = baseRulesDal;
            _mapper = mapper;
            _baseRulesFilesPhotosService = baseRulesFilesPhotosService;
        }

        public async Task<IResult> ActiveDeactiveRuleAsync(int id, int activeDeactive)
        {
            BaseRules baseRules = await _baseRulesDal.GetAsync(br => br.Id == id && br.Status != activeDeactive);

            if (baseRules == null)
            {
                return new ErrorResult("Məlumat tapılmadı.");
            }

            baseRules.Status = activeDeactive;

            await _baseRulesDal.UpdateAsync(baseRules);

            return new SuccessResult("Status uğurla update olundu.");
        }

        public async Task<IDataResult<BaseRulesAddDTO>> AddBaseRuleAsync(BaseRulesAddDTO baseRulesAddDTO, int userId)
        {
            // Fotolar ckeditor5 textarea dan gələcək, ona görə ayrıca yüklənmir
            if (baseRulesAddDTO == null)
            {
                return new ErrorDataResult<BaseRulesAddDTO>("Məlumat tapılmadı.");
            }

            BaseRules baseRuleEntity = _mapper.Map<BaseRules>(baseRulesAddDTO);
            baseRuleEntity.CreateTime = DateTime.UtcNow;
            baseRuleEntity.CreatedBy = userId;

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "baseRules");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string? photoPath = null;

            if (baseRulesAddDTO.CoverPhoto != null && baseRulesAddDTO.CoverPhoto.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(baseRulesAddDTO.CoverPhoto.FileName);
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await baseRulesAddDTO.CoverPhoto.CopyToAsync(stream);
                }

                photoPath = "/uploads/baseRules/" + uniqueFileName;
            }

            if (photoPath == null)
                return new ErrorDataResult<BaseRulesAddDTO>("Şəkil yüklənmədi.");

            else
            {
                baseRuleEntity.CoverPhoto = photoPath;
            }

            // Now process files
            var uploadedPaths = new List<string>();
            string? relativePath = null;
            foreach (var file in baseRulesAddDTO.Files)
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
                    relativePath = Path.Combine("uploads/baseRules/", uniqueFileName);
                }

                if (relativePath == null)
                {
                    return new ErrorDataResult<BaseRulesAddDTO>("File problem");
                }

                else
                {
                    uploadedPaths.Add(relativePath);
                }
            }

            BaseRules baseRulesForFiles = await _baseRulesDal.AddWithReturnAsync(baseRuleEntity);

            BaseRulesFilesPhotosAddDTO ruleFiles = new BaseRulesFilesPhotosAddDTO();
            ruleFiles.RuleItemId = baseRulesForFiles.Id;
            ruleFiles.FileOrPhoto = 1;
            foreach (var path in uploadedPaths)
            {
                ruleFiles.FilePath = path;
                await _baseRulesFilesPhotosService.AddAsync(_mapper.Map<BaseRulesFilesPhotos>(ruleFiles));
            }

            // Now photo process
            uploadedPaths = new List<string>();
            relativePath = null;
            foreach (var photo in baseRulesAddDTO.Photos)
            {
                if (photo.Length > 0)
                {
                    // Unique file name to avoid collisions
                    var uniquePhotoName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
                    var thePhotoPath = Path.Combine(uploadsFolder, uniquePhotoName);

                    // Save file to disk
                    using (var stream = new FileStream(thePhotoPath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    // Save relative path to DB
                    relativePath = Path.Combine("uploads/baseRules/", uniquePhotoName);
                }

                if (relativePath == null)
                {
                    return new ErrorDataResult<BaseRulesAddDTO>("Photo problem");
                }

                else
                {
                    uploadedPaths.Add(relativePath);
                }
            }

            BaseRulesFilesPhotos rulePhotos = new BaseRulesFilesPhotos();
            ruleFiles.RuleItemId = baseRulesForFiles.Id;
            ruleFiles.FileOrPhoto = 2; // 2 - photo
            foreach (var path in uploadedPaths)
            {
                ruleFiles.FilePath = path;
                await _baseRulesFilesPhotosService.AddAsync(_mapper.Map<BaseRulesFilesPhotos>(ruleFiles));
            }

            return new SuccessDataResult<BaseRulesAddDTO>(baseRulesAddDTO, "Added successfully.");
        }

        public async Task<IResult> DeleteBaseRuleAsync(int id)
        {
            BaseRules baseRules = await _baseRulesDal.GetAsync(br => br.Id == id);
            if (baseRules == null)
            {
                return new ErrorResult("Məlumat tapılmadı.");
            }

            await _baseRulesDal.RemoveAsync(baseRules);
            return new SuccessResult("Deleted successfully.");
        }

        public async Task<IDataResult<List<BaseRules>>> GetAllBaseRulesAsync()
        {
            return new SuccessDataResult<List<BaseRules>>(await _baseRulesDal.GetAllAsync(br => br.Status == 1));
        }

        public async Task<IDataResult<List<BaseRules>>> GetBaseRuleByBaseRuleIdAsync(int id)
        {
            return new SuccessDataResult<List<BaseRules>>(await _baseRulesDal.GetAllAsync(br => br.BaseRuleId == id && br.Status == 1));
        }

        public async Task<IDataResult<BaseRules>> GetBaseRuleByIdAsync(int id)
        {
            BaseRules baseRules = await _baseRulesDal.GetAsync(br => br.Id == id && br.Status == 1);

            if (baseRules == null)
                return new ErrorDataResult<BaseRules>("Məlumat tapılmadı.");

            return new SuccessDataResult<BaseRules>(baseRules);
        }

        public async Task<IDataResult<BaseRulesUpdateDTO>> UpdateBaseRuleAsync(BaseRulesUpdateDTO baseRulesUpdateDTO, int userId)
        {
            BaseRules baseRules = _baseRulesDal.GetAsync(br => br.Id == baseRulesUpdateDTO.Id && br.Status == 1).Result;

            if (baseRules == null)
            {
                return new ErrorDataResult<BaseRulesUpdateDTO>("Məlumat tapılmadı.");
            }

            baseRules.UpdateTime = DateTime.UtcNow;
            baseRules.UpdatedBy = userId;

            await _baseRulesDal.UpdateAsync(baseRules);
            return new SuccessDataResult<BaseRulesUpdateDTO>(baseRulesUpdateDTO, "Updated successfully.");
        }
    }
}