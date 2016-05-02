using ELibrary.WebAPI.Filters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace ELibrary.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services


            config.Routes.MapHttpRoute(
                name: "Book",
                routeTemplate: "api/v1/library/books/{bookid}",
                defaults: new { controller = "books", bookid = RouteParameter.Optional}
                );
            config.Routes.MapHttpRoute(
                name: "BookV2",
                routeTemplate: "api/v2/library/books/{bookid}",
                defaults: new { controller = "booksv2", bookid = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "TagsWithBook",
                routeTemplate: "api/library/books/{bookid}/tags/{id}",
                defaults: new { controller = "tagswithbook", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "Tags",
                routeTemplate: "api/library/tags/{id}",
                defaults: new { controller = "tags", id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "Orders",
                routeTemplate: "api/user/orders/{orderid}",
                defaults: new { controller = "orders", orderid=RouteParameter.Optional}
                );


            config.Routes.MapHttpRoute(
                name: "OrderEntries",
                routeTemplate: "api/user/orders/{orderid}/entries/{id}",
                defaults: new { controller = "orderentries", id = RouteParameter.Optional }
                );


            config.Routes.MapHttpRoute(
                name: "OrderSummary",
                routeTemplate: "api/user/orders/{orderid}/summary",
                defaults: new { controller = "ordersummary"}
                );

            config.Routes.MapHttpRoute(
                name: "ClearDB",
                routeTemplate: "api/library/cleardb",
                defaults: new { controller = "cleardb" }
                );

            config.Routes.MapHttpRoute(
                name: "Images",
                routeTemplate: "images/{file}"
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

            //Add JSONP Support
            var jsonpFormatter = new JsonpMediaTypeFormatter(jsonFormatter);
            config.Formatters.Insert(0, jsonpFormatter);


            //Add Cors Support
            config.EnableCors(new EnableCorsAttribute("*","*","*"));

#if !DEBUG
            //config.Filters.Add(new RequireHttpsAttribute());
#endif

            //Disable Host-Level Authentication
            config.SuppressHostPrincipal();
        }
    }
}