using Azure.Identity;
using Business.Abstract;
using Business.Helpers;
using Entities;
using Microsoft.Exchange.WebServices.Data;
using System.Text.RegularExpressions;

namespace Business.Concrete
{
    public class GraphManager : IGraphService
    {
        public List<Appointment> GetMeetings()
        {
            var service = new ExchangeServiceProvider().Connect();

            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(30);

            CalendarView view = new CalendarView(startDate, endDate)
            {
                PropertySet = new PropertySet(
                    AppointmentSchema.Subject,
                    AppointmentSchema.Start,
                    AppointmentSchema.End,
                    AppointmentSchema.Location
                )
            };

            FindItemsResults<Appointment> results =
                service.FindAppointments(WellKnownFolderName.Calendar, view);

            List<Appointment> list = new List<Appointment>();

            foreach (var item in results)
                list.Add(item);

            return list;
        }

        public List<Appointment> GetRoomAppointments(string roomEmail)
        {
            var service = new ExchangeServiceProvider().Connect();

            DateTime start = DateTime.Today;
            DateTime end = DateTime.Today.AddDays(30);

            CalendarView view = new CalendarView(start, end)
            {
                PropertySet = new PropertySet(
                    AppointmentSchema.Subject,
                    AppointmentSchema.Start,
                    AppointmentSchema.End,
                    AppointmentSchema.Location,
                    AppointmentSchema.Organizer
                )
            };

            var roomCalendar = new FolderId(WellKnownFolderName.Calendar, roomEmail);

            return service.FindAppointments(roomCalendar, view).ToList();
        }

        public List<Meeting> GetRoomAppointments2(string roomEmail)
        {
            var service = new ExchangeServiceProvider().Connect();

            DateTime start = DateTime.Today;
            DateTime end = DateTime.Today.AddDays(30);

            var view = new CalendarView(start, end)
            {
                PropertySet = new PropertySet(
                    BasePropertySet.FirstClassProperties,   // gets the main fields
                    AppointmentSchema.Id,
                    AppointmentSchema.Subject,
                    AppointmentSchema.Start,
                    AppointmentSchema.End,
                    AppointmentSchema.Location,
                    AppointmentSchema.Organizer
                //AppointmentSchema.RequiredAttendees
                )
            };

            var roomCalendar = new FolderId(WellKnownFolderName.Calendar, roomEmail);

            var results = service.FindAppointments(roomCalendar, view);

            var list = new List<Meeting>();

            foreach (var a in results)
            {                
                // Sometimes body is not fully loaded; this ensures it is
                a.Load(new PropertySet(
                    BasePropertySet.FirstClassProperties,   // gets the main fields
                    AppointmentSchema.Id,
                    AppointmentSchema.Subject,
                    AppointmentSchema.Start,
                    AppointmentSchema.End,
                    AppointmentSchema.Location,
                    AppointmentSchema.Organizer,
                    AppointmentSchema.Body
                    //AppointmentSchema.RequiredAttendees
                ));

                var cleanText = String.Empty;
                if (a.Body == null) {
                    cleanText = a.Body.Text;
                    cleanText = Regex.Replace(cleanText, "<.*?>", String.Empty);
                }

                list.Add(new Meeting
                {
                    Title = a.Subject,

                    // StartDate yalnız tarixi saxlayır
                    StartDate = a.Start.Date,

                    // StartTime ayrıca TimeSpan saxlayır
                    StartTime = a.Start.ToLocalTime().TimeOfDay,

                    // EndTime TimeSpan? tipindədir
                    EndTime = a.End.TimeOfDay,

                    // Period – iclasın müddəti dəqiqə ilə
                    Period = (int)(a.End - a.Start).TotalMinutes,

                    Location = a.Location,

                    Description = cleanText,

                    OrganizerEmail = a.Organizer?.Address,

                    Status = 1,       // avtomatik approve etmək istəyirsənsə
                    IsActive = 1      // aktiv olsun deyə
                    //RequiredAttendees = a.RequiredAttendees
                    //    .Select(t => new AttendeeInfo { Name = t.Name, Email = t.Address })
                    //    .ToList()
                });
            }

            return list;
        }
    }
}
