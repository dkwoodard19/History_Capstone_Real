using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace History_Web.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            List<EventBLL> items = null;
            using (ContextBLL dtr = new ContextBLL())
            {
                items = dtr.EventsGetAll(0, 100);
            }
            return View(items);
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
                return View("Error", ex);
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            {
                EventBLL newEvent = new EventBLL();
                newEvent.EventDate = DateTime.Now.Date;
                return View(newEvent);
            }
        }

        // POST: Events/Create
        [HttpPost]
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
            catch (Exception Ex)
            {
                return View("Error", Ex);
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            EventBLL @event;
            using (ContextBLL dtr = new ContextBLL())
            {
                @event = dtr.EventFindByID(id);
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        [HttpPost]
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
                return View("Error", ex);
            }
        }

        // GET: Events/Delete/5
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
                return View("Error", ex);
            }
            return View(deleteEvent);
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, EventBLL eventDelete)
        {
            using (ContextBLL dtr = new ContextBLL())
            {
                dtr.EventDelete(id);
            }
            return RedirectToAction("Index");
        }
    }
}
