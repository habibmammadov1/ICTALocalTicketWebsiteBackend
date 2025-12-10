using Entities.Abstract;
using Entities.Novelty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("BASE_RULES")]
    public class BaseRules : IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("BASE_RULE_ID")]
        public int BaseRuleId { get; set; }
        [Column("TITLE")]
        public string Title { get; set; }
        [Column("CONTENT")]
        public string Content { get; set; }
        [Column("COVER_PHOTO")]
        public string CoverPhoto { get; set; }  // store path or URL
        [Column("HASHTAGS")]
        public string? Hashtags { get; set; } // tags
        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; }
        [Column("UPDATE_TIME")]
        public DateTime? UpdateTime { get; set; }
        [Column("CREATED_BY")]
        public int CreatedBy { get; set; }
        [Column("UPDATED_BY")]
        public int? UpdatedBy { get; set; }
        [Column("VIEW_COUNT")]
        public int ViewCount { get; set; } = 0;
        [Column("STATUS")]
        public int Status { get; set; } = 1;    // 1 = active, 0 = passive
        public virtual ICollection<BaseRulesFilesPhotos> BaseRulesFilesPhotos { get; set; } = new List<BaseRulesFilesPhotos>();

        public BaseRules()
        {
        }

        public BaseRules(int id, int baseRuleId, string title, string content, string coverPhoto, string? hashtags, DateTime createTime, DateTime? updateTime, int createdBy, int? updatedBy, int viewCount, int status)
        {
            Id = id;
            BaseRuleId = baseRuleId;
            Title = title;
            Content = content;
            CoverPhoto = coverPhoto;
            Hashtags = hashtags;
            CreateTime = createTime;
            UpdateTime = updateTime;
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
            ViewCount = viewCount;
            Status = status;
        }
    }
}