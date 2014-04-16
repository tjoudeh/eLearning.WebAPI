using Learning.Data.Entities;
using Microsoft.Data.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;

namespace Learning.ODataService
{
    public static class WebApiConfig
    {
        
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapODataRoute("elearningOData", "OData", GenerateEdmModel());
        }

        private static IEdmModel GenerateEdmModel()
        {
          
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Course>("Courses");
            builder.EntitySet<Enrollment>("Enrollments");
            builder.EntitySet<Subject>("Subjects");
          
            var tutorsEntitySet = builder.EntitySet<Tutor>("Tutors");

            tutorsEntitySet.EntityType.Ignore(s => s.UserName);
            tutorsEntitySet.EntityType.Ignore(s => s.Password);

            return builder.GetEdmModel();
        }
    }
}
