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
                return View("Error");
            }
            return View(role);
        }

        // GET: Roles/Create
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
            catch (Exception Ex)
            {
                Logger.Log(Ex);
                return View("Error");
            }
        }

        // GET: Roles/Edit/5
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
            catch (Exception Ex)
            {
                Logger.Log(Ex);
                return View("Error", Ex);
            }
            
        }

        // GET: Roles/Delete/5
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
                return View("Error");                    
            }
            return View(deleteRole);
        }

        // POST: Roles/Delete/5
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
