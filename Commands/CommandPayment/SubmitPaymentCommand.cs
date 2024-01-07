using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.StaticClass;
using System.Threading.Tasks;
using System.Windows;
using DevCoffeeManagerApp.Models;
using System.Collections.ObjectModel;
using static MongoDB.Driver.WriteConcern;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using DevCoffeeManagerApp.Views;

namespace DevCoffeeManagerApp.Commands.CommandPayment
{
    public class SubmitPaymentCommand : CommandBase
    {
        ReceiptDAO receiptDAO = new ReceiptDAO();
        CustomerDAO customerDAO = new CustomerDAO();
        ProductDAO productDAO = new ProductDAO();
        private PaymentViewModel PaymentOrderViewModel;
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
            List<DiscountModel> discounts = new List<DiscountModel>();
            List<DishModel> dishesdb = new List<DishModel>();

            int total = PaymentOrderViewModel.Total;
            int plus_point = PaymentOrderViewModel.PlusPoint;
            int used_point = int.Parse(PaymentOrderViewModel.UsedPoint);
            string receipt_code = GenerateInvoiceNumber();
            DateTime current_date = PaymentOrderViewModel.CurrentDate;
            string guest_monney = PaymentOrderViewModel.InputMoney.Replace(" ", "");
            int change = PaymentOrderViewModel.Change;
            string staff_phonenumber = PaymentOrderViewModel.StaffPhoneNumber;
            int total_amount = PaymentOrderViewModel.TotalAmount;
            bool is_direct_payment = PaymentOrderViewModel.IsDirectPaymentChecked;

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

            if (SessionStatic.GetPhoneNumber != null && SessionStatic.Customer != null && SessionStatic.GetOrdereds != null)
            {
                if (CheckGuestMonney(total_amount, guest_monney))
                {
                    if (is_direct_payment == true && total != 0)
                    {
                        ReceiptModel receiptModel = new ReceiptModel(receipt_code, current_date, SessionStatic.Customer, tables, staff_phonenumber,
                            dishesdb, discounts, "Thanh toán bằng tiền mặt", used_point, total_amount, int.Parse(guest_monney), change);
                        receiptDAO.AddReceipt(receiptModel);
                        SessionStatic.SetReceipt = receiptModel;
                        SessionStatic.Customer.point = (SessionStatic.Customer.point + plus_point) - used_point;
                        customerDAO.UpdateCustomer(SessionStatic.Customer);
                        productDAO.MinusProduct(SessionStatic.GetOrdereds);
                        MessageBox.Show("Thanh toán thành công");
                        Receipt receipt = new Receipt();
                        receipt.Show();
                        SessionStatic.SetTables = null;
                        SessionStatic.SetOrdereds = null;
                        SessionStatic.Customer = null;
                        SessionStatic.SetReceipt = null;
                    }
                    else MessageBox.Show("Đơn đặt món chưa có đầy đủ thông tin");
                }
            }
            else if (SessionStatic.GetPhoneNumber != null && SessionStatic.Customer == null && SessionStatic.GetOrdereds != null)
            {
                if (CheckGuestMonney(total_amount, guest_monney))
                {
                    if (is_direct_payment == true && total != 0)
                    {
                        ReceiptModel receiptModel = new ReceiptModel(receipt_code, current_date, null, tables, staff_phonenumber,
                            dishesdb, discounts, "Thanh toán bằng tiền mặt", used_point, total_amount, int.Parse(guest_monney), change);
                        receiptDAO.AddReceipt(receiptModel);
                        SessionStatic.SetReceipt = receiptModel;
                        MessageBox.Show("Thanh toán thành công");
                        Receipt receipt = new Receipt();
                        receipt.Show();
                        SessionStatic.SetTables = null;
                        SessionStatic.SetOrdereds = null;
                        SessionStatic.Customer = null;
                        SessionStatic.SetReceipt = null;
                    }
                    else MessageBox.Show("Đơn đặt món chưa có đầy đủ thông tin");
                }
            }
            else MessageBox.Show("Đơn đặt món chưa có đầy đủ thông tin");
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
                if(int.TryParse(guestMonney, out _))
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
