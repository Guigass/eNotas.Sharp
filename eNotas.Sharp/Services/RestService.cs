using eNotas.Sharp.Models;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace eNotas.Sharp.Services
{
    internal class RestService : IDisposable
    {
        private HttpClient client;
        private string _apiKey;

        public RestService(string apiUrl, string apiKey)
            : this(apiUrl, apiKey, new HttpClient())
        {
        }

        internal RestService(string apiUrl, string apiKey, HttpMessageHandler handler)
            : this(apiUrl, apiKey, new HttpClient(handler))
        {
        }

        private RestService(string apiUrl, string apiKey, HttpClient httpClient)
        {
            _apiKey = apiKey;

            client = httpClient;
            client.BaseAddress = new Uri(apiUrl);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {_apiKey}");
        }

        public async Task<ApiResponse> Post(string action, object obj, CancellationToken cancellationToken = default)
        {
            var apiResponse = new ApiResponse();
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, action))
                {
                    var json = SerializeObject(obj);

                    var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                    requestMessage.Content = jsonContent;

                    var result = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

                    apiResponse.Status = result.StatusCode.ToString();
                    apiResponse.IsSuccess = result.IsSuccessStatusCode;

                    var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    apiResponse.Message = jsonResult;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) { apiResponse.Exception = ex; }

            return apiResponse;
        }

        public async Task<ApiResponse> PostMultipart(string action, MultipartFormDataContent content, CancellationToken cancellationToken = default)
        {
            var apiResponse = new ApiResponse();
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, action))
                {
                    requestMessage.Content = content;

                    var result = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

                    apiResponse.Status = result.StatusCode.ToString();
                    apiResponse.IsSuccess = result.IsSuccessStatusCode;

                    var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    apiResponse.Message = jsonResult;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) { apiResponse.Exception = ex; }

            return apiResponse;
        }

        public async Task<ApiResponse> Put(string action, CancellationToken cancellationToken = default)
        {
            var apiResponse = new ApiResponse();
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, action))
                {
                    var result = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

                    apiResponse.Status = result.StatusCode.ToString();
                    apiResponse.IsSuccess = result.IsSuccessStatusCode;

                    var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    apiResponse.Message = jsonResult;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) { apiResponse.Exception = ex; }

            return apiResponse;
        }

        public async Task<ApiResponse<T>> Get<T>(string action, string deserializer = "json", CancellationToken cancellationToken = default) where T : class
        {
            var apiResponse = new ApiResponse<T>();
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, action))
                {
                    var result = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

                    apiResponse.Status = result.StatusCode.ToString();
                    apiResponse.IsSuccess = result.IsSuccessStatusCode;

                    var resultString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    apiResponse.Message = resultString;

                    try
                    {
                        if (deserializer == "json")
                        {
                            apiResponse.Object = JsonConvert.DeserializeObject<T>(resultString);
                        }
                        else if (deserializer == "xml")
                        {
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                            using (TextReader reader = new StringReader(resultString))
                                apiResponse.Object = (T)xmlSerializer.Deserialize(reader);
                        }

                    }
                    catch (Exception ex) { apiResponse.Exception = ex; }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) { apiResponse.Exception = ex; }

            return apiResponse;
        }

        public async Task<ApiResponse<byte[]>> GetBytes(string action, CancellationToken cancellationToken = default)
        {
            var apiResponse = new ApiResponse<byte[]>();
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, action))
                {
                    var result = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

                    apiResponse.Status = result.StatusCode.ToString();
                    apiResponse.IsSuccess = result.IsSuccessStatusCode;

                    var bytes = await result.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                    if (result.IsSuccessStatusCode)
                    {
                        apiResponse.Object = bytes;
                    }
                    else
                    {
                        apiResponse.Message = Encoding.UTF8.GetString(bytes);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) { apiResponse.Exception = ex; }

            return apiResponse;
        }

        public async Task<ApiResponse> Delete(string action, CancellationToken cancellationToken = default)
        {
            var apiResponse = new ApiResponse();

            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, action))
                {
                    var result = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);

                    apiResponse.Status = result.StatusCode.ToString();
                    apiResponse.IsSuccess = result.IsSuccessStatusCode;

                    var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    apiResponse.Message = jsonResult;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex) { apiResponse.Exception = ex; }

            return apiResponse;
        }

        private static string SerializeObject(object quote)
        {
            return JsonConvert.SerializeObject(quote, Formatting.None, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            });
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
