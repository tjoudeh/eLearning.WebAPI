using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;
using System.Web.Routing;
using Learning.Data.Entities;

namespace eLearning.WebAPI2.Controllers
{
    [RoutePrefix("api/courses")]
    //[EnableCors("*", "*", "GET,POST")]
    public class CoursesController : ApiController
    {

        [Route(Name = "CoursesRoute")]
        //[EnableCors("*", "*", "GET")]
        public HttpResponseMessage Get(int page = 0, int pageSize = 10)
        {
            IQueryable<Course> query;

            Learning.Data.LearningContext ctx = new Learning.Data.LearningContext();

            Learning.Data.ILearningRepository repo = new Learning.Data.LearningRepository(ctx);

            query = repo.GetAllCourses().OrderBy(c => c.CourseSubject.Id);
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("CoursesRoute", new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("CoursesRoute", new { page = page + 1, pageSize = pageSize }) : "";

            var results = query
                          .Skip(pageSize * page)
                          .Take(pageSize)
                          .ToList();

            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink,
                Results = results
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [Route("~/api/subject/{subjectid:int}/courses")]
        public HttpResponseMessage GetCoursesBySubject(int subjectid)
        {
            Learning.Data.LearningContext ctx = null;
            Learning.Data.ILearningRepository repo = null;
            IQueryable<Course> query;

            try
            {
                ctx = new Learning.Data.LearningContext();
                repo = new Learning.Data.LearningRepository(ctx);

                query = repo.GetCoursesBySubject(subjectid);
                var coursesList = query.Select(c => c.Name).ToList();
                if (coursesList != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, coursesList);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            finally
            {
                ctx.Dispose();
            }
        }

        [Route("{id:int}")]
        //[DisableCors()]
        public IHttpActionResult GetCourse(int id)
        {

            Learning.Data.LearningContext ctx = null;
            Learning.Data.ILearningRepository repo = null;
            try
            {

                ctx = new Learning.Data.LearningContext();
                repo = new Learning.Data.LearningRepository(ctx);

                var course = repo.GetCourse(id, false);
                if (course != null)
                {
                    return Ok<Learning.Data.Entities.Course>(course);
                }
                else
                {
                    return eLearning.WebAPI2.CustomIHttpActionResult.ApiControllerExtension.NotFound(this, "Course not found");
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            finally
            {
                ctx.Dispose();
            }
        }


    }
}
