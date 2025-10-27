using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Novelty
{
    public class BaseFile 
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("FILE_PATH")]
        public string FilePath { get; set; }

        public BaseFile()
        {
        }

        public BaseFile(string filePath)
        {
            FilePath = filePath;
        }
    }
}
