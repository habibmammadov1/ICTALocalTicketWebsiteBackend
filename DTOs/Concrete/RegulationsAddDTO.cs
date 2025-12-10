using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class RegulationsAddDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile File { get; set; }

        public RegulationsAddDTO()
        {
            
        }

        public RegulationsAddDTO(int id, string title, string description, IFormFile photo, IFormFile file)
        {
            Id = id;
            Title = title;
            Description = description;
            Photo = photo;
            File = file;
        }
    }
}
