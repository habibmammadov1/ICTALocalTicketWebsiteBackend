using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Novelty
{
    public class BaseNovelty
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("TITLE")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Column("CONTENT", TypeName = "nvarchar(max)")]
        public string Content { get; set; }
        [Column("COVER_PHOTO_URL", TypeName = "nvarchar(max)")]
        public string CoverPhotoURL { get; set; }
        [NotMapped]
        public string[]? FileURLs { get; set; }
        [Column("AUTHOR_ID")]
        public int AuthorId { get; set; }
        [Column("VIEW_COUNT")]
        public int ViewCount { get; set; }
        [Column("LIKE_COUNT")]
        public int LikeCount { get; set; }
        [Column("DISLIKE_COUNT")]
        public int DislikeCount { get; set; }
        private int _status = 1;
        public int Status // Status of active
        {
            get => _status;
            set
            {
                if (value != 0 && value != 1)
                    throw new ArgumentException("Status must be either 0 or 1.");
                _status = value;
            }
        }

        public BaseNovelty() { }

        public BaseNovelty(int id, string title, string content, string coverPhotoURL, int authorId, int viewCount, int likeCount, int dislikeCount, int status)
        {
            Id = id;
            Title = title;
            Content = content;
            CoverPhotoURL = coverPhotoURL;
            AuthorId = authorId;
            ViewCount = viewCount;
            LikeCount = likeCount;
            DislikeCount = dislikeCount;
            Status = status;
        }
    }
}
