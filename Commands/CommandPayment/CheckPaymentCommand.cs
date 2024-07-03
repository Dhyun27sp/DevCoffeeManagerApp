using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandPayment
{
    internal class CheckPaymentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            string partnerCode = SessionStatic.PartnerCode; // Thay bằng mã đối tác của bạn
            string requestId = Guid.NewGuid().ToString();
            string accesskey = SessionStatic.AccessKey;
            string orderIdToCheck = SessionStatic.GetReceipt.receipt_code; // Thay bằng mã đơn hàng cần kiểm tra

            // Tạo chuỗi signature
            string signatureData = $"accessKey={accesskey}&orderId={orderIdToCheck}&partnerCode={partnerCode}&requestId={requestId}";
            string signature = CalculateHmacSHA256Signature(signatureData, SessionStatic.SerectKey);

            // Tạo JSON request
            JObject request = new JObject
            {
                { "partnerCode", partnerCode },
                { "requestId", requestId },
                { "orderId", orderIdToCheck },
                { "signature", signature },
                { "lang", "en" }
            };

            // Gửi request đến Momo và xử lý response ở đây
            string response = SendRequestToMomo("https://test-payment.momo.vn/v2/gateway/api/query", request.ToString());

            // Xử lý response từ Momo ở đây, bạn có thể hiển thị thông tin giao dịch hoặc thông báo cho người dùng.
            JObject jmessage = JObject.Parse(response);

            string kq = jmessage.GetValue("resultCode").ToString();

            if (kq == "0")
            {
                MessageBox.Show("Giao dịch thành công!");
                Receipt receipt = new Receipt();
                receipt.Show();
            }
            else
                MessageBox.Show("Giao dịch thất bại");
        }

        // Hàm tính chữ ký HMAC-SHA256
        private string CalculateHmacSHA256Signature(string data, string key)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // Hàm gửi request đến Momo và nhận response
        private string SendRequestToMomo(string url, string requestData)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    return client.UploadString(url, "POST", requestData);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi gửi request
                return ex.Message;
            }
        }
    }
}
