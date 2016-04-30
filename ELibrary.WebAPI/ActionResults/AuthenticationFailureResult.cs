using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ELibrary.WebAPI.ActionResults
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        private string _reasonPhrase;
        private HttpRequestMessage _request;

        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            _reasonPhrase = reasonPhrase;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var msg = _request.CreateResponse(HttpStatusCode.Unauthorized);
            msg.ReasonPhrase = _reasonPhrase;
            msg.RequestMessage = _request;

            return Task.FromResult(msg);
        }
    }
}