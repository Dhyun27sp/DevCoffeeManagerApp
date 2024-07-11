using DevCoffeeManagerApp.Shipping;
using DevCoffeeManagerApp.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using DevCoffeeManagerApp.DAOs;
using System.Collections.ObjectModel;
using DevCoffeeManagerApp.Models;
using Microsoft.Web.WebView2.Wpf;

namespace DevCoffeeManagerApp.Commands.CommandCheck
{
    internal class CheckCommand : CommandBase
    {
        ReceiptDAO receiptDAO = new ReceiptDAO();
        CheckViewModel checkViewModel;
        private const string key = "pk_test_0aaaabba78a336de774fbbd3de8a299d";    // put your lalamove API key here
        private const string secret = "sk_test_vzTqN6KnvN9g/zpml//HB0p+tO7V6h0xV2/TMaSWP1tjLa1grVb8FrdG03egcjHn"; // put your lalamove API secret here
        const string baseUrl = "https://rest.sandbox.lalamove.com";
        public CheckCommand(CheckViewModel checkViewModel)
        {
            this.checkViewModel = checkViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            ReceiptModel receiptModel = receiptDAO.FindReceiptbyReceiptCode(checkViewModel.ReceiptCode);
            if (receiptModel == null)
            {
                MessageBox.Show("Mã hoá đơn không đúng");
                return;

            }
            string orderId = receiptModel.ship_code;
            string message = await CheckRequest(orderId);
            if (message == "ERROR")
            {
                MessageBox.Show("Không thấy đơn hàng");
                return;
            }
            else if (message == null)
                return;
            JObject jmessage = JObject.Parse(message);
            checkViewModel.OrderId = orderId;
            checkViewModel.OrderedFood = new ObservableCollection<DishModel>(receiptDAO.FindOrderbyReceiptCode(checkViewModel.ReceiptCode));
            checkViewModel.CusName = jmessage["data"]["stops"][1]["name"].ToString();
            checkViewModel.CusPhone = jmessage["data"]["stops"][1]["phone"].ToString();
            checkViewModel.Address = jmessage["data"]["stops"][1]["address"].ToString();

            if (parameter is WebView2 webview)
            {
                webview.CoreWebView2.Navigate(jmessage["data"]["shareLink"].ToString());
            }
            return;
        }
        private async Task<string> CheckRequest(string orderId)
        {
            string path = "/v3/orders/" + orderId;
            var token = Security_Request.GenerateToken(null, key, secret, HttpMethod.Get, path);
            var client = new HttpClient();
            var request = Security_Request.CreateRequestMessage(HttpMethod.Get, baseUrl, path, token, null);
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (response.StatusCode == HttpStatusCode.OK)
            { return result; }
            return "ERROR";
        }
    }
}
