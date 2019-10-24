using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class EventMapper : Mapper
    {
        int OffsetToEventID;        // 0
        int OffsetToEventName;      // 1
        int OffsetToEventDate;      // 2
        int OffsetToFigureID;       // 3
        int OffsetToFigureName;     // 4
        int OffsetToCivID;          // 5
        int OffsetToCivName;        // 6

        public EventMapper(SqlDataReader reader)
        {
            OffsetToEventID = reader.GetOrdinal("EventID");
            Assert(0 == OffsetToEventID, $"EventID is {OffsetToEventID} instead of 0 as expected");
            OffsetToEventName = reader.GetOrdinal("EventName");
            Assert(1 == OffsetToEventName, $"EventName is {OffsetToEventName} instead of 1 as expected");
            OffsetToEventDate = reader.GetOrdinal("EventDate");
            Assert(2 == OffsetToEventDate, $"EventDate is {OffsetToEventDate} instead of 2 as expected");
            OffsetToFigureID = reader.GetOrdinal("FigureID");
            Assert(3 == OffsetToFigureID, $"FigureID is {OffsetToFigureID} instead of 3 as expected");
            OffsetToFigureName = reader.GetOrdinal("FigureName");
            Assert(4 == OffsetToFigureName, $"FigureName is {OffsetToFigureName} instead of 4 as expected");
            OffsetToCivID = reader.GetOrdinal("CivID");
            Assert(5 == OffsetToCivID, $"CivID is {OffsetToCivID} instead of 5 as expected");
            OffsetToCivName = reader.GetOrdinal("CivName");
            Assert(6 == OffsetToCivName, $"CivName is {OffsetToCivName} instead of 6 as expected");
        }

        public EventDAL ToEvent(SqlDataReader reader)
        {
            EventDAL proposedReturnValue = new EventDAL();
            proposedReturnValue.EventID = reader.GetInt32(OffsetToEventID);
            proposedReturnValue.EventName = reader.GetString(OffsetToEventName);
            proposedReturnValue.EventDate = reader.GetDateTime(OffsetToEventDate);
            proposedReturnValue.FigureID = reader.GetInt32(OffsetToFigureID);
            proposedReturnValue.FigureName = reader.GetString(OffsetToFigureName);
            proposedReturnValue.CivID = reader.GetInt32(OffsetToCivID);
            proposedReturnValue.CivName = reader.GetString(OffsetToCivName);
            return proposedReturnValue;
        }
    }
}
