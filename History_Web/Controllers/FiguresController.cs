using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace History_Web.Controllers
{
    public class FiguresController : Controller
    {
        // GET: Figures
        public ActionResult Index()
        {
            List<FigureBLL> items = null;
            using (ContextBLL dtr = new ContextBLL())
            {
                items = dtr.FiguresGetAll(0, 100);
            }
            return View(items);
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
            catch(Exception ex)
            {
                return View("Error", ex);
            }
            return View(figure);
        }

        // GET: Figures/Create
        public ActionResult Create()
        {
            {
                ContextBLL dtr = new ContextBLL();
                List<SelectListItem> items = new List<SelectListItem>();
                ViewBag.ListItems = items;
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
                return View(newFigure);
            }
        }

        // POST: Figures/Create
        [HttpPost]
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
            catch (Exception Ex)
            {
                return View("Error", Ex);
            }
        }

        // GET: Figures/Edit/5
        public ActionResult Edit(int id)
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

        // POST: Figures/Edit/5
        [HttpPost]
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
                return View("Error", ex);
            }
        }

        // GET: Figures/Delete/5
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
                return View("Error", ex);
            }
            return View(deleteFigure);
        }

        // POST: Figures/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FigureBLL figureDelete)
        {
            using (ContextBLL dtr = new ContextBLL())
            {
                dtr.FigureDelete(id);
            }
            return RedirectToAction("Index");
        }
    }
}
