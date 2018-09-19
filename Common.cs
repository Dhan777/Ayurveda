using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace Ayurveda.Models
{
    public class Common
    {
        private static int DEFAULT_MIN_PASSWORD_LENGTH = 8, DEFAULT_MAX_PASSWORD_LENGTH = 10;
        private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz", PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        private static string PASSWORD_CHARS_NUMERIC = "23456789", PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

        public static string Generate()
        {
            return Generate(DEFAULT_MIN_PASSWORD_LENGTH, DEFAULT_MAX_PASSWORD_LENGTH);
        }

        public static string Generate(int length)
        {
            return Generate(length, length);
        }

        public static string Generate(int minLength, int maxLength)
        {

            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;


            char[][] charGroups = new char[][]
        {
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray()
        };


            int[] charsLeftInGroup = new int[charGroups.Length];


            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;


            int[] leftGroupsOrder = new int[charGroups.Length];


            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;


            byte[] randomBytes = new byte[4];


            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);


            int seed = (randomBytes[0] & 0x7f) << 24 |
                        randomBytes[1] << 16 |
                        randomBytes[2] << 8 |
                        randomBytes[3];


            Random random = new Random(seed);


            char[] password = null;


            if (minLength < maxLength)
                password = new char[random.Next(minLength, maxLength + 1)];
            else
                password = new char[minLength];


            int nextCharIdx;


            int nextGroupIdx;


            int nextLeftGroupsOrderIdx;


            int lastCharIdx;


            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;


            for (int i = 0; i < password.Length; i++)
            {

                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0,
                                                         lastLeftGroupsOrderIdx);


                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];


                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;



                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);


                password[i] = charGroups[nextGroupIdx][nextCharIdx];


                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                                              charGroups[nextGroupIdx].Length;

                else
                {

                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                                    charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }

                    charsLeftInGroup[nextGroupIdx]--;
                }


                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

                else
                {

                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                    leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }

                    lastLeftGroupsOrderIdx--;
                }
            }


            return new string(password);
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public bool SendMail(string EmailID, string Subject, string Body)
        {
            try
            {
                string from = ConfigurationManager.AppSettings["EmailID"].ToString();
                string Password = ConfigurationManager.AppSettings["Password"].ToString();
                using (MailMessage mail = new MailMessage(from, EmailID))
                {
                    mail.Subject = Subject;
                    mail.Body = Body;
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from, Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    return true;
                }
            }
            catch (Exception E)
            {
                return false;
            }
        }

        public bool ValidateEmail(string Email)
        {
            string email = Email;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }
    }

}
