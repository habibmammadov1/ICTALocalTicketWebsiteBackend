using DTOs.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DTOs.Concrete
{
    public class MeetingAddDTO : IDTO
    {
        public string Title { get; set; }

        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int Period { get; set; }
        public string Location { get; set; }
        public string? Description { get; set; }
        public IFormFile? FormFile { get; set; }
        public int AuthorId { get; set; }
        public int Status { get; set; } = 0;

        public MeetingAddDTO()
        {
        }

        public MeetingAddDTO(string title, DateTime startDate, TimeSpan startTime, TimeSpan? endTime, int period, string location, string? description, IFormFile formFile, int authorId, int status)
        {
            Title = title;
            StartDate = startDate;
            StartTime = startTime;
            EndTime = endTime;
            Period = period;
            Location = location;
            Description = description;
            FormFile = formFile;
            AuthorId = authorId;
            Status = status;
        }
    }
}