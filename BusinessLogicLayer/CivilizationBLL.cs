using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CivilizationBLL
    {
        public int CivID { get; set; }
        public string CivName { get; set; }
        public DateTime CivStart { get; set; }
        public DateTime CivEnd { get; set; }

        public CivilizationBLL(DataAccessLayer.CivilizationDAL civ)
        {
            CivID = civ.CivID;
            CivName = civ.CivName;
            CivStart = civ.CivStart;
            CivEnd = civ.CivEnd;
        }

        public CivilizationBLL()
        {
            // default constructor(ctor) is REQUIRED for MVC
        }
    }
}
