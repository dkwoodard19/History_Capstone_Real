using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EventDAL
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public int FigureID { get; set; }
        public string FigureName { get; set; }
        public int CivID { get; set; }
        public string CivName { get; set; }
    }
}
