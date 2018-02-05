﻿using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZY.RMAS.BusinessFactory
{
    public class CourseDetailBO
    {
        private CourseDetailDAL courseDetailDAL;

        public CourseDetailBO()
        {
            courseDetailDAL = new CourseDetailDAL();
        }

        public IEnumerable<CourseDetail> GetList()
        {
            return courseDetailDAL.GetList();
        }

        public CourseDetail GetCourseDetail(CourseDetail item)
        {
            return (CourseDetail)courseDetailDAL.GetItem<CourseDetail>(item);
        }

        public bool SaveCouseDetail(CourseDetail newItem)
        {
            return courseDetailDAL.Save(newItem);
        }
    }
}
