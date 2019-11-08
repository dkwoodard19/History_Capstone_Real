using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using Logger_Project;

namespace History_Web.Controllers
{
    [Models.Filter.MustBeInRole(Roles = Constants.Student)]
    public class FiguresController : Controller
    {
        // GET: Figures
        public ActionResult Index()
        {
            try
            {
                List<FigureBLL> items = null;
                using (ContextBLL dtr = new ContextBLL())
                {
                    items = dtr.FiguresGetAll(0, 100);
                }
                return View(items);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        public ActionResult Statistics()        //new action result for my meaningfulcalc
        {
            try
            {
                List<FigureBLL> items = null;
                using (ContextBLL dtr = new ContextBLL())
                {
                    items = dtr.FiguresGetAll(0, 100);
                    MeaningfulCalc mc = new MeaningfulCalc();
                    List<MeaningfulFigure> i = mc.FiguresToMeaningfulFigures(items);
                    List<FigureStats> s = mc.Calc(i);
                    return View("Statistics",s);
                }
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Figures/Details/5
        public ActionResult Details(int id)
        {
            FigureBLL figure;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    figure = dtr.FigureFindByID(id);
                    if (null == figure)
                    {
                        return View("ItemNotFound");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
            return View(figure);
        }

        // GET: Figures/Create
        [Models.Filter.MustBeInRole(Roles = Constants.Historian)]
        public ActionResult Create()
        {
            {
                try
                {
                    using (ContextBLL dtr = new ContextBLL())
                    {
                        List<SelectListItem> items = new List<SelectListItem>();
                        FigureBLL newFigure = new FigureBLL();
                        newFigure.FigureDOB = DateTime.Now.Date;
                        newFigure.FigureDOD = DateTime.Now.Date;
                        List<CivilizationBLL> civs = dtr.CivilizationsGetAll(0, 100);
                        foreach (CivilizationBLL civ in civs)
                        {
                            SelectListItem item = new SelectListItem();
                            item.Text = civ.CivName;
                            item.Value = civ.CivID.ToString();
                            items.Add(item);
                        }
                        ViewData["ListItems"] = items;
                        return View(newFigure);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    return View("Error", ex);
                }
            }
        }

        // POST: Figures/Create
        [HttpPost]
        [Models.Filter.MustBeInRole(Roles = Constants.Historian)]
        public ActionResult Create(FigureBLL figurecreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(figurecreate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.FigureCreate(figurecreate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Figures/Edit/5
        [Models.Filter.MustBeInRole(Roles = Constants.Historian)]
        public ActionResult Edit(int id)
        {
            try
            {
                FigureBLL figure;
                List<SelectListItem> items = new List<SelectListItem>();
                ViewBag.ListItems = items;
                using (ContextBLL dtr = new ContextBLL())
                {
                    figure = dtr.FigureFindByID(id);
                    List<CivilizationBLL> civs = dtr.CivilizationsGetAll(0, 100);
                    foreach (CivilizationBLL civ in civs)
                    {
                        SelectListItem item = new SelectListItem();
                        item.Text = civ.CivName;
                        item.Value = civ.CivID.ToString();
                        items.Add(item);
                    }
                }
                return View(figure);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // POST: Figures/Edit/5
        [HttpPost]
        [Models.Filter.MustBeInRole(Roles = Constants.Historian)]
        public ActionResult Edit(FigureBLL figureUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(figureUpdate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.FiguresUpdateJust(figureUpdate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Figures/Delete/5
        [Models.Filter.MustBeInRole(Roles = Constants.Admin)]
        public ActionResult Delete(int id)
        {
            FigureBLL deleteFigure;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    deleteFigure = dtr.FigureFindByID(id);
                    if (null == deleteFigure)
                    {
                        return View("Item not found");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
            return View(deleteFigure);
        }

        // POST: Figures/Delete/5
        [HttpPost]
        [Models.Filter.MustBeInRole(Roles = Constants.Admin)]
        public ActionResult Delete(int id, FigureBLL figureDelete)
        {
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.FigureDelete(id);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }
    }
}
