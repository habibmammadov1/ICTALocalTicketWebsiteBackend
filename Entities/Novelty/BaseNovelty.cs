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
        [NotMapped]
        public string[]? PhotoURLs { get; set; }
        [NotMapped]
        public string[]? FileURLs { get; set; }
        [Column("AUTHOR_ID")]
        public int AuthorId { get; set; }
        [Column("LAST_UPDATE_DATE")]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        [Column("CREATE_DATE")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Column("VIEW_COUNT")]
        public int ViewCount { get; set; }
        [Column("LIKE_COUNT")]
        public int LikeCount { get; set; }
        [Column("DISLIKE_COUNT")]
        public int DislikeCount { get; set; }
        public int Status { get; set; } = 1; // Status of active

        public BaseNovelty() { }

        public BaseNovelty(int id, string title, string content, string[]? photoURLs, string[]? fileURLs, int authorId, DateTime lastUpdateDate, DateTime createDate, int viewCount, int likeCount, int dislikeCount, int status)
        {
            Id = id;
            Title = title;
            Content = content;
            PhotoURLs = photoURLs;
            FileURLs = fileURLs;
            AuthorId = authorId;
            LastUpdateDate = lastUpdateDate;
            CreateDate = createDate;
            ViewCount = viewCount;
            LikeCount = likeCount;
            DislikeCount = dislikeCount;
            Status = status;
        }
    }
}
