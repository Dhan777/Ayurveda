using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayurveda.Models;
using Ayurveda.Filters;
namespace Ayurveda.Controllers
{
    public class ShopController : Controller
    {
        DataLayer db = new DataLayer();
       // [SessionTimeoutAttribute]
        public ActionResult Index()
        {
            Products objPro = new Products();
            objPro.products = db.GetProducts<Products>();
            return View(objPro);
        }

    }
}
