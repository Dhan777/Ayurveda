using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ayurveda.Models
{
    public class Users:GenerateOtp
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayPassword { get; set; }
        public string Address { get; set; }
        public string CaptchaCode { get; set; }
    }

    public class GenerateOtp:Message
    {
        public string OTP { get; set; }
        public static string Random()
        {
            return new Random().Next(1000, 9999).ToString();
        }
    }
    public class Message:UploadImage
    {
        public string message { get; set; }
        public string SendBy { get; set; }
        public string To { get; set; }
        public string Mutual { get; set; }
    }

    public class UploadImage:M_Disease
    {
       // public HttpPostedFileBase File { get; set; }
      //  public byte[] ImageData { get; set; }
        public int Size { get; set; }
        public string ImageName { get; set; }
    }
    
}