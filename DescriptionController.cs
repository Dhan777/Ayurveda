using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayurveda.Models;

namespace Ayurveda.Controllers
{
    public class DescriptionController : Controller
    {
        DataLayer db = new DataLayer();

        public ActionResult Index()
        {
            M_Description model = new M_Description();
            model.Descriptions = db.GetDescription<M_Description>().ToList();
            return View(model);
        }

        public ActionResult AddDescription()
        {
            return View();
       }

        [HttpPost]
        public ActionResult AddDescription(M_Description model)
        {
            if (!(model.File.ContentLength > 0))
            {
                return View(model);
            }
            byte[] data = new byte[model.File.ContentLength];
            model.File.InputStream.Read(data, 0, model.File.ContentLength);
            model.ImageData = data;
            db.UploadDescription<M_Description>(model);
            return View();
        }

    }
}
