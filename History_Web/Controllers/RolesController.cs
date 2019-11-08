using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using Logger_Project;

namespace History_Web.Controllers
{
    public class RolesController : Controller
    {
        [Models.Filter.MustBeInRole(Roles = Constants.Student)]
        // GET: Roles
        public ActionResult Index()
        {
            try
            {
                List<RoleBLL> items = null;
                using (ContextBLL dtr = new ContextBLL())
                {
                    items = dtr.RolesGetAll(0, 100);
                }
                return View(items);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Roles/Details/5
        [Models.Filter.MustBeInRole(Roles = Constants.Admin)]
        public ActionResult Details(int id)
        {
            RoleBLL role;
            try
            {
                using(ContextBLL dtr = new ContextBLL())
                {
                    role = dtr.RoleFindByID(id);
                    if(null == role)
                    {
                        return View("Item Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
            return View(role);
        }

        // GET: Roles/Create
        [Models.Filter.MustBeInRole(Roles = Constants.Developer)]
        public ActionResult Create()
        {
            try
            {
                RoleBLL newRole = new RoleBLL();
                newRole.RoleID = 0;
                return View(newRole);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // POST: Roles/Create
        [Models.Filter.MustBeInRole(Roles = Constants.Developer)]
        [HttpPost]
        public ActionResult Create(RoleBLL rolecreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(rolecreate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.RoleCreate(rolecreate);
                }
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // GET: Roles/Edit/5
        [Models.Filter.MustBeInRole(Roles = Constants.Developer)]
        public ActionResult Edit(int id)
        {
            try
            {
                RoleBLL role;

                using (ContextBLL dtr = new ContextBLL())
                {
                    role = dtr.RoleFindByID(id);
                }

                return View(role);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [Models.Filter.MustBeInRole(Roles = Constants.Developer)]
        public ActionResult Edit(BusinessLogicLayer.RoleBLL roleUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(roleUpdate);
                }
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.RoleUpdateJust(roleUpdate);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }

        }

        // GET: Roles/Delete/5
        [Models.Filter.MustBeInRole(Roles = Constants.Developer)]
        public ActionResult Delete(int id)
        {
            RoleBLL deleteRole;
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    deleteRole = dtr.RoleFindByID(id);
                    if (null == deleteRole)
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
            return View(deleteRole);
        }

        // POST: Roles/Delete/5
        [Models.Filter.MustBeInRole(Roles = Constants.Developer)]
        [HttpPost]
        public ActionResult Delete(int id, RoleBLL roledelete)
        {
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.RoleDelete(id);
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
