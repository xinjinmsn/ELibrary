using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Routing;

namespace ELibrary.WebAPI.Constraints
{
    /// <summary>
    /// A Constraint implementation that matches an HTTP header against an expected version value.  Matches
    /// both custom request header ("X-ELibrary-Version") and custom content type vnd.myservice.vX+json (or other dt type)
    /// 
    /// Adapted from ASP .NET samples
    /// </summary>
    internal class VersionConstraint : IHttpRouteConstraint
    {
        public const string VersionHeaderName = "X-ELibrary-Version";

        private const int DefaultVersion = 1;

        public VersionConstraint(int allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion
        {
            get;
            private set;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route,
            string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                // try custom request header "X-ELibrary-Version"

                int? version = GetVersionFromCustomRequestHeader(request);

                // not found?  Try custom content type in accept header

                if (version == null)
                {
                    version = GetVersionFromCustomContentType(request);
                }


                return ((version ?? DefaultVersion) == AllowedVersion);
            }

            return true;
        }

        private int? GetVersionFromCustomContentType(HttpRequestMessage request)
        {
            var accept = request.Headers.Accept;
            var ex = new Regex(@"application\/vnd\.elibrary\.([a-z]+)\.v([0-9]+)\+json", RegexOptions.IgnoreCase);

            foreach (var mime in accept)
            {
                var match = ex.Match(mime.MediaType);
                if (match != null)
                {
                    string versionAsString = match.Groups[2].Value;
                    // ... and return
                    int version;
                    if (versionAsString != null && Int32.TryParse(versionAsString, out version))
                    {
                        return version;
                    }
                }
            }

            return null;
        }



        private int? GetVersionFromCustomRequestHeader(HttpRequestMessage request)
        {
            string versionAsString;
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues(VersionHeaderName, out headerValues) && headerValues.Count() == 1)
            {
                versionAsString = headerValues.First();
            }
            else
            {
                return null;
            }

            int version;
            if (versionAsString != null && Int32.TryParse(versionAsString, out version))
            {
                return version;
            }

            return null;
        }
    }
}