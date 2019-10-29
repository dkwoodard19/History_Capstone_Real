using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using Logger_Project;

namespace History_Web.Controllers
{
    public class CivilizationsController : Controller
    {
        // GET: Civilizations
        public ActionResult Index()
        {
            try
            {
                List<CivilizationBLL> items = null;
                using (ContextBLL dtr = new ContextBLL())
                {
                    items = dtr.CivilizationsGetAll(0, 100);
                }
                return View(items);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Civilizations/Details/5
        public ActionResult Details(int id)
        {
            CivilizationBLL civ;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    civ = dtr.CivilizationFindByID(id);
                    if (null == civ)
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
            return View(civ);
        }

        // GET: Civilizations/Create
        public ActionResult Create()
        {
            try
            {

                CivilizationBLL newCiv = new CivilizationBLL();
                newCiv.CivStart = DateTime.Now.Date;
                newCiv.CivEnd = DateTime.Now.Date;
                return View(newCiv);

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // POST: Civilizations/Create
        [HttpPost]
        public ActionResult Create(CivilizationBLL civCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(civCreate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.CivilizationCreate(civCreate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                Logger.Log(Ex);
                return View("Error", Ex);
            }
        }

        // GET: Civilizations/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                CivilizationBLL civ;
                using (ContextBLL dtr = new ContextBLL())
                {
                    civ = dtr.CivilizationFindByID(id);
                }
                return View(civ);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // POST: Civilizations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CivilizationBLL civUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(civUpdate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.CivilizationsUpdateJust(civUpdate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Civilizations/Delete/5
        public ActionResult Delete(int id)
        {
            CivilizationBLL deleteCiv;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    deleteCiv = dtr.CivilizationFindByID(id);
                    if (null == deleteCiv)
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
            return View(deleteCiv);
        }

        // POST: Civilizations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CivilizationBLL civDelete)
        {
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.CivilizationDelete(id);
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
