using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;

namespace Entities.Novelty
{
    [Table("NOVELTIES_LIKE_DISLIKE")]
    // This table/entity is used for likes and dislikes on all news, announcements, articles, etc.
    public class NoveltyLike : LikeDislikeActiveDeactive, IEntity
    {
        // Navigation property
        [ForeignKey(nameof(NoveltyItemId))]
        public virtual BaseNovelty NoveltyItem { get; set; }
        public NoveltyLike() { }

        public NoveltyLike(int id, int noveltyId, int likeStatus, int dislikeStatus, string whoLikes) : base(id, noveltyId, likeStatus, dislikeStatus, whoLikes)
        {
        }
    }
}
