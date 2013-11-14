using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Learning.Web.Services
{
    public class LearningControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;
        public LearningControllerSelector(HttpConfiguration config)
            : base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
           var controllers =  GetControllerMapping(); //Will ignore any controls in same name even if they are in different namepsace

           var routeData = request.GetRouteData();

           var controllerName = routeData.Values["controller"].ToString();

           HttpControllerDescriptor descriptor;

           if (controllers.TryGetValue(controllerName, out descriptor))
           {

               //var version = GetVersionFromQueryString(request);
               //var version = GetVersionFromHeader(request);
               var version = GetVersionFromAcceptHeaderVersion(request);

               var newName = string.Concat(controllerName, "V", version);

               HttpControllerDescriptor versionDescriptor;
               if (controllers.TryGetValue(newName, out versionDescriptor))
               {
                   return versionDescriptor;
               }

               return descriptor;
           }

           return null;
            
        }

        private string GetVersionFromAcceptHeaderVersion(HttpRequestMessage request)
        {
            var accept = request.Headers.Accept;

            foreach (var mime in accept)
            {
                if (mime.MediaType == "application/json")
                {
                    var value = mime.Parameters
                                    .Where(v => v.Name.Equals("version", StringComparison.OrdinalIgnoreCase))
                                    .FirstOrDefault();

                    if (value != null)
                    {
                        return value.Value;
                    }
                    else
                    {
                        return "1";
                    }
                    
                }
            }

            return "1";
        }

        private string GetVersionFromHeader(HttpRequestMessage request)
        {
            const string HEADER_NAME = "X-Learning-Version";

            if (request.Headers.Contains(HEADER_NAME))
            {
                var header = request.Headers.GetValues(HEADER_NAME).FirstOrDefault();
                if (header != null) {
                    return header;
                }
            }

            return "1";
        }

        private string GetVersionFromQueryString(HttpRequestMessage request)
        {
            var query = HttpUtility.ParseQueryString(request.RequestUri.Query);

            var version = query["v"];

            if (version != null)
            {
                return version;
            }

            return "1";

        }
    }
}