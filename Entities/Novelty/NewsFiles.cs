using Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Novelty
{
    [Table("NEWS_FILES")]
    public class NewsFiles : BaseFile, IEntity
    {
        [Column("NEWS_ID")]
        public int NewsId;
        [ForeignKey(nameof(NewsId))]
        public virtual News News { get; set; }

        public NewsFiles()
        {
        }

        public NewsFiles(int newsId, string filePath) : base(filePath)
        {
            NewsId = newsId;
        }
    }
}
