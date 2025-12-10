using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class BaseRulesAddResultDTO : IDTO
    {
        public BaseRulesAddDTO baseRulesAddDTO { get; set; }
        public List<string> UnusedImagePaths { get; set; } = new();
        public BaseRulesAddResultDTO() { }

        public BaseRulesAddResultDTO(BaseRulesAddDTO baseRulesAddDTO, List<string> unusedImagePaths)
        {
            this.baseRulesAddDTO = baseRulesAddDTO;
            UnusedImagePaths = unusedImagePaths;
        }
    }
}
