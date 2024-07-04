using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.Shipping
{
    
    class Security_Request
    {
        public Security_Request() { }
        public static HttpRequestMessage CreateRequestMessage(HttpMethod method, string baseUrl, string path, string token, string body)
        {
            var url = $"{baseUrl}{path}";
            //var requestId = Guid.NewGuid().ToString();
            var request = new HttpRequestMessage(method, url);
            request.Headers.TryAddWithoutValidation("Authorization", $"hmac {token}");
            request.Headers.TryAddWithoutValidation("Market", "VN");
            if (method == HttpMethod.Post)
            {
                request.Content = new StringContent(body);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
            }
            return request;
        }
        public static string GenerateToken(string body, string key, string secret, HttpMethod httpMethod, string path)
        {
            var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var method = httpMethod.Method.ToUpper();
            string rawSignature;
            if (httpMethod == HttpMethod.Post) 
                rawSignature = $"{time}\r\n{method}\r\n{path}\r\n\r\n{body}";
            else
                rawSignature = $"{time}\r\n{method}\r\n{path}\r\n\r\n";
            byte[] keyByte = Encoding.UTF8.GetBytes(secret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(rawSignature);
            byte[] hashmessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
            var signature = string.Concat(Array.ConvertAll(hashmessage, x => x.ToString("x2")));
            var token = $"{key}:{time}:{signature}";

            return token;
        }
    }
}
