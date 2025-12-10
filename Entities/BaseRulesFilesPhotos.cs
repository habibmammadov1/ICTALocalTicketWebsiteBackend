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
    [Table("BASE_RULES_FILES_PHOTOS")]
    public class BaseRulesFilesPhotos : IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("FILE_PATH")]
        public string FilePath { get; set; }
        [Column("RULE_ITEM_ID")]
        public int RuleItemId { get; set; }
        [Column("FILE_OR_PHOTO")]
        public int FileOrPhoto { get; set; } // 1=file, 2=photo
        [Column("IS_TEMPORARY")]
        public bool IsTemporary { get; set; }
        [ForeignKey(nameof(RuleItemId))]
        public virtual BaseRules BaseRules { get; set; }

        public BaseRulesFilesPhotos()
        {
        }

        public BaseRulesFilesPhotos(string filePath, int ruleItemId, int fileOrPhoto, bool isTemporary)
        {
            FilePath = filePath;
            RuleItemId = ruleItemId;
            FileOrPhoto = fileOrPhoto;
            IsTemporary = isTemporary;
        }
    }

}
