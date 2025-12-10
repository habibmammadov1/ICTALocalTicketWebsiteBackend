using DTOs.Abstract;
using Microsoft.AspNetCore.Http;

namespace DTOs.Concrete
{
    public class BaseRulesAddDTO : IDTO
    {
        public int BaseRuleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile CoverPhoto { get; set; } 
        public IFormFile[] Photos { get; set; } 
        public IFormFile[]? Files { get; set; } 
        public string? Hashtags { get; set; }

        public BaseRulesAddDTO()
        {
        }

        public BaseRulesAddDTO(int baseRuleId, string title, string content, string? hashtags)
        {
            BaseRuleId = baseRuleId;
            Title = title;
            Content = content;
            Hashtags = hashtags;
        }
    }
}
