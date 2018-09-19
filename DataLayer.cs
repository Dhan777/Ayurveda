using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
namespace Ayurveda.Models
{
    public class DataLayer : DbContext
    {
        public virtual IEnumerable<T> RegisterUser<T>(Users model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@Name",Value=model.Name},
                 new SqlParameter{ParameterName="@Password",Value=model.Password},
                 new SqlParameter{ParameterName="@DisplayPassword",Value=model.DisplayPassword},
                 new SqlParameter{ParameterName="@FatherName",Value=model.FatherName},
                 new SqlParameter{ParameterName="@Gender",Value=model.Gender},
                 new SqlParameter{ParameterName="@Contact",Value=model.Contact},
                 new SqlParameter{ParameterName="@Email",Value=model.Email},
                 new SqlParameter{ParameterName="@Address",Value=model.Address},
                 new SqlParameter{ParameterName="@OTP",Value=model.OTP},
                 new SqlParameter{ParameterName="@Image",Value=model.ImageData}
                };
                string Query = "Proc_UserRegistration @Name,@Password,@DisplayPassword,@FatherName,@Gender,@Contact,@Email,@Address,@OTP,@Image";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> UpdateOTP<T>(Users model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@UserID",Value=model.UserID},
                 new SqlParameter{ParameterName="@OTP",Value=model.OTP}
                };
                string Query = "Proc_OTP_Updation @UserID,@OTP";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> VerifyOTP<T>(Users model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@UserID",Value=model.UserID},
                 new SqlParameter{ParameterName="@OTP",Value=model.OTP}
                };
                string Query = "Proc_OTP_Verification @UserID,@OTP";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> Login<T>(Users model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@UserId",Value=model.UserID},
                 new SqlParameter{ParameterName="@Password",Value=model.Password},
                };
                string Query = "Proc_Login @UserId,@Password";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> SendQuery<T>(Users model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@UserId",Value=model.UserID.ToLower()},
                 new SqlParameter{ParameterName="@message",Value=model.message},
                 new SqlParameter{ParameterName="@To",Value=model.To.ToLower()},
                };
                string Query = "Proc_SendQuery @UserId,@message,@To";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> GetQuery<T>(Users model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@UserId",Value=model.UserID}
                };
                string Query = "Proc_GetQueries @UserId";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> GetUsers<T>(Users model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@UserId",Value=model.UserID??string.Empty},
                 new SqlParameter{ParameterName="@Name",Value=model.Name??string.Empty},
                 new SqlParameter{ParameterName="@Email",Value=model.Email??string.Empty}
                };
                string Query = "Proc_GetUsers @UserId,@Name,@Email";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> GetUnit<T>()
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] { };
                string Query = "Proc_Unit";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> UploadProduct<T>(Products model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@ProductName",Value=model.ProductName},
                 new SqlParameter{ParameterName="@ProductPrice",Value=model.Price},
                 new SqlParameter{ParameterName="@CompanyName",Value=model.CompnayName},
                 new SqlParameter{ParameterName="@Quantity",Value=model.Quantity},
                 new SqlParameter{ParameterName="@Unit",Value=model.UnitId},
                 new SqlParameter{ParameterName="@Image",Value=model.ImageData},
                 new SqlParameter{ParameterName="@Disease",Value=model.DiseaseID},
                };
                string Query = "Proc_UploadProduct @ProductName,@ProductPrice,@CompanyName,@Quantity,@Unit,@Image,@Disease";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual List<T> GetProducts<T>()
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] { };
                string Query = "Proc_GetProducts";
                return this.Database.SqlQuery<T>(Query, Para).ToList();
            }
            catch (Exception E)
            {
                return null;
            }
        }

        public virtual List<T> GetDisease<T>()
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] { };
                string Query = "Proc_GetDisease";
                return this.Database.SqlQuery<T>(Query, Para).ToList();
            }
            catch (Exception E)
            {
                return null;
            }
        }

        public virtual List<T> UploadYoga<T>(Yoga model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@YogaName",Value=model.YogaName},
                 new SqlParameter{ParameterName="@YogaDescription",Value=model.YogaDescription},
                 new SqlParameter{ParameterName="@ImageData",Value=model.ImageData},
                 new SqlParameter{ParameterName="@Disease",Value=model.DiseaseID}
                };
                string Query = "Proc_UploadYoga @YogaName,@YogaDescription,@ImageData,@Disease";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual List<T> GetYoga<T>()
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] { };
                string Query = "proc_GetYoga";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual List<T> UploadDescription<T>(M_Description model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                 new SqlParameter{ParameterName="@Description",Value=model.Description},
                 new SqlParameter{ParameterName="@Disease",Value=model.DiseaseID},
                 new SqlParameter{ParameterName="@ImageData",Value=model.ImageData},
                };
                string Query = "Proc_AddDescription @Description,@Disease,@ImageData";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> GetDescription<T>()
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
            {
            };
                string Query = "Proc_GetDescription";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }
        }

        public virtual IEnumerable<T> AddDisease<T>(M_Disease model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
                {
                  new SqlParameter{ParameterName="@Disease",Value=model.Disease}
                };
                string query = "Proc_Add_Disease @Disease";
                var lst = this.Database.SqlQuery<T>(query, Para).ToList();
                return lst;
            }
            catch (Exception E) { return null; }

        }

        public virtual IEnumerable<T> AddUnit<T>(M_Unit model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[]
            {
           new SqlParameter{ParameterName="@Unit", Value=model.Unit}
            };
                string Query = "Proc_AddUnit @Unit";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E)
            {
                return null;
            }
        }

        public virtual IEnumerable<T> SearchProducts<T>(Products model)
        {
            try
            {
                SqlParameter[] Para = new SqlParameter[] 
            {
            new SqlParameter{ParameterName="@ProductName" ,Value=model.ProductName??string.Empty}
            };
                string Query = "Proc_SearchProducts @ProductName";
                var lst = this.Database.SqlQuery<T>(Query, Para).ToList();
                return lst;
            }
            catch (Exception E)
            {

                return null;
            }

        }
    }

}