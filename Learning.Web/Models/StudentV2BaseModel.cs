using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learning.Web.Models
{
    public class StudentV2BaseModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string FullName { get; set; }
        public Data.Enums.Gender Gender { get; set; }
        public int EnrollmentsCount { get; set; }
        public double CoursesDuration { get; set; }
   
    }
}