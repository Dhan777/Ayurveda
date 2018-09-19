using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayurveda.Models;
using System.Text.RegularExpressions;
namespace Ayurveda.Controllers
{
    public class RegistrationController : Controller
    {
        Common objcomman = new Common();
        public ActionResult Register()
        {
            TempData["Page"] = "Registration";
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users model)
        {
            if (!(
                (string.IsNullOrWhiteSpace(model.Name)) ||
                (string.IsNullOrWhiteSpace(model.FatherName)) ||
                (string.IsNullOrWhiteSpace(model.Email)) ||
                (string.IsNullOrWhiteSpace(model.Contact)) ||
                (string.IsNullOrWhiteSpace(model.Address)) ||
                (string.IsNullOrWhiteSpace(model.Gender))
                ))
            {
                if(!(objcomman.ValidateEmail(model.Email)))
                {
                    DisplayMessage("Emaiil is Not Valid", "", 'w');
                    return View(model);
                }
                if (model.Contact!="" && model.Contact.Length!=10)
                {
                    DisplayMessage("Contact is Not Valid", "", 'w');
                    return View(model);
                }
                if (!(model.File.ContentLength > 0))
                {
                    DisplayMessage("Upload User Image", "", 'w');
                    return View(model);
                }
                model.OTP = GenerateOtp.Random();
                model.DisplayPassword = Common.Generate(8);
                model.Password = Common.EncodePasswordToBase64(model.DisplayPassword);
                byte []data= new byte[model.File.ContentLength];
                model.File.InputStream.Read(data, 0, model.File.ContentLength);
                model.ImageData = data;
                var lst = new DataLayer().RegisterUser<Users>(model).First();
                string Body="Your OTP IS :"+lst.OTP+" . Please Verify in order to Complete your Registratin";
                objcomman.SendMail(lst.Email, "OTP Verification", Body);
                DisplayMessage("User Registered Successfully", "OTP has been Sent on your Registered Email Id Please Check and Verify. ", 's');
                TempData["Page"] = "OTP";
                return View(lst);
            }
            else
            {
                DisplayMessage("Please Fill All Fields", "", 'w');
                return View();
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OTPVerification(Users model)
        {
            if(string.IsNullOrWhiteSpace(model.OTP))
            {
                DisplayMessage("Please Enter OTP", "OTP can not be null", 'w');
                TempData["Page"] = "OTP";
                return View("Register",model);
            }
            var lst =new DataLayer().VerifyOTP<Users>(model);
            if (lst.Count() > 0)
            {
                DisplayMessage("OTP Verfied Successfully", 
                 "Please Check Your Registserd Email ID for More Information", 
                 's');

                string Body = "you Registered successfully on our Online Ayurveda Portal. Your User ID IS : " +
                    lst.First().UserID + " and Password : " +
                    lst.First().Password + " . Please Login and Use Your Portal";

                objcomman.SendMail(lst.First().Email, "Ayurveda Login Information", Body);
                Response.Redirect(Url.Action("Login", "Account"));
               }
            else
            {
                DisplayMessage("Invalid OTP", "Please Enter a valid OTP", 'w');
                TempData["Page"] = "OTP";
                return View("Register", model);
            }
            return View();
        }
        public void DisplayMessage(string Heading, string Description, char Status)
        {
            switch (Status)
            {
                case 'S':case 's':{TempData["Status"]="success";break;}
                case 'W':case 'w': { TempData["Status"] = "warning"; break; }
                case 'E':case 'e': { TempData["Status"] = "error"; break; }
                case 'I':case 'i': { TempData["Status"] = "info"; break; }
            }
                 TempData["Msg"] = Heading;
                 TempData["Desc"] = Description;
        }
    }
}
