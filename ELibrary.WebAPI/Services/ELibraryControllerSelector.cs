using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace ELibrary.WebAPI.Services
{
    public class ELibraryControllerSelector:DefaultHttpControllerSelector
    {
        private HttpConfiguration _config;

        public ELibraryControllerSelector(HttpConfiguration config):base(config)
        {
            _config = config;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();

            var routeData = request.GetRouteData();

            var controllerName = (string)routeData.Values["controller"];

            HttpControllerDescriptor descriptor;

            if(string.IsNullOrWhiteSpace(controllerName))
            {
                base.SelectController(request);
            }else if(controllers.TryGetValue(controllerName, out descriptor))
            {
                var version = "2";

                var newName = string.Concat(controllerName, "V", version);

                HttpControllerDescriptor versionedDescriptor;

                if(controllers.TryGetValue(newName, out versionedDescriptor))
                {
                    return versionedDescriptor;
                }

                return descriptor;
            }

            return null;
        }
    }
}