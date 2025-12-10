using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using Data.Concrete;
using DTOs.Concrete;
using Entities;
using Entities.Novelty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CompOfferManager : ICompOfferService
    {
        private readonly ICompOfferDal _compOfferDal;
        private readonly IMapper _mapper;

        public CompOfferManager(ICompOfferDal compOfferDal, IMapper mapper)
        {
            _compOfferDal = compOfferDal;
            _mapper = mapper;
        }

        public async Task<IDataResult<CompOfferAnonymAddDTO>> AddCompOfferAnonymAsync(CompOfferAnonymAddDTO compOfferAddDTO)
        {
            CompOffer compOffer = _mapper.Map<CompOffer>(compOfferAddDTO);

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "compOffer");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string? relativePath = null;
            if (compOfferAddDTO.File.Length > 0)
            {
                // Unique file name to avoid collisions
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(compOfferAddDTO.File.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await compOfferAddDTO.File.CopyToAsync(stream);
                }

                // Save relative path to DB
                relativePath = Path.Combine("uploads/compOffer/", uniqueFileName);
            }

            if (relativePath == null)
            {
                return new ErrorDataResult<CompOfferAnonymAddDTO>("File problem");
            }

            else
            {
                compOffer.FilePath = relativePath;
            }

            await _compOfferDal.AddAsync(compOffer);
            return new SuccessDataResult<CompOfferAnonymAddDTO>(compOfferAddDTO);
        }

        public async Task<IDataResult<CompOfferAddDTO>> AddCompOfferAsync(CompOfferAddDTO compOfferAddDTO)
        {
            CompOffer compOffer = _mapper.Map<CompOffer>(compOfferAddDTO);

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "compOffer");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string? relativePath = null;
            if (compOfferAddDTO.File.Length > 0)
            {
                // Unique file name to avoid collisions
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(compOfferAddDTO.File.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await compOfferAddDTO.File.CopyToAsync(stream);
                }

                // Save relative path to DB
                relativePath = Path.Combine("uploads/compOffer/", uniqueFileName);
            }

            if (relativePath == null)
            {
                return new ErrorDataResult<CompOfferAddDTO>("File problem");
            }

            else
            {
                compOffer.FilePath = relativePath;
            }

            await _compOfferDal.AddAsync(compOffer);
            return new SuccessDataResult<CompOfferAddDTO>(compOfferAddDTO);
        }

        public async Task<IDataResult<List<CompOffer>>> GetAllAsync()
        {
            return new SuccessDataResult<List<CompOffer>>(await _compOfferDal.GetAllAsync());
        }

        public async Task<IDataResult<CompOffer>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<CompOffer>(await _compOfferDal.GetAsync(co => co.Id == id));
        }

        public async Task<IResult> RemoveAsync(int id)
        {
            var deletedEntity = await _compOfferDal.GetAsync(co => co.Id == id);
            await _compOfferDal.RemoveAsync(deletedEntity);

            return new SuccessResult("Deleted");
        }
    }
}
