using Learning.Data;
using Learning.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace Learning.ODataService.Controllers
{
    public class CoursesController : EntitySetController<Course, int>
    {
        LearningContext ctx = new LearningContext();

        [Queryable(PageSize=10)]
        public override IQueryable<Course> Get()
        {
            return ctx.Courses.AsQueryable();
        }

        protected override Course GetEntityByKey(int key)
        {
            return ctx.Courses.Find(key);
        }

        public HttpResponseMessage GetName(int key)
        {
            Course course = ctx.Courses.Find(key);
            if (course == null)
            {
                throw Helpers.ResourceNotFoundError(Request);
            }
            return  Request.CreateResponse(HttpStatusCode.OK, course.Name);
        }

        protected override void Dispose(bool disposing)
        {
            ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}
