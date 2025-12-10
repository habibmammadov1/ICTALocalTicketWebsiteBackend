using Azure;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Footer")]
    public class Footer : IEntity
    {
        [Key]
        [Column("ID")]
        public int id { get; private set; }

        [Column("DATE_YEAR")]
        public int? dateYear { get; private set; }

        [Column("ROOM_NO")]
        public string roomNo { get; set; }

        [Column("TECHNICAL_PHONE")]
        public string technicalPhone { get; set; }

        [Column("MADE_BY")]
        public string madeBy { get; set; }

        [Column("LAST_UPDATED")]
        public DateTime lastUpdated { get; private set; }

        public Footer()
        {
            
        }

        public Footer(int? dateYear, string roomNo, string technicalPhone, string madeBy, DateTime lastUpdated)
        {
            this.dateYear = dateYear;
            this.roomNo = roomNo;
            this.technicalPhone = technicalPhone;
            this.madeBy = madeBy;
            this.lastUpdated = lastUpdated;
        }

        public Footer(int id,int? dateYear, string roomNo, string technicalPhone, string madeBy, DateTime lastUpdated)
        {
            this.id = id;
            this.dateYear = dateYear;
            this.roomNo = roomNo;
            this.technicalPhone = technicalPhone;
            this.madeBy = madeBy;
            this.lastUpdated = lastUpdated;
        }

        public void SetDateYear(int year)
        {
            dateYear = year;
        }

        public void SetLastUpdated(DateTime dateTime)
        {
            lastUpdated = dateTime;
        }

        public void SetId(int id)
        {
            this.id = id;
        }
    }
}