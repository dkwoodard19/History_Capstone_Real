using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class FigureMapper : Mapper
    {
        int OffsetToFigureID;   // 0
        int OffsetToFigureName; // 1
        int OffsetToFigureDOB;  // 2
        int OffsetToFigureDOD;  // 3
        int OffsetToCivID;      // 4
        int OffsetToCivName;    // 5

        public FigureMapper(SqlDataReader reader)
        {
            OffsetToFigureID = reader.GetOrdinal("FigureID");
            Assert(0 == OffsetToFigureID, $"FigureID is {OffsetToFigureID} instead of 0 as expected");
            OffsetToFigureName = reader.GetOrdinal("FigureName");
            Assert(1 == OffsetToFigureName, $"FigureName is {OffsetToFigureName} instead of 1 as expected");
            OffsetToFigureDOB = reader.GetOrdinal("FigureDOB");
            Assert(2 == OffsetToFigureDOB, $"FigureDOB is {OffsetToFigureDOB} instead of 2 as expected");
            OffsetToFigureDOD = reader.GetOrdinal("FigureDOD");
            Assert(3 == OffsetToFigureDOD, $"FigureDOD is {OffsetToFigureDOD} instead of 3 as expected");
            OffsetToCivID = reader.GetOrdinal("CivID");
            Assert(4 == OffsetToCivID, $"CivID is {OffsetToCivID} instead of 4 as expected");
            OffsetToCivName = reader.GetOrdinal("CivName");
            Assert(5 == OffsetToCivName, $"CivName is {OffsetToCivName} instead of 5 as expected");
        }

        public FigureDAL ToFigure(SqlDataReader reader)
        {
            FigureDAL proposedReturnValue = new FigureDAL();
            proposedReturnValue.FigureID = reader.GetInt32(OffsetToFigureID);
            proposedReturnValue.FigureName = reader.GetString(OffsetToFigureName);
            proposedReturnValue.FigureDOB = reader.GetDateTime(OffsetToFigureDOB);
            proposedReturnValue.FigureDOD = reader.GetDateTime(OffsetToFigureDOD);
            proposedReturnValue.CivID = reader.GetInt32(OffsetToCivID);
            proposedReturnValue.CivName = reader.GetString(OffsetToCivName);
            return proposedReturnValue;
        }
    }
}
