using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ayurveda.Models
{
    public class Products:M_Unit
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string CompnayName { get; set; }
        public int Quantity { get; set; }
        public IList<Products> products { get; set; }
        public string SearchBox { get; set; }
        public string Unit { get; set; }
    }
    public class M_Unit:Yoga
    {
        public int UnitId { get; set; }
        public string Unit { get; set; }
    }
    public class Yoga:M_Description
    {
        public string YogaName { get; set; }
        public string YogaDescription { get; set; }
        public IList<Yoga> YogaList { get; set; }
    }

    public class M_Description : M_Disease
    {
        public int DescriptionId { get; set; }
        public string Description { get; set; }
        public IList<M_Description> Descriptions { get; set; }
    }

    public class M_Disease
    {
        public M_Disease(){}
        public int DiseaseID { get; set; }
        public string Disease { get; set; }
        public HttpPostedFileBase File { get; set; }
        public byte[] ImageData { get; set; }
    }
   
}
