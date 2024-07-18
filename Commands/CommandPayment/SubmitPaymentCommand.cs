using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Models;
using System.Collections.ObjectModel;
using DevCoffeeManagerApp.Views;
using DevCoffeeManagerApp.MomoPayment;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using QRCoder;
using System.Drawing;

namespace DevCoffeeManagerApp.Commands.CommandPayment
{
    public class SubmitPaymentCommand : CommandBase
    {
        ReceiptDAO receiptDAO = new ReceiptDAO();
        CustomerDAO customerDAO = new CustomerDAO();
        ProductDAO productDAO = new ProductDAO();
        TableDAO tableDAO = new TableDAO();
        private PaymentViewModel PaymentOrderViewModel;

        private string partnerCode = SessionStatic.PartnerCode;
        private string accessKey = SessionStatic.AccessKey;
        private string serectkey = SessionStatic.SerectKey;
        public SubmitPaymentCommand(PaymentViewModel PaymentOrderViewModel)
        {
            this.PaymentOrderViewModel = PaymentOrderViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            ObservableCollection<TableModel> tables = SessionStatic.GetTables;
            List<DishModel> dishesdb = new List<DishModel>();

            string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";

            int total = PaymentOrderViewModel.Total;
            int plus_point = PaymentOrderViewModel.PlusPoint;
            int used_point = int.Parse(PaymentOrderViewModel.UsedPoint);

            string staff_phonenumber = PaymentOrderViewModel.StaffPhoneNumber;
            string receipt_code = GenerateInvoiceNumber();
            int change = PaymentOrderViewModel.Change;
            DateTime current_date = PaymentOrderViewModel.CurrentDate;
            string guest_monney = PaymentOrderViewModel.InputMoney.Replace(" ", "");

            int total_amount = PaymentOrderViewModel.TotalAmount;
            bool is_direct_payment = PaymentOrderViewModel.IsDirectPaymentChecked;
            bool is_momo_payment = PaymentOrderViewModel.IsMoMoChecked;

            if (SessionStatic.GetOrdereds != null)
            {
                List<DishModel> dishes = new List<DishModel>(SessionStatic.GetOrdereds);
                foreach (var dish in dishes)
                {
                    DishModel dishdb = new DishModel(dish._id, dish.dish_name, null, dish.price, null, null, null,
                        dish.Saleprice, dish.Quantity, dish.Amount);
                    dishesdb.Add(dishdb);
                }
            }
            else
            {
                MessageBox.Show("Hãy đặt món");
                return;
            }            
            if (SessionStatic.GetPhoneNumber != null)
                if (SessionStatic.Customer != null)
                {
                    if (is_momo_payment)
                    {
                        string orderInfo = "Khách Hàng " + SessionStatic.Customer.name;
                        string redirectUrl = "https://www.google.com/";//Link trang Web cua doanh nghiep
                        string ipnUrl = "https://www.google.com/";//
                        string requestType = "captureWallet";
                        string tongtieng = total_amount.ToString();
                        string orderId = receipt_code;
                        string requestId = receipt_code;
                        string extraData = "";
                        ;
                        //Before sign HMAC SHA256 signature
                        string rawHash = "accessKey=" + accessKey +
                            "&amount=" + tongtieng +
                            "&extraData=" + extraData +
                            "&ipnUrl=" + ipnUrl +
                            "&orderId=" + orderId +
                            "&orderInfo=" + orderInfo +
                            "&partnerCode=" + partnerCode +
                            "&redirectUrl=" + redirectUrl +
                            "&requestId=" + requestId +
                            "&requestType=" + requestType;

                        MoMoSecurity crypto = new MoMoSecurity();
                        //sign signature SHA256
                        string signature = crypto.signSHA256(rawHash, serectkey);

                        //build body json request
                        JObject message = new JObject
                    {
                                    { "partnerCode", partnerCode },
                                    { "partnerName", "Cửa hàng Cà phê DevShop" },
                                    { "storeId", "DevShop" },
                                    { "requestId", requestId },
                                    { "amount", tongtieng },
                                    { "orderId", orderId },
                                    { "orderInfo", orderInfo },
                                    { "redirectUrl", redirectUrl },
                                    { "ipnUrl", ipnUrl },
                                    { "lang", "vi" },
                                    { "extraData", extraData },
                                    { "requestType", requestType },
                                    { "signature", signature }
                    };
                        string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
                        JObject jmessage = JObject.Parse(responseFromMomo);
                        DialogResult result = MessageBox.Show("Ấn OK để tới trang thanh toán", "Thông báo", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                        {
                            ReceiptModel receiptModel = new ReceiptModel(receipt_code, current_date, SessionStatic.Customer, tables, staff_phonenumber,
                                dishesdb, "Thanh toán bằng Momo", used_point, total_amount, 0, 0, PaymentOrderViewModel.Additionalinfor);
                            SessionStatic.SetReceipt = receiptModel;
                            SessionStatic.Customer.point = (SessionStatic.Customer.point + plus_point) - used_point;
                            QRCodeGenerator qrGenerator = new QRCodeGenerator();
                            QRCodeData qrCodeData = qrGenerator.CreateQrCode(jmessage.GetValue("qrCodeUrl").ToString(), QRCodeGenerator.ECCLevel.Q);
                            QRCode qrCode = new QRCode(qrCodeData);
                            Bitmap qrCodeImage = qrCode.GetGraphic(20);
                            SessionStatic.Img = qrCodeImage;
                            //qrCodeImage.Save("D:\\hyu\\img.png",ImageFormat.Png);
                            Qr qr = new Qr();
                            qr.Show();

                        }
                    }
                    else if (CheckGuestMonney(total_amount, guest_monney))
                    {
                        if (is_direct_payment == true && total != 0)
                        {
                            ReceiptModel receiptModel = new ReceiptModel(receipt_code, current_date, SessionStatic.Customer, tables, staff_phonenumber,
                                dishesdb, "Thanh toán bằng tiền mặt", used_point, total_amount, int.Parse(guest_monney), change, PaymentOrderViewModel.Additionalinfor);
                            receiptDAO.AddReceipt(receiptModel);
                            SessionStatic.SetReceipt = receiptModel;
                            SessionStatic.Customer.point = (SessionStatic.Customer.point + plus_point) - used_point;
                            customerDAO.UpdateCustomer(SessionStatic.Customer);
                            productDAO.MinusProduct(SessionStatic.GetOrdereds);
                            if (SessionStatic.GetTables != null)
                            {
                                foreach (var item in SessionStatic.GetTables)
                                    tableDAO.SetStatus(item.No_);
                            }
                            MessageBox.Show("Thanh toán thành công");
                            Receipt receipt = new Receipt();
                            receipt.Show();
                            SessionStatic.SetTables = null;
                            SessionStatic.SetOrdereds = null;
                            SessionStatic.Customer = null;
                            if (!SessionStatic.ShipFlag)
                                SessionStatic.SetReceipt = null;
                        }
                        else MessageBox.Show("Đơn đặt món chưa có đầy đủ thông tin1");
                    }
                }
                else if (SessionStatic.Customer == null)
                {
                    if (is_momo_payment)
                    {
                        string orderInfo = "Khách Hàng";
                        string redirectUrl = "https://www.google.com/";//Link trang Web cua doanh nghiep
                        string ipnUrl = "https://www.google.com/";//
                        string requestType = "captureWallet";
                        string tongtieng = total_amount.ToString();
                        string orderId = receipt_code;
                        string requestId = receipt_code;
                        string extraData = "";
                        ;
                        //Before sign HMAC SHA256 signature
                        string rawHash = "accessKey=" + accessKey +
                            "&amount=" + tongtieng +
                            "&extraData=" + extraData +
                            "&ipnUrl=" + ipnUrl +
                            "&orderId=" + orderId +
                            "&orderInfo=" + orderInfo +
                            "&partnerCode=" + partnerCode +
                            "&redirectUrl=" + redirectUrl +
                            "&requestId=" + requestId +
                            "&requestType=" + requestType;

                        MoMoSecurity crypto = new MoMoSecurity();
                        //sign signature SHA256
                        string signature = crypto.signSHA256(rawHash, serectkey);

                        //build body json request
                        JObject message = new JObject
                                    {
                                    { "partnerCode", partnerCode },
                                    { "partnerName", "Cửa hàng Cà phê DevShop" },
                                    { "storeId", "DevShop" },
                                    { "requestId", requestId },
                                    { "amount", tongtieng },
                                    { "orderId", orderId },
                                    { "orderInfo", orderInfo },
                                    { "redirectUrl", redirectUrl },
                                    { "ipnUrl", ipnUrl },
                                    { "lang", "vi" },
                                    { "extraData", extraData },
                                    { "requestType", requestType },
                                    { "signature", signature }
                                };
                        string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
                        JObject jmessage = JObject.Parse(responseFromMomo);
                        DialogResult result = MessageBox.Show("Ấn OK để tới trang thanh toán", "Thông báo", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                        {

                            ReceiptModel receiptModel = new ReceiptModel(receipt_code, current_date, null, tables, staff_phonenumber,
                                dishesdb, "Thanh toán bằng Momo", used_point, total_amount, 0, 0, PaymentOrderViewModel.Additionalinfor);
                            SessionStatic.SetReceipt = receiptModel;
                            QRCodeGenerator qrGenerator = new QRCodeGenerator();
                            QRCodeData qrCodeData = qrGenerator.CreateQrCode(jmessage.GetValue("qrCodeUrl").ToString(), QRCodeGenerator.ECCLevel.Q);
                            QRCode qrCode = new QRCode(qrCodeData);
                            Bitmap qrCodeImage = qrCode.GetGraphic(20);
                            SessionStatic.Img = qrCodeImage;
                            Qr qr = new Qr();
                            qr.Show();
                        }
                    }
                    else if (CheckGuestMonney(total_amount, guest_monney))
                    {
                        if (is_direct_payment == true && total != 0)
                        {
                            ReceiptModel receiptModel = new ReceiptModel(receipt_code, current_date, null, tables, staff_phonenumber,
                                dishesdb, "Thanh toán bằng tiền mặt", used_point, total_amount, int.Parse(guest_monney), change, PaymentOrderViewModel.Additionalinfor);
                            receiptDAO.AddReceipt(receiptModel);
                            SessionStatic.SetReceipt = receiptModel;
                            productDAO.MinusProduct(SessionStatic.GetOrdereds);
                            if (SessionStatic.GetTables != null)
                            {
                                foreach (var item in SessionStatic.GetTables)
                                    tableDAO.SetStatus(item.No_);
                            }
                            MessageBox.Show("Thanh toán thành công");
                            Receipt receipt = new Receipt();
                            receipt.Show();
                            SessionStatic.SetTables = null;
                            SessionStatic.SetOrdereds = null;
                            SessionStatic.Customer = null;
                            if (!SessionStatic.ShipFlag)
                                SessionStatic.SetReceipt = null;
                        }
                        else MessageBox.Show("Đơn đặt món chưa có đầy đủ thông tin2");
                    }
                }
                else MessageBox.Show("Đơn đặt món chưa có đầy đủ thông tin3");
        }

        private bool CheckGuestMonney(int totalAmount, string guestMonney)
        {
            if (totalAmount == 0)
            {
                if (guestMonney == "0")
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Tiền khách đưa không hợp lệ");
                    return false;
                }
            }
            else
            {
                if (int.TryParse(guestMonney, out _))
                {
                    if (guestMonney == "0")
                    {
                        MessageBox.Show("Tiền khách đưa không đủ");
                        return false;
                    }
                    else if (guestMonney == "")
                    {
                        MessageBox.Show("Tiền khách đưa không hợp lệ");
                        return false;
                    }
                    else if (int.Parse(guestMonney) >= totalAmount)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Tiền khách đưa không đủ");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Tiền khách đưa không hợp lệ");
                    return false;
                }
            }
        }
        private static string GenerateInvoiceNumber()
        {
            // Sử dụng thời gian hiện tại để tạo mã hóa đơn
            string invoiceNumber = $"INV-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            return invoiceNumber;
        }


    }
}
