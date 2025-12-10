using Entities;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Concrete.GraphManager;

namespace Business.Abstract
{
    public interface IGraphService
    {
        public List<Appointment> GetMeetings();
        public List<Appointment> GetRoomAppointments(string roomEmail);
        public List<Meeting> GetRoomAppointments2(string roomEmail);
    }
}
