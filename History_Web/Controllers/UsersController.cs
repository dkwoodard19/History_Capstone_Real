using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace History_Web.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            List<UserBLL> items = null;
            using (ContextBLL dtr = new ContextBLL())
            {
                items = dtr.UsersGetAll(0, 100);
            }
            return View(items);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            UserBLL user;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    user = dtr.UserFindByID(id);
                    if (null == user)
                    {
                        return View("Item Not Found");
                    }
                }
            }
            catch
            {
                return View("Error");
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            {
                ContextBLL dtr = new ContextBLL();
                List<SelectListItem> items = new List<SelectListItem>();
                ViewBag.ListItems = items;
                UserBLL newUser = new UserBLL();                
                newUser.UserID = 0;
                newUser.UserName = null;
                newUser.Email = null;
                newUser.Hash = null;
                newUser.Salt = null;
                newUser.RoleID = 0;
                List<RoleBLL> roles = dtr.RolesGetAll(0, 100);
                foreach (RoleBLL role in roles)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = role.RoleName;
                    item.Value = role.RoleID.ToString();
                    items.Add(item);
                }
                return View(newUser);
            }
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(UserBLL usercreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(usercreate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.UserCreate(usercreate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            { 
                return View("Error", Ex);
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            UserBLL user;
            List<SelectListItem> items = new List<SelectListItem>();
            ViewBag.ListItems = items;
            using (ContextBLL dtr = new ContextBLL())
            {
                user = dtr.UserFindByID(id);
                List<RoleBLL> roles = dtr.RolesGetAll(0, 100);
                foreach (RoleBLL role in roles)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = role.RoleName;
                    item.Value = role.RoleID.ToString();
                    items.Add(item);
                }
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(UserBLL userUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(userUpdate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.UserUpdateJust(userUpdate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            UserBLL deleteUser;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    deleteUser = dtr.UserFindByID(id);
                    if (null == deleteUser)
                    {
                        return View("Item not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return View(deleteUser);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, UserBLL userdelete)
        {
            using (ContextBLL dtr = new ContextBLL())
            {
                dtr.UserDelete(id);
            }
            return RedirectToAction("Index");
        }
    }
}
