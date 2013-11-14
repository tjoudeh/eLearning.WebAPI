using Learning.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learning.Web.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
        public TutorModel Tutor { get; set; }
        public SubjectModel Subject { get; set; }

    }
}