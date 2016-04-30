using ELibrary.WebAPI.ActionResults;
using ELibrary.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace ELibrary.WebAPI.Filters
{
    public class IdentityBasicAuthenticationAttribute : Attribute, IAuthenticationFilter
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
            var authHeaders = req.Headers.Authorization;

            if (authHeaders != null)
            {
                if (authHeaders.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(authHeaders.Parameter))
                    {

                        var rawCredentials = authHeaders.Parameter;
                        var encoding = Encoding.GetEncoding("iso-8859-1");
                        var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));

                        var split = credentials.Split(':');
                        var userName = split[0];
                        var password = split[1];

                        IUserService userService = new UserService();
                        if (userService.Authenticate(userName, password))
                        {
                            context.Principal = new GenericPrincipal(new GenericIdentity(userName), null);
                        }
                        else
                        {
                            context.ErrorResult = new AuthenticationFailureResult("Invalid Credentials", req);
                        }
                    }
                    else
                    {
                        context.ErrorResult = new AuthenticationFailureResult("Missing Credentials", req);
                    }
                }

            }


            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Basic");

            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);

            return Task.FromResult<object>(null);
        }
    }
}