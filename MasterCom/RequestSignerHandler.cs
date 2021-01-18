using Mastercard.Developer.OAuth1Signer.Core.Signers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCom
{
    internal class RequestSignerHandler : HttpClientHandler
    {
        private readonly NetHttpClientSigner signer;

        public RequestSignerHandler(string consumerKey, RSA signingKey)
        {
            signer = new NetHttpClientSigner(consumerKey, signingKey);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            signer.Sign(request);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
