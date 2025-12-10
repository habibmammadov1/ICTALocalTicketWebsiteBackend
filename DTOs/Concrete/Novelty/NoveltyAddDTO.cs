using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DTOs.Concrete.Novelty
{
    public class NoveltyAddDTO : IDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile CoverPhoto { get; set; }
        //public IFormFile[]? Photos { get; set; }
        public List<IFormFile>? Files { get; set; }
        public int NoveltyId { get; set; }
        [BindNever]
        public int AuthorId { get; set; }

        public NoveltyAddDTO() { }

        public NoveltyAddDTO(string title, string content, IFormFile coverPhoto, List<IFormFile>? files, int noveltyId, int authorId)
        {
            Title = title;
            Content = content;
            CoverPhoto = coverPhoto;
            Files = files;
            NoveltyId = noveltyId;
            AuthorId = authorId;
        }
    }
}
