using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Novelty
{
    [Table("Announcements")]
    public class Announcement : BaseNovelty, IEntity
    {
        public Announcement() { }

        public Announcement(int id, string title, string content, string coverPhotoURL, int authorId, int viewCount, int likeCount, int dislikeCount, int status) : base(id, title, content, coverPhotoURL, authorId, viewCount, likeCount, dislikeCount, status)
        {
        }
    }
}
