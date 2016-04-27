using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ELibrary.WebAPI.ActionResults
{
    public class RequireHttpsActionResult : IHttpActionResult
    {
        private string _reasonPhrase;
        private HttpRequestMessage _request;

        public RequireHttpsActionResult(string reasonPhrase, HttpRequestMessage request)
        {
            _reasonPhrase = reasonPhrase;
            _request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage msg;

            if (_request.Method.Method == "GET")
            {
                msg = _request.CreateResponse(HttpStatusCode.Found);
                msg.Content = new StringContent(_reasonPhrase, Encoding.UTF8, "text/html");

                var uriBuilder = new UriBuilder(_request.RequestUri);
                uriBuilder.Scheme = Uri.UriSchemeHttps;
                uriBuilder.Port = 443;

                msg.Headers.Location = uriBuilder.Uri;

            }
            else
            {
                msg = _request.CreateResponse(HttpStatusCode.NotFound);
                msg.Content = new StringContent(_reasonPhrase, Encoding.UTF8, "text/html");
            }

            return Task.FromResult(msg);
        }
    }
}