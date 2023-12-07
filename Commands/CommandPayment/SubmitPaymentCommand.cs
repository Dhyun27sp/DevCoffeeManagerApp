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

namespace DevCoffeeManagerApp.Commands.CommandPayment
{
    public class SubmitPaymentCommand : CommandBase
    {
        ReceiptDAO receiptDAO = new ReceiptDAO();
        CustomerDAO customerDAO = new CustomerDAO();
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
            List<TableModel> tables = new List<TableModel>();
            List<DiscountModel> discounts = new List<DiscountModel>();
            List<DishModel> dishesdb = new List<DishModel>();

            int total = PaymentOrderViewModel.Total;
            int plus_point = PaymentOrderViewModel.PlusPoint;
            string used_point = PaymentOrderViewModel.UsedPoint;
            string current_date = PaymentOrderViewModel.CurrentDate;
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
                        ReceiptModel receiptModel = new ReceiptModel(current_date, SessionStatic.Customer, tables, staff_phonenumber,
                            dishesdb, discounts, "Thanh toán bằng tiền mặt", total_amount, int.Parse(guest_monney), change);
                        receiptDAO.AddReceipt(receiptModel);
                        SessionStatic.Customer.point = (SessionStatic.Customer.point + plus_point) - int.Parse(used_point);
                        customerDAO.UpdateCustomer(SessionStatic.Customer);
                        MessageBox.Show("Thanh toán thành công");
                        SessionStatic.SetTables = null;
                        SessionStatic.SetOrdereds = null;
                        SessionStatic.Customer = null;
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
                        ReceiptModel receiptModel = new ReceiptModel(current_date, null, tables, staff_phonenumber,
                            dishesdb, discounts, "Thanh toán bằng tiền mặt", total_amount, int.Parse(guest_monney), change);
                        receiptDAO.AddReceipt(receiptModel);
                        MessageBox.Show("Thanh toán thành công");
                        SessionStatic.SetTables = null;
                        SessionStatic.SetOrdereds = null;
                        SessionStatic.Customer = null;
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
    }
}
