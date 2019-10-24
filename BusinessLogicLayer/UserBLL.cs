using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserBLL
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public UserBLL(DataAccessLayer.UserDAL user)
        {
            UserID = user.UserID;
            UserName = user.UserName;
            Email = user.Email;
            Hash = user.Hash;
            Salt = user.Salt;
            RoleID = user.RoleID;
            RoleName = user.RoleName;
        }
        public UserBLL()
        {
            // default constructor(ctor) is REQUIRED for MVC
        }
    }
}
