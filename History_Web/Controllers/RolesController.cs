using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace History_Web.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            List<RoleBLL> items = null;
            using (ContextBLL dtr = new ContextBLL())
            {
                items = dtr.RolesGetAll(0, 100);
            }
            return View(items);
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
            catch
            {
                return View("Error");
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            RoleBLL newRole = new RoleBLL();
            newRole.RoleID = 0;
            return View(newRole);
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
                ViewBag.Excepction = Ex;
                return View("Error");
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            RoleBLL role;

            using (ContextBLL dtr = new ContextBLL())
            {
                role = dtr.RoleFindByID(id);
            }

            return View(role);
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
                ViewBag.Exception = ex;
                return View("Error");                    
            }
            return View(deleteRole);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, RoleBLL roledelete)
        {
                using (ContextBLL dtr = new ContextBLL())
                {
                    dtr.RoleDelete(id);
                }
                return RedirectToAction("Index");
            

        }
    }
}
