using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using ELibrary.WebAPI.ActionResults;

namespace ELibrary.WebAPI.Filters
{
    public class RequireHttpsAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var req = context.Request;

            if(req.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var html = "<p>Https is required.</p>";

                context.ErrorResult = new RequireHttpsActionResult(html, req);

            }

            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }
    }
}