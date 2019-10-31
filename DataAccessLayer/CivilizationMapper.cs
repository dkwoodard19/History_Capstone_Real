using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class CivilizationMapper : Mapper        //parent child relationship
    {
        int OffsetToCivID;      // 0
        int OffsetToCivName;    // 1
        int OffsetToCivStart;   // 2
        int OffsetToCivEnd;     // 3

        public CivilizationMapper(SqlDataReader reader)
        {
            OffsetToCivID = reader.GetOrdinal("CivID");
            Assert(0 == OffsetToCivID, $"CivID is {OffsetToCivID} instead of 0 as expected");
            OffsetToCivName = reader.GetOrdinal("CivName");
            Assert(1 == OffsetToCivName, $"CivName is {OffsetToCivName} instead of 1 as expected");
            OffsetToCivStart = reader.GetOrdinal("CivStart");
            Assert(2 == OffsetToCivStart, $"CivStart is {OffsetToCivStart} instead of 2 as expected");
            OffsetToCivEnd = reader.GetOrdinal("CivEnd");
            Assert(3 == OffsetToCivEnd, $"CivEnd is {OffsetToCivEnd} instead of 3 as expected");
        }

        public CivilizationDAL ToCivilization(SqlDataReader reader)
        {
            CivilizationDAL proposedReturnValue = new CivilizationDAL();
            proposedReturnValue.CivID = reader.GetInt32(OffsetToCivID);
            proposedReturnValue.CivName = reader.GetString(OffsetToCivName);
            proposedReturnValue.CivStart = reader.GetDateTime(OffsetToCivStart);
            proposedReturnValue.CivEnd = reader.GetDateTime(OffsetToCivEnd);
            return proposedReturnValue;
        }
    }
}
