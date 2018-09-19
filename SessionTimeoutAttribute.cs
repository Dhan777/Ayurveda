using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ayurveda.Filters
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoSessionTimeoutAttribute), false).Any())
            {
                return;
            }
            if (filterContext.HttpContext.Request.UrlReferrer == null || filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", area = "" }));
            }
            else
            {
                HttpContext ctx = HttpContext.Current;
                if (String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["UserId"])) &&
                String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["Name"])) &&
                String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["FName"])) &&
                String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["Email"])) &&
                String.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["Address"]))
                  )
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", area = "" }));
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", area = "" }));
                    }
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
    public class NoSessionTimeoutAttribute : ActionFilterAttribute
    {
    }
}