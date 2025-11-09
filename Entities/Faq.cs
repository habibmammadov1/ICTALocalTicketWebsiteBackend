using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;

namespace Entities
{
    [Table("FAQ")]
    public class Faq : IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("QUESTION")]
        public string Question { get; set; }
        [Column("ANSWER")]
        public string Answer { get; set; }
        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; } = true;

    }
}

