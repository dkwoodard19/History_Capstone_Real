using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tests
{
    [TestClass()]
    public class MeaningfulCalcTests
    {
        #region MeaningfulFigureTests
        List<MeaningfulFigure> SampleFigures(int count)
        {
            List<MeaningfulFigure> proposedReturnValue = new List<MeaningfulFigure>();
            for (int i = 0; i < count; i++)
            {
                FigureBLL f = new FigureBLL();
                f.FigureID = i;
                f.FigureDOB = new DateTime(1900 + i * 5, 1, 1);
                f.FigureDOD = new DateTime(1950 + i * 5, 1, 1);
                f.CivID = i;
                proposedReturnValue.Add(new MeaningfulFigure(f));
            }
            return proposedReturnValue;
        }
        [TestMethod()]
        public void Should_ReturnNegative_WhenDODisLessThanDOB_InMeaningfulFig()
        {
            //arrange create instance of the thing that contains the 'Age' property
            FigureBLL f = new FigureBLL();
            MeaningfulFigure itemTotest = new MeaningfulFigure(f);
            itemTotest.FigureDOB = new DateTime(2019, 10, 31);
            itemTotest.FigureDOD = new DateTime(2018, 10, 31);
            int ExpectedAge = -1;
            //act 
            int ActualAge = itemTotest.Age;
            //assert
            Assert.AreEqual(ExpectedAge, ActualAge);
        }
        [TestMethod()]
        public void Should_ThrowException_WhenFiguresNull_InMeaningfulFig()
        {
            //arrange create instance of the thing that contains the 'Age' property
            //FigureBLL fs = new FigureBLL(); 
           // MeaningfulFigure mf = new MeaningfulFigure(fs);
            List<MeaningfulFigure> figures = SampleFigures(0); //figures from sample figures = 0
            // from stack overflow 741029
            //assert
            Assert.ThrowsException<System.InvalidOperationException>(()=>figures.Average(x => x.Age));
        }

        [TestMethod()]
        public void Should_AvgNonZero_WhenFiguresNonNull_InMeaningfulFig()
        {
            //arrange create instance of the thing that contains the 'Age' property
            //FigureBLL fs = new FigureBLL();
            //MeaningfulFigure mf = new MeaningfulFigure(fs);
            List<MeaningfulFigure> figures = SampleFigures(3); //figures from sample figures = 3
            double ExpectedAvg = 0;
            //act 
            double ActualAvg = figures.Average(x => x.Age);
            //assert
            Assert.AreNotEqual(ExpectedAvg, ActualAvg);
        }
        #endregion

        #region MeaningfulCivTests
        List<MeaningfulCiv> SampleCivs(int count)
        {
            List<MeaningfulCiv> proposedReturnValue = new List<MeaningfulCiv>();
            for (int i = 0; i < count; i++)
            {
                CivilizationBLL c = new CivilizationBLL();
                c.CivID = i;
                c.CivStart = new DateTime(1900 + i * 5, 1, 1);
                c.CivEnd = new DateTime(2000 + i * 5, 1, 1);
                proposedReturnValue.Add(new MeaningfulCiv(c));
            }
            return proposedReturnValue;
        }
        [TestMethod()]
        public void Should_ReturnNegative_WhenEndisLessThanStart_InMeaningfulCiv()
        {
            //arrange create instance of the thing that contains the 'Age' property
            CivilizationBLL c = new CivilizationBLL();
            MeaningfulCiv itemTotest = new MeaningfulCiv(c);
            itemTotest.CivStart = new DateTime(2019, 10, 31);
            itemTotest.CivEnd = new DateTime(2018, 10, 31);
            int ExpectedAge = -1;
            //act 
            int ActualAge = itemTotest.Age;
            //assert
            Assert.AreEqual(ExpectedAge, ActualAge);
        }

        [TestMethod()]
        public void Should_ThrowException_WhenCivsNull_InMeaningfulCiv()
        {
            //arrange create instance of the thing that contains the 'Age' property
            //FigureBLL fs = new FigureBLL(); 
            // MeaningfulFigure mf = new MeaningfulFigure(fs);
            List<MeaningfulCiv> civs = SampleCivs(0); //figures from sample figures = 0
            // from stack overflow 741029
            //assert
            Assert.ThrowsException<System.InvalidOperationException>(() => civs.Average(x => x.Age));
        }
        [TestMethod()]
        public void Should_AvgNonZero_WhenCivsNonNull_InMeaningfulCiv()
        {
            //arrange create instance of the thing that contains the 'Age' property
            //FigureBLL fs = new FigureBLL();
            //MeaningfulFigure mf = new MeaningfulFigure(fs);
            List<MeaningfulCiv> civs = SampleCivs(3); //figures from sample figures = 3
            double ExpectedAvg = 0;
            //act 
            double ActualAvg = civs.Average(x => x.Age);
            //assert
            Assert.AreNotEqual(ExpectedAvg, ActualAvg);
        }
        #endregion
    }

}