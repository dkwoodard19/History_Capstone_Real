using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace History_Web.Models
{
    public class Filter
    {
        public class MustBeLoggedInAttribute : AuthorizeAttribute
        {
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)    //grabs user data and authenticates
                {
                    //Call base class to allow user into the action method
                    base.OnAuthorization(filterContext);
                }
                else                                                                //or send them to Login page
                {
                    string ReturnURL = filterContext.RequestContext.HttpContext.Request.Path.ToString();
                    filterContext.Controller.TempData.Add("Message",
                            $"Please LogIn to view this material.");
                    filterContext.Controller.TempData.Add("ReturnURL", ReturnURL);      //must have returnurl
                    System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
                    dict.Add("Controller", "Home");
                    dict.Add("Action", "Login");
                    filterContext.Result = new RedirectToRouteResult(dict);
                }
            }
        }


        public class MustBeInRoleAttribute : System.Web.Mvc.AuthorizeAttribute

        {
            public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
            {
                if (this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
                {
                    base.OnAuthorization(filterContext);

                }
                else
                {
                    string ReturnURL = filterContext.RequestContext.HttpContext.Request.Path.ToString();
                    filterContext.Controller.TempData.Add("Message",
                                         $"You must be a: {Roles}");
                    filterContext.Controller.TempData.Add("ReturnURL", ReturnURL);
                    System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
                    dict.Add("Controller", "Home");
                    dict.Add("Action", "Login");
                    filterContext.Result = new RedirectToRouteResult(dict);

                }
            }
        }
    }
}