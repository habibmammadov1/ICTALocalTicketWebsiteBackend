using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Novelty
{
    [Table("News")]
    public class News : BaseNovelty, IEntity
    {
        public News() { }
        public News(int id, string title, string content, string[]? photoURLs, string[]? fileURLs, int authorId, DateTime lastUpdateDate, DateTime createDate, int viewCount, int likeCount, int dislikeCount, int status)
            : base(id, title, content, photoURLs, fileURLs, authorId, lastUpdateDate, createDate, viewCount, likeCount, dislikeCount, status)
        {
        }
    }
}
