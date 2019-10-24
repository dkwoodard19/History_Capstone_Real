using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CivilizationDAL
    {
        public int CivID { get; set; }
        public string CivName { get; set; }
        public DateTime CivStart { get; set; }
        public DateTime CivEnd { get; set; }
    }
}
