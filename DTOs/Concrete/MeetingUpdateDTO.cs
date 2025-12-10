using DTOs.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Concrete
{
    public class MeetingUpdateDTO : IDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int Period { get; set; }
        public string Location { get; set; }
        public IFormFile? FormFile { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; } = 0;
        public int IsActive { get; set; }

        public MeetingUpdateDTO()
        {
        }

        public MeetingUpdateDTO(int id, string title, DateTime startDate, TimeSpan startTime, TimeSpan? endTime, int period, string location, string? description, int status, int isActive)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            StartTime = startTime;
            EndTime = endTime;
            Period = period;
            Location = location;
            Description = description;
            Status = status;
            IsActive = isActive;
        }
    }
}
