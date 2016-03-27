using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace ELibrary.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Routes.MapHttpRoute(
                name: "Book",
                routeTemplate: "api/library/books/{bookid}",
                defaults: new { controller = "books", bookid = RouteParameter.Optional}
                );

            config.Routes.MapHttpRoute(
                name: "Tags",
                routeTemplate: "api/library/books/{bookid}/tags/{id}",
                defaults: new { controller = "tags", id = RouteParameter.Optional }
                );

            ////Remove this template with constraints
            ////Because it will overwrite id = RouteParameter.Optional
            //config.Routes.MapHttpRoute(
            //    name: "Book",
            //    routeTemplate: "api/library/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional },
            //    constraints: new { id=@"\d+"}
            //    );


            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            jsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            //jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }
    }
}