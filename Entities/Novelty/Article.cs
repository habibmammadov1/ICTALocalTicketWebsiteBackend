using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Novelty
{
    [Table("Articles")] 
    public class Article : BaseNovelty, IEntity
    {
        public Article() { }

        public Article(int id, string title, string content, string coverPhotoURL, int authorId, int viewCount, int likeCount, int dislikeCount, int status) : base(id, title, content, coverPhotoURL, authorId, viewCount, likeCount, dislikeCount, status)
        {
        }
    }
}
