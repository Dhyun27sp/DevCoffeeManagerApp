using DevCoffeeManagerApp.Shipping;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using DevCoffeeManagerApp.DAOs;

namespace DevCoffeeManagerApp.Commands.CommandBook
{
    public class BookCommand : CommandBase
    {
        ReceiptDAO receiptDAO = new ReceiptDAO();
        BookViewModel bookviewModel;
        private const string key = "pk_test_0aaaabba78a336de774fbbd3de8a299d";    // put your lalamove API key here
        private const string secret = "sk_test_vzTqN6KnvN9g/zpml//HB0p+tO7V6h0xV2/TMaSWP1tjLa1grVb8FrdG03egcjHn"; // put your lalamove API secret here
        const string baseUrl = "https://rest.sandbox.lalamove.com";
        public BookCommand(BookViewModel bookViewModel)
        {
            this.bookviewModel = bookViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            string[] stops = SessionStatic.StopsId;
            Contact[] contacts = new Contact[] { SessionStatic.ShopContact, SessionStatic.CusContact };

            if (stops[1] != null)
            {
                string message = await OrderRequest(stops, contacts);
                if (message == "ERROR")
                {
                    MessageBox.Show("Không báo giá đc");
                    return;
                }
                else if (message == null)
                    return;
                JObject jmessage = JObject.Parse(message);

                string receiptId = SessionStatic.GetReceipt.receipt_code;
                string orderId = jmessage["data"]["orderId"].ToString();
                receiptDAO.UpdateOrder(receiptId, orderId);
                int fee = Int32.Parse(jmessage["data"]["priceBreakdown"]["total"].ToString());
                MessageBox.Show("Đã đặt thành công giao hàng với phí giao hàng là "+fee.ToString()+"VNĐ.");
                SessionStatic.CusStop = new Stop();
                SessionStatic.CusContact = new Contact();
                SessionStatic.SetReceipt = null;
                SessionStatic.ShipFlag = false;
                SessionStatic.QuotationId = null;
                SessionStatic.StopsId = null;
            }
            else
                MessageBox.Show("có lỗi");
            return;
        }
        private async Task<string> OrderRequest(string[] stops, Contact[] contacts)
        {
            string path = "/v3/orders";

            var dictionary = new Dictionary<string, object>();
            dictionary.Add("quotationId", SessionStatic.QuotationId);

            var sender = new Dictionary<string, object>();
            sender.Add("stopId", stops[0]);
            sender.Add("name", contacts[0].Name);
            sender.Add("phone", contacts[0].Phone);
            dictionary.Add("sender", sender);

            var recipients = new Dictionary<string, object>();
            recipients.Add("stopId", stops[1]);
            recipients.Add("name", contacts[1].Name);
            recipients.Add("phone", contacts[1].Phone);
            dictionary.Add("recipients", new List<object>() { recipients });

            dictionary.Add("isPODEnabled", true);

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
