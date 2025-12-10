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
    [Table("NOVELTIES")]
    public class BaseNovelty : IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("TITLE")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Column("CONTENT", TypeName = "nvarchar(max)")]
        public string Content { get; set; }
        [Column("NOVELTY_ID")]
        public int NoveltyId { get; set; } // 1 - News, 2 - Announcements, 3 - Articles
        [Column("COVER_PHOTO_URL", TypeName = "nvarchar(max)")]
        public string CoverPhotoURL { get; set; }
        [NotMapped]
        public string[]? FileURLs { get; set; }
        [Column("AUTHOR_ID")]
        public int AuthorId { get; set; }
        [Column("VIEW_COUNT")]
        public int ViewCount { get; set; }
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

        public virtual ICollection<NoveltyFile> NoveltyFiles { get; set; } = new List<NoveltyFile>();
        public virtual ICollection<NoveltyLike> NoveltyLike { get; set; } = new List<NoveltyLike>();

        public BaseNovelty() { }

        public BaseNovelty(int id, string title, string content, int noveltyId, string coverPhotoURL, int authorId, int viewCount, int status)
        {
            Id = id;
            Title = title;
            Content = content;
            NoveltyId = noveltyId;
            CoverPhotoURL = coverPhotoURL;
            AuthorId = authorId;
            ViewCount = viewCount;
            Status = status;
        }
    }
}
