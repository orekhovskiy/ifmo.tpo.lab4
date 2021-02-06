using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Commons;
using ChatApp.Models;
using Newtonsoft.Json;

namespace ChatApp.Services
{
    public static class ServerService
    {
        private static readonly HttpClient Client = new HttpClient();


        public static async Task<Result> AddUser(string login, string password, string firstname, string lastname)
        {
            var baseUrl = UrlService.GetUserControllerUrl();
            var endpoint = $"{baseUrl}AddUser?login={login}&password={password}&firstname={firstname}&lastname={lastname}";
            var result = await GetRequest<UnsuccessfullResult>(endpoint);
            return new Result(result.success, result.value);
        }

        public static async Task<Result> GetUser(string login, string password)
        {
            var baseUrl = UrlService.GetUserControllerUrl();
            var endpoint = $"{baseUrl}GetUser?login={login}&password={password}";
            try
            {
                var result = await GetRequest<SuccessfulUserResult>(endpoint);
                if (result.Success) return new Result(result.Success, result.Value);
                throw new Exception();
            }
            catch
            {
                var result = await GetRequest<UnsuccessfullResult>(endpoint);
                return  new Result(result.success, result.value);
            }
        }

        public static async Task<T> GetRequest<T>(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var res = await reader.ReadToEndAsync();
                T result = JsonConvert.DeserializeObject<T>(res);
                return result;
            }
        }
    }

    public class UnsuccessfullResult
    {
        public bool success { get; set; }
        public string value { get; set; }
    }

    public class SuccessfulUserResult
    {
        public bool Success { get; set; }
        public User Value { get; set; }
    }


}
