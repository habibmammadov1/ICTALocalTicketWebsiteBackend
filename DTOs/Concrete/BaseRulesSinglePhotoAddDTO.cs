using DTOs.Abstract;
using Microsoft.AspNetCore.Http;

namespace DTOs.Concrete
{
    public class BaseRulesSinglePhotoAddDTO : IDTO
    {
        public IFormFile Photo { get; set; }
        public BaseRulesSinglePhotoAddDTO() { }
    }
}
