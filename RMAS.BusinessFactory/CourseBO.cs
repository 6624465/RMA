using System.Collections.Generic;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class CourseBO
    {
        private CourseDAL courseDAL;

        public CourseBO()
        {
            courseDAL = new CourseDAL();
        }

        public IEnumerable<CourseVm> GetList()
        {
            return courseDAL.GetList();
        }

        public Course GetCourse(Course item)
        {
            return (Course)courseDAL.GetItem<Course>(item);
        }

        public bool SaveCouse(Course newItem)
        {
            return courseDAL.Save(newItem);
        }

        public List<Course> GetCoursesByProduct(int Id)
        {
            return courseDAL.GetCoursesByProduct(Id);
        }
    }
}
