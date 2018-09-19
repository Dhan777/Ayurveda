using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayurveda.Models;
using Ayurveda.Filters;
namespace Ayurveda.Controllers
{
    public class HomeController : Controller
    {
        DataLayer db = new DataLayer();

        public ActionResult Index()
        {
            return View(new Products());
        }

        [HttpPost]
        public ActionResult Index(Products model, string search)
        {
           /* if (search == "disease") { }
            if (search == "medicine") { }*/
            model.products = db.SearchProducts<Products>(model).ToList();
            return View(model);
        } 

        [SessionTimeout]
        public ActionResult Home()
        {
            Users userobj = new Users();
            userobj.UserID= Session["UserId"].ToString();
            var lst = db.GetUsers<Users>(userobj).FirstOrDefault();
            userobj.Name=Session["Name"].ToString(); 
            userobj.FatherName=Session["FName"].ToString(); 
            userobj.Email=Session["Email"].ToString();
            userobj.Address = Session["Address"].ToString();
            return View(lst);
        }

        [HttpPost]
        public ActionResult SendMessage(string Msg)
        {
            Users model = new Users();
            model.UserID = Session["UserId"].ToString();
            model.message = Msg;
            model.To = "Admin";
            var lst =new DataLayer().SendQuery<Users>(model);
            return Json(lst,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMessage(string To)
        {
            try
            {
                Users model = new Users();
                model.UserID = Session["UserId"].ToString().ToLower();
                var lst = new DataLayer().GetQuery<Users>(model);
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception E)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
