using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using Data.Abstract;
using DTOs.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RegulationsManager : IRegulationsService
    {
        private readonly IRegulationsDal _regulationsDal;
        private readonly IMapper _mapper;

        public RegulationsManager(IRegulationsDal regulationsDal, IMapper mapper)
        {
            _regulationsDal = regulationsDal;
            _mapper = mapper;
        } 

        public async Task<IDataResult<Regulations>> getRegulationsAsync(int id, string rootPath)
        {
            Regulations regulations = await _regulationsDal.GetAsync(r => r.id == id);
            if (regulations == null || string.IsNullOrEmpty(regulations.photoPath) || string.IsNullOrEmpty(regulations.filePath))
            {
                return new ErrorDataResult<Regulations>("Məlumat tapılmadı.");
            }

            var relativePhotoPath = regulations.photoPath.TrimStart('/');
            var fullPathPhoto = Path.Combine(rootPath, relativePhotoPath);
            var relativeFilePath = regulations.filePath.TrimStart('/');
            var fullPathFile = Path.Combine(rootPath, relativeFilePath);

            Console.WriteLine($"Full photo path: {fullPathPhoto}");
            Console.WriteLine($"Exists: {System.IO.File.Exists(fullPathPhoto)}");
            if (!System.IO.File.Exists(fullPathPhoto))
                return new ErrorDataResult<Regulations>("Şəkil məlumat tapılmadı.");

            if (!System.IO.File.Exists(fullPathFile))
                return new ErrorDataResult<Regulations>("Şəkil məlumat tapılmadı.");

            // Read the file as bytes
            byte[] photoBytes = await System.IO.File.ReadAllBytesAsync(fullPathPhoto);
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(fullPathFile);

            // Convert to Base64
            string photoBase64 = Convert.ToBase64String(photoBytes);
            string fileBase64 = Convert.ToBase64String(fileBytes);

            regulations.PhotoBase64 = photoBase64;
            regulations.PhotoName = Path.GetFileName(fullPathPhoto);
            regulations.PhotoContentType = GetContentType(fullPathPhoto);

            regulations.FileBase64 = fileBase64;
            regulations.FileName = Path.GetFileName(fullPathFile);
            regulations.FileContentType = GetContentType(fullPathFile);

            return new SuccessDataResult<Regulations>(regulations);
        }

        public async Task<IDataResult<RegulationsAddDTO>> updateRegulationsAsync(RegulationsAddDTO regulationsAddDTO)
        {
            if (regulationsAddDTO == null)
            {
                return new ErrorDataResult<RegulationsAddDTO>("Məlumat tapılmadı.");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string photoPath = null;
            string filePath = null;

            if (regulationsAddDTO.Photo != null && regulationsAddDTO.Photo.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(regulationsAddDTO.Photo.FileName);
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await regulationsAddDTO.Photo.CopyToAsync(stream);
                }

                photoPath = "/uploads/" + uniqueFileName;
            }

            if (regulationsAddDTO.File != null && regulationsAddDTO.File.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(regulationsAddDTO.File.FileName);
                string fullPath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await regulationsAddDTO.File.CopyToAsync(stream);
                }

                filePath = "/uploads/" + uniqueFileName;
            }

            Regulations regulations = await _regulationsDal.GetAsync(r => r.id == regulationsAddDTO.Id);

            if (regulations == null) {
                Regulations regulationsToAdd = _mapper.Map<Regulations>(regulationsAddDTO);
                regulationsToAdd.filePath = filePath;
                regulationsToAdd.photoPath = photoPath;
                regulationsToAdd.lastUpdated = DateTime.Now;

                await _regulationsDal.AddAsync(regulationsToAdd);
            }

            else
            {
                regulations.filePath = filePath;
                regulations.photoPath = photoPath;
                regulations.lastUpdated = DateTime.Now;

                await _regulationsDal.UpdateAsync(regulations);
            }

            return new SuccessDataResult<RegulationsAddDTO>(regulationsAddDTO, "Uğurlu.");
        }

        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".pdf", "application/pdf" },
            { ".txt", "text/plain" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }
            // add more as needed
        };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }
    }
}
