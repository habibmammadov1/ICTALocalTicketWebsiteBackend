using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class BaseRulesFilesPhotosAddDTO : IDTO
    {
        public int RuleItemId;
        public string FilePath { get; set; }
        public int FileOrPhoto { get; set; }
        public bool IsTemporary { get; set; } = true;

        public BaseRulesFilesPhotosAddDTO()
        {
        }

        public BaseRulesFilesPhotosAddDTO(int ruleItemId, string filePath, int fileOrPhoto, bool isTemporary = true)
        {
            RuleItemId = ruleItemId;
            FilePath = filePath;
            FileOrPhoto = fileOrPhoto;
            IsTemporary = isTemporary;
        }
    }
}
