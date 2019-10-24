using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    class RoleMapper : Mapper
    {
        int OffsetToRoleID;     // 0
        int OffsetToRoleName;   // 1

        public RoleMapper(SqlDataReader reader)
        {
            OffsetToRoleID = reader.GetOrdinal("RoleID");
            Assert(0 == OffsetToRoleID, $"RoleID is {OffsetToRoleID} instead of 0 as expected");
            OffsetToRoleName = reader.GetOrdinal("RoleName");
            Assert(1 == OffsetToRoleName, $"RoleName is {OffsetToRoleName} instead of 1 as expected");
        }

        public RoleDAL ToRole(SqlDataReader reader)
        {
            RoleDAL proposedReturnValue = new RoleDAL();
            proposedReturnValue.RoleID = reader.GetInt32(OffsetToRoleID);
            proposedReturnValue.RoleName = reader.GetString(OffsetToRoleName);
            return proposedReturnValue;
        }
    }
}
