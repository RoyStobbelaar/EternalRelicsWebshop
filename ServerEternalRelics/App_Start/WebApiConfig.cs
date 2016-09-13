using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ServerEternalRelics
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            /*
			config.MapHttpAttributeRoutes();
             */
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           
            config.Routes.MapHttpRoute(
                name: "Product",
                routeTemplate: "api/Product/",
                defaults: new { controller = "Product" }
            );

            config.Routes.MapHttpRoute(
                name: "ProductCategories",
                routeTemplate: "api/Category/{productId}/{categoryId}",
                defaults: new { controller = "Category" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
