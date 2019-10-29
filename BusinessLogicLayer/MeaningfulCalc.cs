using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{

    public class FigureStats
    {
        public int CivID { get; set; }
        public string CivName {get; set;}
        public double AverageAge { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
    }

    public class MeaningfulFigure : FigureBLL
    {
        public int Age
        {
            get
            {
                int DOB = int.Parse(this.FigureDOB.ToString("yyyyMMdd"));
                int DOD = int.Parse(this.FigureDOD.ToString("yyyyMMdd"));
                return (DOD - DOB) / 10000;
            }
        }
            
    }
    public class MeaningfulCalc : MeaningfulFigure
    {        
        public List<MeaningfulFigure> Calc(List<FigureBLL> figures)
        {
            List<MeaningfulFigure> returnvalue = null;
            // do work to convert FigureBLL to MeaningfullFigure
            // call second fucnction
            // return the answer
            return returnvalue;
        }

        public List<FigureStats> Calc(List<MeaningfulFigure> figures)
        {
            List<FigureStats> proposedReturnValue = new List<FigureStats>();
            //var sorted = figures.OrderBy(x => x.CivID);
            IEnumerable<IGrouping<int, MeaningfulFigure>> group = figures.GroupBy(x => x.CivID);
            foreach (IGrouping<int, MeaningfulFigure> x in group)
            {
               double average= x.Average(y => y.Age);
               int MinAge = x.Min(y => y.Age);
               int MaxAge = x.Max(y => y.Age);
                // get all the stats
                // put them in a FigureStats
                // add it to the list
            }
            return proposedReturnValue;
          
        }
    }
}
