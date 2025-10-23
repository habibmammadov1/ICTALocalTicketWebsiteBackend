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
        public IFormFile[]? Photos { get; set; }
        public IFormFile[]? Files { get; set; }
        [BindNever]
        public int AuthorId { get; set; }

        public NoveltyAddDTO() { }

        public NoveltyAddDTO(string title, string content, IFormFile[]? photos, IFormFile[]? files, int authorId)
        {
            Title = title;
            Content = content;
            Photos = photos;
            Files = files;
            AuthorId = authorId;
        }
    }
}
