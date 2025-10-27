using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class NewsFilesAddDTO : IDTO
    {
        public int NewsId;
        public string FilePath { get; set; }

        public NewsFilesAddDTO()
        {
        }   

        public NewsFilesAddDTO(int newsId, string filePath)
        {
            NewsId = newsId;
            FilePath = filePath;
        }
    }
}
