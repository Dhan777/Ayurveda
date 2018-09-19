using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayurveda.Models;
using System.Web.Security;
namespace Ayurveda.Controllers
{
    public class AccountController : Controller
    {
        Common objcomman = new Common();
        DataLayer db = new DataLayer();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users model)
        {
            if ((model.UserID.ToLower() == "admin") &&
                (model.DisplayPassword.ToLower() == "admin"))
            {
                Session["UserId"] = "Admin";
                Session["Name"] = "Admin";
                Session["FName"] = "F_Admin";
                Session["Email"] = "Admin@gmail.com";
                Session["Address"] = "Lucknow";

                return Redirect(Url.Action("Home", "Admin"));
            }
            else
            {
                model.Password = Common.EncodePasswordToBase64(model.DisplayPassword);
                var lst = db.Login<Users>(model);
                if (lst != null && lst.Count() > 0)
                {
                    Session["UserId"] = lst.First().UserID;
                    Session["Name"] = lst.First().Name;
                    Session["FName"] = lst.First().FatherName;
                    Session["Email"] = lst.First().Email;
                    Session["Address"] = lst.First().Address;
                    return Redirect(Url.Action("Home", "Home"));
                }
                else
                {
                    DisplayMessage("Invalid Login Id  And Password", "Please Check ur Login ID and Password", 'e');
                    return View(model);
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult generateCaptcha()
        {
            var dropdowndata = this.Session.SessionID.ToString() + ".png";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "./Content/writereaddata/Captcha/" + dropdowndata;
            System.Drawing.FontFamily family = new System.Drawing.FontFamily("Arial");
            CaptchaImage img = new CaptchaImage(150, 50, family);
            string text = img.CreateRandomText(4).ToUpper();
            img.SetText(text);
            img.GenerateImage();
            Session["captchaText"] = text;
            img.Image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            byte[] d = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            //return File(d, "application/octet-stream");
            var bytestrim = d;
            return base.File(bytestrim, "image/png");
            //return Json("/Content/writereaddata/Captcha/" + dropdowndata + "?t=" + DateTime.Now.Ticks, JsonRequestBehavior.AllowGet);
            //test
        }

        public ActionResult ForgetPassword()
        {
            TempData["Page"] = "userid";
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(Users model)
        {
            if (TempData.Peek("Page") == "userid")
            {
                if (string.IsNullOrWhiteSpace(model.UserID))
                {
                    TempData["Page"] = "userid";
                    DisplayMessage("Please Enter User ID", "User ID Can not be left blank", 'w');
                    return View(model);
                }
                if (Session["captchaText"].ToString() != model.CaptchaCode)
                {
                    TempData["Page"] = "userid";
                    DisplayMessage("Invalid Captch Code", "Please Enter a Valid Captcha", 'w');
                    return View(model);
                }
               Session["UserID"] =model.UserID;
               TempData["Page"] = "otp";
               model.OTP = GenerateOtp.Random();
              var lst= db.UpdateOTP<Users>(model);
              if (lst != null && lst.Count() > 0)
              {
                  objcomman.SendMail(lst.First().Email,"Forget Password OTP ","Your OTP Is"+model.OTP);
                  DisplayMessage("Otp has been Sent on Registered Mail ID ", " Please Check and Verify OTP", 's');
              }
               return View();
            }
            if (TempData.Peek("Page") == "otp")
            {
                if (string.IsNullOrWhiteSpace(model.OTP))
                {
                    DisplayMessage("Please Enter OTP", "OTP Can not be left blank", 'w');
                    return View(model);
                }
                model.UserID = Session["UserID"].ToString();
                var lst=db.VerifyOTP<Users>(model);
                if (lst != null && lst.Count() > 0)
                {
                    TempData["Page"] = "ForgetPassword";
                    DisplayMessage("Otp Verified Succefully", "Please Enter new Password", 's');
                    return View();
                }
                else 
                {
                    TempData["Page"] = "otp";
                    DisplayMessage("Otp Not Verified Succefully", "Please Enter  Valid OTP", 's');
                    return View();
                }
            }
            return View();
        }

        public void DisplayMessage(string Heading, string Description, char Status)
        {
            switch (Status)
            {
                case 'S':
                case 's': { TempData["Status"] = "success"; break; }
                case 'W':
                case 'w': { TempData["Status"] = "warning"; break; }
                case 'E':
                case 'e': { TempData["Status"] = "error"; break; }
                case 'I':
                case 'i': { TempData["Status"] = "info"; break; }
            }
            TempData["Msg"] = Heading;
            TempData["Desc"] = Description;
        }
    }
}
