using DTOs.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete.Novelty
{
    public class NoveltyUpdateDTO : NoveltyAddDTO, IDTO
    {
        public int Id { get; set; }
        public int Status { get; set; }

        public NoveltyUpdateDTO() : base() { }

        public NoveltyUpdateDTO(int id, string title, string content, IFormFile coverPhoto, List<IFormFile>? files, int noveltyId, int authorId, int status)
            : base(title, content, coverPhoto, files, noveltyId, authorId)
        {
            Id = id;
            Status = status;
        }
    }
}
