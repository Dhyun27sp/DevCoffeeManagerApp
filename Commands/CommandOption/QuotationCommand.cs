using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;
using DevCoffeeManagerApp.Shipping;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security.Cryptography;
using System.Net.Http.Headers;

namespace DevCoffeeManagerApp.Commands.CommandOption
{
    internal class QuotationCommand : CommandBase
    {
        OptionViewModel OptionOrderViewModel;
        const string key = "pk_test_0aaaabba78a336de774fbbd3de8a299d";    // put your lalamove API key here
        const string secret = "sk_test_vzTqN6KnvN9g/zpml//HB0p+tO7V6h0xV2/TMaSWP1tjLa1grVb8FrdG03egcjHn"; // put your lalamove API secret here
        const string baseUrl = "https://rest.sandbox.lalamove.com";

        public QuotationCommand(OptionViewModel optionOrderViewModel)
        {
            this.OptionOrderViewModel = optionOrderViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            Stop[] stops = new Stop[] { SessionStatic.ShopStop, SessionStatic.CusStop };
            OrderItem item = ChangeOrderedstoOrderItem(OptionOrderViewModel.OrderedFood);
            if (stops[1] != null)
            {
                string message = await CalculateRequest(stops, item);
                if (message == "ERROR")
                {
                    MessageBox.Show("Không báo giá đc");
                    return;
                }
                JObject jmessage = JObject.Parse(message);
                int fee = Int32.Parse(jmessage["data"]["priceBreakdown"]["total"].ToString());
                OptionOrderViewModel.Shipfee = fee;
            }
            else
                MessageBox.Show("có lỗi");
            return;

        }



        private OrderItem ChangeOrderedstoOrderItem(ObservableCollection<DishModel> dishes)
        {
            if (dishes == null)
            {
                return null;
            }
            int quantity = 0;
            foreach (DishModel dish in dishes)
            { quantity += dish.Quantity; }

            OrderItem item = new OrderItem
            {
                Quantity = quantity.ToString()
            };
            return item;
        }
        private async Task<string> CalculateRequest(Stop[] stops, OrderItem orderItem)
        {
            string path = "/v3/quotations";

            var dictionary = new Dictionary<string, object>();
            dictionary.Add("serviceType", "MOTORCYCLE");
            dictionary.Add("specialRequests", new List<String>() { "THERMAL_BAG_2" });
            dictionary.Add("language", "vi_VN");
            dictionary.Add("stops", stops);
            dictionary.Add("isRouteOptimized", true);
            dictionary.Add("item", orderItem);
            var data = new Dictionary<string, object>();
            data.Add("data", dictionary);
            string postdata = JsonConvert.SerializeObject(data);

            var token = GenerateToken(postdata, HttpMethod.Post, path);

            var client = new HttpClient();
            var request = CreateRequestMessage(HttpMethod.Post, path, token, postdata);
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            { return result; }
            return "ERROR";
        }

        static HttpRequestMessage CreateRequestMessage(HttpMethod method, string path, string token, string body)
        {
            var url = $"{baseUrl}{path}";
            //var requestId = Guid.NewGuid().ToString();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.TryAddWithoutValidation("Authorization", $"hmac {token}");
            request.Headers.TryAddWithoutValidation("Market", "VN");
            request.Content = new StringContent(body);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            return request;
        }

        static string GenerateToken(string body, HttpMethod httpMethod, string path)
        {
            var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var method = httpMethod.Method.ToUpper();
            var rawSignature = $"{time}\r\n{method}\r\n{path}\r\n\r\n{body}";
            byte[] keyByte = Encoding.UTF8.GetBytes(secret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(rawSignature);
            byte[] hashmessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
            var signature = string.Concat(Array.ConvertAll(hashmessage, x => x.ToString("x2")));
            var token = $"{key}:{time}:{signature}";

            return token;
        }
    }
}
