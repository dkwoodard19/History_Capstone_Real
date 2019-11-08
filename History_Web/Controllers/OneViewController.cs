using BusinessLogicLayer;
using Logger_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace History_Web
{
    [Models.Filter.MustBeInRole(Roles = Constants.Historian)]
    public class OneViewController : Controller
    {
        // GET: OneView/Create
        public ActionResult Create()
        {
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    OneViewModel ov = new OneViewModel();
                    List<CivilizationBLL> items = dtr.CivilizationsGetAll(0, 100);
                    //CivilizationBLL civ = dtr.CivilizationFindByID(id);
                    ov.Civs = new SelectList(items, "CivID", "CivName");
                    items.Insert(0, new CivilizationBLL() { CivID = -1, CivName = "Select an exsiting Civilization..." });
                    items.Insert(0, new CivilizationBLL() { CivID = 0, CivName = "Create a new Civilization..." });
                    ov.FigureName = "FigureName";
                    ov.FigureDOB = DateTime.Now.Date;
                    ov.FigureDOD = DateTime.Now.Date;
                    ov.CivName = "CivName";
                    ov.CivID = -1;
                    //if (civ != null)
                    //{

                    //    ov.NewCivName = "";
                    //}
                    return View(ov);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // POST: OneView/Create
        [HttpPost]
        public ActionResult Create(OneViewModel ov)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL dtr = new ContextBLL())
                {
                    if (!ModelState.IsValid)
                    {
                        List<CivilizationBLL> items = dtr.CivilizationsGetAll(0, 100);
                        //CivilizationBLL civ = dtr.CivilizationFindByID();
                        ov.Civs = new SelectList(items, "CivID", "CivName");
                        items.Insert(0, new CivilizationBLL() { CivID = -1, CivName = "Select an exsiting Civilization..." });
                        items.Insert(0, new CivilizationBLL() { CivID = 0, CivName = "Create a new Civilization..." });
                    }
                    if (ov.CivID > 0)
                    {
                        ov.CivID = dtr.FigureCreate(ov.FigureID, ov.FigureName, ov.FigureDOB, ov.FigureDOD, ov.CivID);
                        //ov.CivID = dtr.CivilizationCreate(ov.CivID, ov.NewCivName, ov.CivStart, ov.CivEnd);
                    }
                    else
                    {

                        int CivID = dtr.CivilizationCreate(ov.CivID, ov.NewCivName, ov.CivStart, ov.CivEnd);

                        int FigureID = dtr.FigureCreate(ov.FigureID, ov.FigureName, ov.FigureDOB, ov.FigureDOD, CivID);
                    }
                }

                return RedirectToAction("Index", "Figures");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

    }
}
