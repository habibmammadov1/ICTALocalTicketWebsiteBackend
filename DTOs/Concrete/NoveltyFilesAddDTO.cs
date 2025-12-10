using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class NoveltyFilesAddDTO : IDTO
    {
        public int NoveltyItemId;
        public string FilePath { get; set; }

        public NoveltyFilesAddDTO()
        {
        }   

        public NoveltyFilesAddDTO(int noveltyItemId, string filePath)
        {
            NoveltyItemId = noveltyItemId;
            FilePath = filePath;
        }
    }
}
