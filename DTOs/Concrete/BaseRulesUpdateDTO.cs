using DTOs.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class BaseRulesUpdateDTO : IDTO
    {
        public int Id { get; set; }
        public int BaseRuleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile CoverPhoto { get; set; }
        //public int[]? DeleteFileIds { get; set; } // API den delete olmali olan fayl ve shekillerin id lerini alir
        public string? Hashtags { get; set; }

        public BaseRulesUpdateDTO()
        {
        }

        public BaseRulesUpdateDTO(int id, int baseRuleId, string title, string content, string? hashtags)
        {
            Id = id;
            BaseRuleId = baseRuleId;
            Title = title;
            Content = content;
            Hashtags = hashtags;
        }
    }
}
