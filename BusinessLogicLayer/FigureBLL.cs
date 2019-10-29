using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class FigureBLL
    {
        public int FigureID { get; set; }
        public string FigureName { get; set; }
        public DateTime FigureDOB { get; set; }
        public DateTime FigureDOD { get; set; }
        [Display(Name = "Civilization")] public int CivID { get; set; }
        public string CivName { get; set; }

        public FigureBLL(DataAccessLayer.FigureDAL figure)
        {
            FigureID = figure.FigureID;
            FigureName = figure.FigureName;
            FigureDOB = figure.FigureDOB;
            FigureDOD = figure.FigureDOD;
            CivID = figure.CivID;
            CivName = figure.CivName;
        }

        public FigureBLL()
        {
            // default constructor(ctor) is REQUIRED for MVC
        }
    }
}
