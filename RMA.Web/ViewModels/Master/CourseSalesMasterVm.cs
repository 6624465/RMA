using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EZY.RMAS.Contract;
using RMA.Web.ViewModels.CourseDetail;

namespace RMA.Web.ViewModels.Master
{
    public class CourseSalesMasterVm
    {
        public CourseSalesMaster courseSalesMaster { get; set; }

        public IEnumerable<Country> countryList { get; set; }

        public IEnumerable<Course> courseList { get; set; }

        public IEnumerable<EduProduct> eduProductList { get; set; }

        public IEnumerable<MonthVm> monthList { get; set; }
    }
}