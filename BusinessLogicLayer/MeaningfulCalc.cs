﻿using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    #region FigureStats
    public class FigureStats
    {
        public int CivID { get; set; }
        public string CivName { get; set; }
        public double AverageAge { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
    }   

    public class MeaningfulFigure : FigureBLL       //parent child relationship like the mapper class
    {
        public int Age
        {
            get     //only a 'get' because it wont be set at all; its used to calculate what i actually want
            {
                int DOB = int.Parse(this.FigureDOB.ToString("yyyyMMdd"));       //found on stackoverflow to calculate age
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
        public List<MeaningfulFigure> FiguresToMeaningfulFigures(List<FigureBLL> figures)   //feeds to the method the data in figurebll and creates new list with the figures' age
        {
            List<MeaningfulFigure> returnvalue = new List<MeaningfulFigure>();
            foreach(FigureBLL i in figures)
            {
                MeaningfulFigure mf = new MeaningfulFigure(i);      //mf means meaningful figures get your mind out the gutter
                returnvalue.Add(mf);
            }
            // do work to convert FigureBLL to MeaningfulFigure
            // return the answer
            return returnvalue;
        }

        public List<FigureStats> Calc(List<MeaningfulFigure> figures)       //uses the new list created in the above method to do the actual calculations and 'sets' data into the FigureStats class
        {
            List<FigureStats> proposedReturnValue = new List<FigureStats>();
            IOrderedEnumerable<MeaningfulFigure> sorted = figures.OrderBy(x => x.CivID);        
            IEnumerable<IGrouping<int, MeaningfulFigure>> group = figures.GroupBy(x => x.CivID);
            foreach (IGrouping<int, MeaningfulFigure> x in group)
            {
               FigureStats fs = new FigureStats(); //calculates and stores this information in my figurestats class above
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
    #endregion
    public class CivStats
    {
        public double AverageAge { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
    }
    public class MeaningfulCiv : CivilizationBLL
    {
        public int Age
        {
            get
            {
                int Start = int.Parse(this.CivStart.ToString("yyyyMMdd"));
                int End = int.Parse(this.CivEnd.ToString("yyyyMMdd"));
                return (End - Start) / 10000;
            }
        }
        public MeaningfulCiv(CivilizationBLL civ)
        {
            CivID = civ.CivID;
            CivName = civ.CivName;
            CivStart = civ.CivStart;
            CivEnd = civ.CivEnd;
        }       
    }
    public class CivCalc
    {
        public List<MeaningfulCiv> CivsToMeaningfulCivs(List<CivilizationBLL> civs)
        {
            List<MeaningfulCiv> proposedreturnValue = new List<MeaningfulCiv>();
            foreach (CivilizationBLL i in civs)
            {
                MeaningfulCiv mc = new MeaningfulCiv(i);
                proposedreturnValue.Add(mc);
            }
            return proposedreturnValue;
        }
        public CivStats Calc(List<MeaningfulCiv> civs)
        {
                CivStats cs = new CivStats();
                cs.AverageAge = civs.Average(y => y.Age);
                cs.MinAge = civs.Min(y => y.Age);
                cs.MaxAge = civs.Max(y => y.Age);            
                return cs;
        }
    }
}
