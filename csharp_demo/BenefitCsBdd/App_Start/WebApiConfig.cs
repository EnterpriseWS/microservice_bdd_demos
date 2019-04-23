using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Configuration;
using System.Collections.Specialized;

namespace BenefitCsBdd
{
    public static class WebApiConfig
    {
        public static List<string> WellknownSites = new List<string>();

        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "OopMaxMetApi",
                routeTemplate: "api/oopmaxmet/{action}/{memberId}",
                //constraints: new
                //{
                //    memberId = @"\d{9}"
                //},
                defaults: new { controller = "OopMax", action = "GetMet" });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
