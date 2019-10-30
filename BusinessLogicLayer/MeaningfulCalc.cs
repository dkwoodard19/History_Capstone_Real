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
        public string CivName { get; set; }
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
                int DOB = int.Parse(this.FigureDOB.ToString("yyyyMMdd"));       //found on stackoverflow
                int DOD = int.Parse(this.FigureDOD.ToString("yyyyMMdd"));
                return (DOD - DOB) / 10000;
            }
        }

        public MeaningfulFigure(FigureBLL figure)
        {
            FigureID = figure.FigureID;
            FigureName = figure.FigureName;
            FigureDOB = figure.FigureDOB;
            FigureDOD = figure.FigureDOD;
            CivID = figure.CivID;
            CivName = figure.CivName;
        }
        
    }
    public class MeaningfulCalc
    {
        public List<MeaningfulFigure> FiguresToMeaningfulFigures(List<FigureBLL> figures)
        {
            List<MeaningfulFigure> returnvalue = new List<MeaningfulFigure>();
            foreach(var i in figures)
            {
                MeaningfulFigure mf = new MeaningfulFigure(i);      //mf means meaningful figures get your mind out the gutter
                returnvalue.Add(mf);
            }
            // do work to convert FigureBLL to MeaningfulFigure
            // return the answer
            return returnvalue;
        }

        public List<FigureStats> Calc(List<MeaningfulFigure> figures)
        {
            List<FigureStats> proposedReturnValue = new List<FigureStats>();
            IOrderedEnumerable<MeaningfulFigure> sorted = figures.OrderBy(x => x.CivID);
            IEnumerable<IGrouping<int, MeaningfulFigure>> group = figures.GroupBy(x => x.CivID);
            foreach (IGrouping<int, MeaningfulFigure> x in group)
            {
               FigureStats fs = new FigureStats(); 
               fs.AverageAge = x.Average(y => y.Age);
               fs.MinAge = x.Min(y => y.Age);
               fs.MaxAge = x.Max(y => y.Age);
                fs.CivID = x.Key;
                fs.CivName = x.ToList()[0].CivName;
               proposedReturnValue.Add(fs);
                // get all the stats
                // put them in a FigureStats
                // add it to the list
            }
            return proposedReturnValue;
          
        }
    }
}
