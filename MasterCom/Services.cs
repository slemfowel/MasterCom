using Mastercard.Developer.OAuth1Signer.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MasterCom
{
   public class Services
    {
        private  string _consumerKey;
        private  Uri _baseUri;
        private  RSA _signingKey;
        public Services(string consumerKey, string fullCertificatePath, string signingKeyAlias, string signingKeyPassword, string baseURL)
        {
            _consumerKey = consumerKey;
            _baseUri = new Uri(baseURL +"/mastercom/v6/queues/names");
            _signingKey = AuthenticationUtils.LoadSigningKey(
                        fullCertificatePath,
                        signingKeyAlias,
                        signingKeyPassword,
                         X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

        }
        public string Get(string fullUrl)
        {
            var httpClient = new HttpClient(new RequestSignerHandler(_consumerKey, _signingKey)) { BaseAddress = _baseUri };
            var getTask = httpClient.GetAsync(fullUrl);
            var result = getTask.Result;
            return result.Content.ReadAsStringAsync().Result;
        }
        public string Post(string fullUrl, string payload)
        {
            var httpClient = new HttpClient(new RequestSignerHandler(_consumerKey, _signingKey)) { BaseAddress = _baseUri };
            using (var content = new StringContent(payload, Encoding.UTF8, "application/json"))
            {
                content.Headers.ContentType.CharSet = "";
                var postTask = httpClient.PostAsync(fullUrl, content);
                var result = postTask.Result;
                return result.Content.ReadAsStringAsync().Result;
            }

        }

        public string Putt(string fullUrl, string payload)
        {
            var httpClient = new HttpClient(new RequestSignerHandler(_consumerKey, _signingKey)) { BaseAddress = _baseUri };
            using (var content = new StringContent(payload, Encoding.UTF8, "application/json"))
            {
                content.Headers.ContentType.CharSet = "";
                var postTask = httpClient.PutAsync(fullUrl, content);
                var result = postTask.Result;
                return result.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
