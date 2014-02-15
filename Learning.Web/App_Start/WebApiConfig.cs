using Learning.Web.Filters;
using Learning.Web.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Learning.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            /***Uncomment the Routes Students & Students2 to allow versioning withing URLs***/

            // config.Routes.MapHttpRoute(
            //   name: "Students",
            //   routeTemplate: "api/v1/students/{userName}",
            //   defaults: new { controller = "students", userName = RouteParameter.Optional }
            //);

            // config.Routes.MapHttpRoute(
            //    name: "Students2",
            //    routeTemplate: "api/v2/students/{userName}",
            //    defaults: new { controller = "studentsV2", userName = RouteParameter.Optional }
            //);

            /***--------------------------------------------------------------------------***/

            config.Routes.MapHttpRoute(
                name: "Students",
                routeTemplate: "api/students/{userName}",
                defaults: new { controller = "students", userName = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "Courses",
               routeTemplate: "api/courses/{id}",
               defaults: new { controller = "courses", id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
              name: "Enrollments",
              routeTemplate: "api/courses/{courseId}/students/{userName}",
              defaults: new { controller = "Enrollments", userName = RouteParameter.Optional }
          );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Replace the controller configuration selector
            config.Services.Replace(typeof(IHttpControllerSelector), new LearningControllerSelector((config)));

            /*To configure HTTP Caching using Entity Tags (ETags):
            Please Uncomment the below code to active using CacheCow. Do not forget to run the SQL script file 
            found on path {projectpath}\packages\CacheCow.Server.EntityTagStore.SqlServer.0.4.11\scripts\script.sql
            which contains SQL script needed by cache cow to run correctlly. */

            //var connString = System.Configuration.ConfigurationManager.ConnectionStrings["eLearningConnection"].ConnectionString;
            //var eTagStore = new CacheCow.Server.EntityTagStore.SqlServer.SqlServerEntityTagStore(connString);
            //var cacheCowCacheHandler = new CacheCow.Server.CachingHandler(eTagStore);
            //cacheCowCacheHandler.AddLastModifiedHeader = false;
            //config.MessageHandlers.Add(cacheCowCacheHandler);


#if !DEBUG
            //force HTTPs
            config.Filters.Add(new ForceHttpsAttribute());
#endif
        }
    }
}
