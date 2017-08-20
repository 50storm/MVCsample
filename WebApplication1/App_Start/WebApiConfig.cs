using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //http://localhost:50195/member/MemberApi/

            config.Routes.MapHttpRoute(
    name: "MemberApi",
    routeTemplate: "member/{controller}/{id}",
    defaults: new { id = RouteParameter.Optional }
);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
