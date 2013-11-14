using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Data.Entities
{
    public class Subject
    {
        public Subject()
        {
            Courses = new List<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Course> Courses;
    }
}
