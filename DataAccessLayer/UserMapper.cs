using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class UserMapper : Mapper
    {
        int OffsetToUserID;     // 0
        int OffsetToUserName;   // 1
        int OffsetToEmail;      // 2
        int OffsetToHash;       // 3
        int OffsetToSalt;       // 4
        int OffsetToRoleID;     // 5
        int OffsetToRoleName;   // 6

        public UserMapper(SqlDataReader reader)
        {
            OffsetToUserID = reader.GetOrdinal("UserID");
            Assert(0 == OffsetToUserID, $"UserID is {OffsetToUserID} instead of 0 as expected");
            OffsetToUserName = reader.GetOrdinal("UserName");
            Assert(1 == OffsetToUserName, $"UserName is {OffsetToUserName} instead of 1 as expected");
            OffsetToEmail = reader.GetOrdinal("Email");
            Assert(2 == OffsetToEmail, $"Email is {OffsetToEmail} instead of 2 as expected");
            OffsetToHash = reader.GetOrdinal("Hash");
            Assert(3 == OffsetToHash, $"Hash is {OffsetToHash} instead of 3 as expected");
            OffsetToSalt = reader.GetOrdinal("Salt");
            Assert(4 == OffsetToSalt, $"Salt is {OffsetToSalt} instead of 4 as expected");
            OffsetToRoleID = reader.GetOrdinal("RoleID");
            Assert(5 == OffsetToRoleID, $"RoleID is {OffsetToRoleID} instead of 5 as expected");
            OffsetToRoleName = reader.GetOrdinal("RoleName");
            Assert(6 == OffsetToRoleName, $"RoleName is {OffsetToRoleName} instead of 6 as expected");
        }

        public UserDAL ToUser(SqlDataReader reader)
        {
            UserDAL proposedReturnValue = new UserDAL();
            proposedReturnValue.UserID = reader.GetInt32(OffsetToUserID);
            proposedReturnValue.UserName = reader.GetString(OffsetToUserName);
            proposedReturnValue.Email = reader.GetString(OffsetToEmail);
            proposedReturnValue.Hash = reader.GetString(OffsetToHash);
            proposedReturnValue.Salt = reader.GetString(OffsetToSalt);
            proposedReturnValue.RoleID = reader.GetInt32(OffsetToRoleID);
            proposedReturnValue.RoleName = reader.GetString(OffsetToRoleName);
            return proposedReturnValue;
        }
    }
}
