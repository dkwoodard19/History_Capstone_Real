using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class EventBLL
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public int FigureID { get; set; }
        public string FigureName { get; set; }
        public int CivID { get; set; }
        public string CivName { get; set; }

        public EventBLL(DataAccessLayer.EventDAL @event)
        {
            EventID = @event.EventID;
            EventName = @event.EventName;
            EventDate = @event.EventDate;
            FigureID = @event.FigureID;
            FigureName = @event.FigureName;
            CivID = @event.CivID;
            CivName = @event.CivName;
        }

        public EventBLL()
        {
            // default constructor(ctor) is REQUIRED for MVC
        }
    }
}
