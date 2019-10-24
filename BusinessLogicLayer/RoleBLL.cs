using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class RoleBLL
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public RoleBLL(DataAccessLayer.RoleDAL role)
        {
            RoleID = role.RoleID;
            RoleName = role.RoleName;
        }

        public RoleBLL()
        {
            // default constructor(ctor) is REQUIRED for MVC
        }
    }
}
