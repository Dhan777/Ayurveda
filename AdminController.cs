using Ayurveda.Filters;
using Ayurveda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ayurveda.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [SessionTimeoutAttribute]
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        [SessionTimeoutAttribute]
        public ActionResult SendMessage(string Msg,string To)
        {
            Users model = new Users();
            model.UserID = Session["UserId"].ToString();
            model.message = Msg;
            model.To = To;
            var lst = new DataLayer().SendQuery<Users>(model);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [SessionTimeoutAttribute]
        public ActionResult GetMessage(string To)
        {
            Users model = new Users();
            model.UserID = Session["UserId"].ToString().ToLower().ToLower();
            var lst = new DataLayer().GetQuery<Users>(model);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadProducts()
        {
            return View();
        }

        [HttpPost]
        [SessionTimeoutAttribute]
        public ActionResult UploadProducts(Products model)
        {
            if(!(model.File.ContentLength>0))
            {
                return View(model);
            }
            byte []data= new byte[model.File.ContentLength];
            model.File.InputStream.Read(data, 0, model.File.ContentLength);
            model.ImageData = data;
            var lst=new DataLayer().UploadProduct<Products>(model);
            return View();
        }

        [SessionTimeoutAttribute]
        public ActionResult AddDisease() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDisease(M_Disease model) 
        {
            new DataLayer().AddDisease<M_Disease>(model);
            return View();
        }

        public ActionResult AddUnit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUnit(M_Unit model)
        {
            new DataLayer().AddUnit<M_Unit>(model);
            return View();
        }
    }
}
