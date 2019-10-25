﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace History_Web.Controllers
{
    public class ArticlesController : Controller
    {
        // GET: Articles
        public ActionResult Index()
        {
            List<ArticleBLL> items = null;
            using (ContextBLL dtr = new ContextBLL())
            {
                items = dtr.ArticlesGetAll(0, 100);
            }
            return View(items);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int id)
        {
            ArticleBLL article;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    article = dtr.ArticleFindByID(id);
                    if (null == article)
                    {
                        return View("ItemNotFound");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            {
                ContextBLL dtr = new ContextBLL();
                List<SelectListItem> useritems = new List<SelectListItem>();
                List<SelectListItem> eventitems = new List<SelectListItem>();
                ViewBag.UserItems = useritems;
                ViewBag.EventItems = eventitems;
                ArticleBLL newArticle = new ArticleBLL();
                newArticle.ArticleText = null;
                newArticle.UserID = 0;
                newArticle.EventID = 0;
                List<UserBLL> users = dtr.UsersGetAll(0, 100);
                foreach (UserBLL user in users)
                {
                    SelectListItem useritem = new SelectListItem();
                    useritem.Text = user.UserName;
                    useritem.Value = user.UserID.ToString();
                    useritems.Add(useritem);
                }
                List<EventBLL> events = dtr.EventsGetAll(0, 100);
                foreach (EventBLL @event in events)
                {
                    SelectListItem eventitem = new SelectListItem();
                    eventitem.Text = @event.EventName;
                    eventitem.Value = @event.EventID.ToString();
                    eventitems.Add(eventitem);
                }
                return View(newArticle);
            }
        }

        // POST: Articles/Create
        [HttpPost]
        public ActionResult Create(ArticleBLL articlecreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(articlecreate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.ArticleCreate(articlecreate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                return View("Error", Ex);
            }
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int id)
        {
            ArticleBLL article;
            List<SelectListItem> useritems = new List<SelectListItem>();
            List<SelectListItem> eventitems = new List<SelectListItem>();
            ViewBag.UserItems = useritems;
            ViewBag.EventItems = eventitems;
            using (ContextBLL dtr = new ContextBLL())
            {
                article = dtr.ArticleFindByID(id);
                List<UserBLL> users = dtr.UsersGetAll(0, 100);
                foreach (UserBLL user in users)
                {
                    SelectListItem useritem = new SelectListItem();
                    useritem.Text = user.UserName;
                    useritem.Value = user.UserID.ToString();
                    useritems.Add(useritem);
                }
                List<EventBLL> events = dtr.EventsGetAll(0, 100);
                foreach (EventBLL @event in events)
                {
                    SelectListItem eventitem = new SelectListItem();
                    eventitem.Text = @event.EventName;
                    eventitem.Value = @event.EventID.ToString();
                    eventitems.Add(eventitem);
                }
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        public ActionResult Edit(ArticleBLL articleUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(articleUpdate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.ArticlesUpdateJust(articleUpdate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int id)
        {
            ArticleBLL deleteArticle;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    deleteArticle = dtr.ArticleFindByID(id);
                    if (null == deleteArticle)
                    {
                        return View("Item not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return View(deleteArticle);
        }

        // POST: Articles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (ContextBLL dtr = new ContextBLL())
            {
                dtr.ArticleDelete(id);
            }
            return RedirectToAction("Index");
        }
    }
}
