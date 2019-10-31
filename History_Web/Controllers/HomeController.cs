using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using Logger_Project;

namespace History_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("AUTHUserName");
            Session.Remove("AUTHRoles");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                LoginModel Lm = new LoginModel();
                Lm.message = TempData["message"]?.ToString() ?? "";
                Lm.ReturnURL = TempData["ReturnURL"]?.ToString() ?? @"~/Home";
                Lm.UserName = "username";
                Lm.Password = "password";
                return View(Lm);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            try
            {
                // logic to authenticate user with information in login goes here
                using (ContextBLL dtr = new ContextBLL())
                {
                    UserBLL user = dtr.UserFindByUserName(login.UserName);
                    if (user == null)
                    {
                        login.message = $"The UserName '{login.UserName}' does not exist in the database";
                        return View(login);
                    }
                    string actual = user.Hash;
                    string potential = login.Password + user.Salt;
                    bool validuser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual, potential);
                    string validationType = $"ClearText:({user.UserID})";
                    if (!validuser)
                    {
                        potential = login.Password + user.Salt;
                        try
                        {
                            validuser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual, potential);
                            validationType = $"HASHED:({user.UserID})";
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(ex);
                            validuser = false;
                        }
                    }
                    if (validuser)
                    {
                        Session["AUTHUserName"] = user.UserName;
                        Session["AUTHRoles"] = user.RoleID;
                        Session["AUTHTYPE"] = validationType;
                        return Redirect(login.ReturnURL);
                    }
                    login.message = "UserName or password was incorrect";
                    return View(login);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error", ex);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            try
            {
                RegistrationModel Rm = new RegistrationModel();
                Rm.Email = "email";
                Rm.UserName = "username";
                Rm.Password = "password";
                Rm.PasswordAgain = "passwordagain";
                Rm.Message = "";
                return View();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel register)
        {
            try
            {
                using (ContextBLL dtr = new ContextBLL())
                {
                    UserBLL email = dtr.UserFindByEmail(register.Email);
                    if (email != null)
                    {
                        register.Message = $"The Email address '{register.Email}' is already in use";
                        return View(register);
                    }
                    UserBLL user = dtr.UserFindByUserName(register.UserName);
                    if (user != null)
                    {
                        register.Message = $"The UserName '{register.UserName}' is already in use";
                        return View(register);
                    }
                    user = new UserBLL();
                    user.Email = register.Email;
                    user.UserName = register.UserName;
                    user.Salt = System.Web.Helpers.Crypto.GenerateSalt(Constants.SaltSize);
                    user.Hash = System.Web.Helpers.Crypto.HashPassword(register.Password + user.Salt);
                    user.RoleID = Constants.DefaultRoleID;

                    dtr.UserCreate(user);
                    Session["AUTHUserName"] = user.UserName;
                    Session["AUTHEmail"] = user.Email;
                    Session["AUTHRoles"] = user.RoleID;
                    Session["AUTHTYPE"] = "HASHED";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return View("Error");
            }
        }
    }

    //public ActionResult Hash()
    //{
    //    if (!User.Identity.IsAuthenticated)
    //    {
    //        return View("NotLoggedIn");
    //    }
    //    if (User.Identity.AuthenticationType.StartsWith("HASHED"))
    //    {
    //        return View("AlreadyHashed");
    //    }
    //    if (User.Identity.AuthenticationType.StartsWith("IMPERSONATED"))
    //    {
    //        return View("ActionNotAllowed");
    //    }
    //    using (ContextBLL dtr = new ContextBLL())
    //    {
    //        UserBLL email = dtr.UserFindByEmail(User.Identity.Name);
    //        UserBLL user = dtr.UserFindByUserName(User.Identity.Name);
    //        if (email == null)
    //        {
    //            Exception ex = new Exception($"The Email '{User.Identity.Name}' does not exist in the database");
    //            return View("Error", ex);
    //        }
    //        if (user == null)
    //        {
    //            Exception ex = new Exception($"The UserName '{User.Identity.Name}' does not exist in the database");
    //            return View("Error", ex);
    //        }
    //    }
    //}
}
