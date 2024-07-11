using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;
using DevCoffeeManagerApp.Shipping;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using DevCoffeeManagerApp.DAOs;

namespace DevCoffeeManagerApp.Commands.CommandShip
{
    internal class QuotationCommand : CommandBase
    {
        ReceiptDAO receiptDAO = new ReceiptDAO();
        const string key = "pk_test_0aaaabba78a336de774fbbd3de8a299d";    // put your lalamove API key here
        const string secret = "sk_test_vzTqN6KnvN9g/zpml//HB0p+tO7V6h0xV2/TMaSWP1tjLa1grVb8FrdG03egcjHn"; // put your lalamove API secret here
        const string baseUrl = "https://rest.sandbox.lalamove.com";

        public QuotationCommand()
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            if (SessionStatic.CusStop == null)
            {
                MessageBox.Show("Chưa đặt hàng");
                return;
            }
            Stop[] stops = new Stop[] { SessionStatic.ShopStop, SessionStatic.CusStop };
            var orders = new ObservableCollection<DishModel>(receiptDAO.FindOrderbyReceiptCode(SessionStatic.GetReceipt.receipt_code));
            OrderItem item = ChangeOrderedstoOrderItem(orders);
            if (stops[1] != null)
            {
                string message = await CalculateRequest(stops, item);
                if (message == "ERROR")
                {
                    MessageBox.Show("Không báo giá đc");
                    return;
                }
                else if (message == null)
                    return;
                JObject jmessage = JObject.Parse(message);

                string quotationId = jmessage["data"]["quotationId"].ToString();
                SessionStatic.QuotationId = quotationId;

                int fee = Int32.Parse(jmessage["data"]["priceBreakdown"]["total"].ToString());
                int total_fee = Security_Request.IncreasePriceRoundUpToThousand(fee);
                SessionStatic.ShipFee = total_fee;

                string[] stopsId = new string[2];
                stopsId[0] = jmessage["data"]["stops"][0]["stopId"].ToString();
                stopsId[1] = jmessage["data"]["stops"][1]["stopId"].ToString();
                SessionStatic.StopsId = stopsId;

                SessionStatic.ShipFlag = true;
                MessageBox.Show(total_fee.ToString());
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
            if (orderItem == null)
            {
                MessageBox.Show("Chưa đặt món");
                return null;
            }
            dictionary.Add("item", orderItem);
            var data = new Dictionary<string, object>();
            data.Add("data", dictionary);
            string postdata = JsonConvert.SerializeObject(data);

            var token = Security_Request.GenerateToken(postdata, key, secret, HttpMethod.Post, path);

            var client = new HttpClient();
            var request = Security_Request.CreateRequestMessage(HttpMethod.Post, baseUrl, path, token, postdata);
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (response.StatusCode == HttpStatusCode.Created)
            { return result; }
            return "ERROR";
        }


    }
}
