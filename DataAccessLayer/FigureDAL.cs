using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FigureDAL
    {
        public int FigureID { get; set; }
        public string FigureName { get; set; }
        public DateTime FigureDOB { get; set; }
        public DateTime FigureDOD { get; set; }
        public int CivID { get; set; }
        public string CivName { get; set; }
    }
}
