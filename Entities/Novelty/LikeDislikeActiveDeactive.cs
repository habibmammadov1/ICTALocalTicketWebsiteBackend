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
    public class LikeDislikeActiveDeactive
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NOVELTY_ITEM_ID")]
        public int NoveltyItemId { get; set; }
        [Column("LIKE_STATUS")]
        public int LikeStatus { get; set; }
        [Column("DISLIKE_STATUS")]
        public int DislikeStatus { get; set; }
        [Column("WHO_LIKES")]
        public string WhoLikes { get; set; }

        public LikeDislikeActiveDeactive()
        {

        }

        public LikeDislikeActiveDeactive(int id, int noveltyItemId, int likeStatus, int dislikeStatus, string whoLikes)
        {
            Id = id;
            NoveltyItemId = noveltyItemId;
            LikeStatus = likeStatus;
            DislikeStatus = dislikeStatus;
            WhoLikes = whoLikes;
        }
    }
}
