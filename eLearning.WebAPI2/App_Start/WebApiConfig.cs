using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;

namespace eLearning.WebAPI2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

           //Support for CORS
            EnableCorsAttribute CorsAttribute = new EnableCorsAttribute("*", "*", "GET,POST");
            config.EnableCors(CorsAttribute);
        }
    }
}
