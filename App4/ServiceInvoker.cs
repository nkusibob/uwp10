using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.EBC.SurfaceComponent.Common
{
    using Microsoft.EBC.SurfaceComponent.Data.Entities.DataEntities; // TOBE: Removed Once all the functionality is completed.
    //using Microsoft.EBC.SurfaceComponent.PCL.Common;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    public class ServiceInvoker
    {
        static string result = string.Empty;
        static HttpClient _client = null;
        
       
        private static async Task<HttpClient> RequestTokenFromACS(string scope, string wrapPassword, string wrapUsername, string acsNamespace)
        {

            if (_client != null)
            {
                return _client;
            }

            _client = new HttpClient();
            _client.Timeout = new TimeSpan(0, 0, ConstantsSurfaceComponent.HTTP_Timeout_Secs);

            HttpRequestMessage request =
                new HttpRequestMessage(
                            HttpMethod.Post,
                            string.Format("https://{0}.{1}{2}",
                                        acsNamespace,
                                        ConstantsSurfaceComponent.ACCESS_CONTROL_GENERIC_URL + "/",
                                        ConstantsSurfaceComponent.Wrap_Version)
                            );




            Dictionary<string, string> tokenreq = new Dictionary<string, string>()
            {
              {"wrap_name", wrapUsername},
              {"wrap_password", wrapPassword},
              {"wrap_scope", scope}
            };

            FormUrlEncodedContent form = new FormUrlEncodedContent(tokenreq);

            request.Content = form;

            int retryCount = 0;
            while (true)
            {
                try
                {
                    HttpResponseMessage response = await _client.SendAsync(request);

                    string responseString = await response.Content.ReadAsStringAsync();

                    string token = responseString
                                    .Split('&')
                                    .Single(value => value.StartsWith(ConstantsSurfaceComponent.Wrap_Access_Token + "=", StringComparison.OrdinalIgnoreCase))
                                    .Split('=')[1];
                    string swt = string.Format("WRAP access_token=\"{0}\"", WebUtility.UrlDecode(token));
                    _client.DefaultRequestHeaders.Add("Authorization", swt);
                    _client.DefaultRequestHeaders.Add("AuthorizationToken", swt);

                    break;
                }
                catch (Exception e)
                {
                    //System.Console.WriteLine("RequestTokenFromACS failed, retry count " + retryCount);
                    ++retryCount;
                    if (retryCount == 3)
                    {
                        retryCount = 0;
                        throw e.InnerException;
                    }

                }
            }
            return _client;
        }

        public static async Task<string> GetResponseFromServiceAsync(string uri)
        {
            int retryCount = 0;
            //while (true)
            {
                try
                {
                    if (_client == null)
                    {
                        _client = await RequestTokenFromACS(Constants.SCOPE
                            , Constants.WRAP_PASSWORD
                            , Constants.WRAP_USERNAME
                            , Constants.ACS_NAMESPACE);
                    }
                    string x = await _client.GetStringAsync(uri);
                    return x;
                }

                catch (Exception e)
                {
                    //System.Console.WriteLine("GetResponseFromServiceAsync failed, retry count " + retryCount);
                    ++retryCount;
                    if (retryCount == 3)
                    {
                        retryCount = 0;
                        throw e.InnerException;
                    }
                }
            }
            return null;
        }

        public static async Task<string> PostToServiceAsync(string uri, string data)
        {
            int retryCount = 0;
            while (true)
            {
                try
                {
                    if (_client == null)
                    {
                        _client = await RequestTokenFromACS(Constants.SCOPE
                            , Constants.WRAP_PASSWORD
                            , Constants.WRAP_USERNAME
                            , Constants.ACS_NAMESPACE);
                    }
                    StringContent content = new System.Net.Http.StringContent(data, Encoding.UTF8, "application/json");
                    var x = await _client.PostAsync(uri, content);

                    if ((x != null) && (x.IsSuccessStatusCode) && (x.Content != null))
                    {
                        string retResponse = x.Content.ReadAsStringAsync().Result;
                        return retResponse;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception e)
                {
                    // System.Console.WriteLine("PostToServiceAsync failed, retry count " + retryCount);
                    ++retryCount;
                    if (retryCount == 3)
                    {
                        retryCount = 0;
                        throw e.InnerException;
                    }
                }
            }
        }
    }
}
