using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ELibrary.WebAPI.ActionResults
{
    public class AddChallengeOnUnauthorizedResult : IHttpActionResult
    {
        private AuthenticationHeaderValue _challenge;
        private IHttpActionResult _innerResult;

        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
        {
            _challenge = challenge;
            _innerResult = innerResult;
        }
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _innerResult.ExecuteAsync(cancellationToken);

            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if(!response.Headers.WwwAuthenticate.Any(h => h.Scheme == _challenge.Scheme))
                {
                    response.Headers.WwwAuthenticate.Add(_challenge);
                }
            }

            return response;
            
        }
    }
}