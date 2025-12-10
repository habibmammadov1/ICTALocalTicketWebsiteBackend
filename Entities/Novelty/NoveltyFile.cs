using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;

namespace Entities.Novelty
{
    [Table("NOVELTIES_FILES")]
    public class NoveltyFile : IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("FILE_PATH")]
        public string FilePath { get; set; }

        [Column("NOVELTY_ITEM_ID")]
        public int NoveltyItemId { get; set; }

        // Navigation property
        [ForeignKey(nameof(NoveltyItemId))]
        public virtual BaseNovelty NoveltyItem { get; set; }

        public NoveltyFile()
        {
        }

        public NoveltyFile(string filePath)
        {
            FilePath = filePath;
        }
    }
}
