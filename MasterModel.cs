using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ayurveda.Models
{
    public class MasterModel
    {
        DataLayer db = new DataLayer();
        public SelectList GetUnit()
        {
            var lst = new List<M_Unit>();
            lst = db.GetUnit<M_Unit>().ToList();
            SelectList sb = new SelectList(lst, "UnitId", "Unit");
            return sb;
        }

        public SelectList GetDisease()
        {
            var lst = new List<M_Disease>();
            lst = db.GetDisease<M_Disease>().ToList();
            SelectList sb = new SelectList(lst, " DiseaseID", "Disease");
            return sb;
        }

    }
}