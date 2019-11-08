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
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            try
            {
                List<EventBLL> items = null;
                using (ContextBLL dtr = new ContextBLL())
                {
                    items = dtr.EventsGetAll(0, 100);
                }
                return View(items);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        public ActionResult FigureStats()        //new action result for my meaningfulcalc
        {
            try
            {
                List<EventBLL> items = null;
                using (ContextBLL dtr = new ContextBLL())
                {
                    items = dtr.EventsGetAll(0, 100);
                    EventFigCalc ec = new EventFigCalc();
                    //List<MeaningfulEvents> i = ec.EventCivsToMeaningfulEventCivs(items);
                    //List<MeaningfulEvents> s = ec.EventsFiguresToMeaningfulEventFigures(items);
                    List<FigEventStats> figevents = ec.FigCalc(items);
                    return View("FigureStats", figevents);
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }
        public ActionResult CivStats()
        {
            try
            {
                List<EventBLL> items = null;
                using (ContextBLL dtr = new ContextBLL())
                {
                    items = dtr.EventsGetAll(0, 100);
                    EventCivCalc ec = new EventCivCalc();
                    //List<MeaningfulEvents> i = ec.EventCivsToMeaningfulEventCivs(items);
                    //List<MeaningfulEvents> s = ec.EventsFiguresToMeaningfulEventFigures(items);
                    List<CivEventStats> civevents = ec.CivCalc(items);
                    return View("CivStats", civevents);
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            EventBLL @event;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    @event = dtr.EventFindByID(id);
                    if (null == @event)
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
            return View(@event);
        }

        // GET: Events/Create
        [Models.Filter.MustBeInRole(Roles = Constants.Historian)]
        public ActionResult Create()
        {
            try
            {
                List<SelectListItem> figureitems = new List<SelectListItem>();
                List<SelectListItem> civitems = new List<SelectListItem>();
                ViewBag.FigureItems = figureitems;
                ViewBag.CivilizationItems = civitems;
                using (ContextBLL dtr = new ContextBLL())
                {
                    EventBLL newEvent = new EventBLL();
                    newEvent.EventDate = DateTime.Now.Date;
                    newEvent.FigureID = 0;
                    newEvent.CivID = 0;
                    List<FigureBLL> figures = dtr.FiguresGetAll(0, 100);
                    foreach (FigureBLL figure in figures)
                    {
                        SelectListItem figureitem = new SelectListItem();
                        figureitem.Text = figure.FigureName;
                        figureitem.Value = figure.FigureID.ToString();
                        figureitems.Add(figureitem);
                    }
                    List<CivilizationBLL> civs = dtr.CivilizationsGetAll(0, 100);
                    foreach (CivilizationBLL civ in civs)
                    {
                        SelectListItem civitem = new SelectListItem();
                        civitem.Text = civ.CivName;
                        civitem.Value = civ.CivID.ToString();
                        civitems.Add(civitem);
                    }
                    return View(newEvent);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // POST: Events/Create
        [HttpPost]
        [Models.Filter.MustBeInRole(Roles = Constants.Historian)]
        public ActionResult Create(EventBLL eventCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(eventCreate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.EventCreate(eventCreate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Events/Edit/5
        [Models.Filter.MustBeInRole(Roles = Constants.Historian)]
        public ActionResult Edit(int id)
        {
            try
            {
                EventBLL @event;
                List<SelectListItem> figureitems = new List<SelectListItem>();
                List<SelectListItem> civitems = new List<SelectListItem>();
                ViewBag.FigureItems = figureitems;
                ViewBag.CivilizationItems = civitems;
                using (ContextBLL dtr = new ContextBLL())
                {
                    @event = dtr.EventFindByID(id);
                    List<FigureBLL> figures = dtr.FiguresGetAll(0, 100);
                    foreach (FigureBLL figure in figures)
                    {
                        SelectListItem figureitem = new SelectListItem();
                        figureitem.Text = figure.FigureName;
                        figureitem.Value = figure.FigureID.ToString();
                        figureitems.Add(figureitem);
                    }
                    List<CivilizationBLL> civs = dtr.CivilizationsGetAll(0, 100);
                    foreach (CivilizationBLL civ in civs)
                    {
                        SelectListItem civitem = new SelectListItem();
                        civitem.Text = civ.CivName;
                        civitem.Value = civ.CivID.ToString();
                        civitems.Add(civitem);
                    }
                }
                return View(@event);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // POST: Events/Edit/5
        [HttpPost]
        [Models.Filter.MustBeInRole(Roles = Constants.Historian)]
        public ActionResult Edit(EventBLL eventUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(eventUpdate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.EventUpdateJust(eventUpdate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Events/Delete/5
        [Models.Filter.MustBeInRole(Roles = Constants.Admin)]
        public ActionResult Delete(int id)
        {
            EventBLL deleteEvent;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    deleteEvent = dtr.EventFindByID(id);
                    if (null == deleteEvent)
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
            return View(deleteEvent);
        }

        // POST: Events/Delete/5
        [HttpPost]
        [Models.Filter.MustBeInRole(Roles = Constants.Admin)]
        public ActionResult Delete(int id, EventBLL eventDelete)
        {
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.EventDelete(id);
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
