using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayurveda.Filters;
using Ayurveda.Models;
namespace Ayurveda.Controllers
{
    public class YogaController : Controller
    {
        DataLayer db = new DataLayer();
        [SessionTimeoutAttribute]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Yoga model )
        {
            if (!(model.File.ContentLength > 0))
            {
                return View(model);
            }
            byte[] data = new byte[model.File.ContentLength];
            model.File.InputStream.Read(data, 0, model.File.ContentLength);
            model.ImageData = data;
            db.UploadYoga<Yoga>(model);
            return View();
        }

        public ActionResult YogaList()
        {
            Yoga model = new Yoga();
            model.YogaList = db.GetYoga<Yoga>();
            return View(model);
        }
    }
}
