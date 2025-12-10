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
    [Table("MEETINGS")]
    public class Meeting : IEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("TITLE")]
        public string Title { get; set; }
        [Column("START_DATE")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Column("START_TIME")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan StartTime { get; set; }
        [Column("END_TIME")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan? EndTime { get; set; }
        [Column("PERIOD")]
        public int Period { get; set; }
        [Column("LOCATION")]
        public string Location { get; set; }
        [Column("DESCRIPTION")]
        public string? Description { get; set; }
        [Column("FILE_PATH")]
        public string? FilePath { get; set;} 
        [Column("AUTHOR_ID")] 
        public int? OrganizerId { get; set; }
        [Column("ORGANIZER_EMAIL")]
        public string? OrganizerEmail { get; set; }
        [Column("STATUS")]
        public int Status { get; set; } // 0: Pending, 1: Approved, 2: Rejected
        [Column("IS_ACTIVE")]
        public int IsActive { get; set; } // 0: Inactive, 1: Active

        public Meeting()
        {
        }

        public Meeting(string title, DateTime startDate, TimeSpan startTime, TimeSpan? endTime, int period, string location, string? description, string organizerEmail, int status, int isActive)
        {
            Title = title;
            StartDate = startDate;
            StartTime = startTime;
            EndTime = endTime;
            Period = period;
            Location = location;
            Description = description;
            OrganizerEmail = organizerEmail;
            Status = status;
            IsActive = isActive;
        }
    }
}
