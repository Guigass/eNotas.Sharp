using eNotas.Sharp.Models;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace eNotas.Sharp.Services
{
    internal class RestService : IDisposable
    {
        private HttpClient client;
        private string _apiUrl;
        private string _apiKey;

        public RestService(string apiUrl, string apiKey)
        {
            _apiKey = apiKey;
            _apiUrl = apiUrl;

            client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {_apiKey}");
        }

        public async Task<ApiResponse> Post(string action, object obj)
        {
            var apiResponse = new ApiResponse();
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, action))
                {
                    var json = SerializeObject(obj);

                    var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                    requestMessage.Content = jsonContent;

                    var result = await client.SendAsync(requestMessage);

                    apiResponse.Status = result.StatusCode.ToString();
                    apiResponse.IsSuccess = result.IsSuccessStatusCode;

                    var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    apiResponse.Message = jsonResult;
                }
            }
            catch (Exception ex) { apiResponse.Exception = ex; }

            return apiResponse;
        }

        public async Task<ApiResponse> Put(string action)
        {
            var apiResponse = new ApiResponse();
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, action))
                {
                    var result = await client.SendAsync(requestMessage);

                    apiResponse.Status = result.StatusCode.ToString();
                    apiResponse.IsSuccess = result.IsSuccessStatusCode;

                    var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                    apiResponse.Message = jsonResult;
                }
            }
            catch (Exception ex) { apiResponse.Exception = ex; }

            return apiResponse;
        }

        public async Task<ApiResponse<T>> Get<T>(string action, string deserializer = "json") where T : class
        {
            var apiResponse = new ApiResponse<T>();
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, action))
                {
                    var result = await client.SendAsync(requestMessage);

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
                    catch (Exception ex){}
                }
            }
            catch (Exception ex) { apiResponse.Exception = ex; }

            return apiResponse;
        }

        public async Task<ApiResponse> Delete(string action)
        {
            var apiResponse = new ApiResponse();

            try
            {
                var result = await client.DeleteAsync($"{_apiUrl}/{action}");

                apiResponse.Status = result.StatusCode.ToString();
                apiResponse.IsSuccess = result.IsSuccessStatusCode;

                var jsonResult = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                apiResponse.Message = apiResponse.Message = jsonResult;
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
