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
            if (SessionStatic.GetOrdereds != null)
            {
                List<DishModel> dishes = new List<DishModel>(SessionStatic.GetOrdereds);
                foreach (var dish in dishes)
                {
                    DishModel dishdb = new DishModel(dish._id,dish.dish_name, null, dish.price, null, null, null, dish.Saleprice, dish.Quantity, dish.Amount);
                    dishesdb.Add(dishdb);
                }
            }

            if (SessionStatic.GetPhoneNumber != null && SessionStatic.Customer != null && SessionStatic.GetOrdereds != null)
            {
                if (PaymentOrderViewModel.IsDirectPaymentChecked == true && PaymentOrderViewModel.Total != 0)
                {
                    ReceiptModel receiptModel = new ReceiptModel(PaymentOrderViewModel.CurrentDate,
                    SessionStatic.Customer, tables, PaymentOrderViewModel.StaffPhoneNumber,
                    dishesdb, discounts, "Thanh toán bằng tiền mặt");
                    receiptDAO.AddReceipt(receiptModel);
                    SessionStatic.Customer.point = (SessionStatic.Customer.point + PaymentOrderViewModel.PlusPoint) - int.Parse(PaymentOrderViewModel.UsedPoint);
                    customerDAO.UpdateCustomer(SessionStatic.Customer);
                    MessageBox.Show("Thanh toán thành công");
                    SessionStatic.SetTables = null;
                    SessionStatic.SetOrdereds = null;
                    SessionStatic.Customer = null;
                }
                else MessageBox.Show("Dữ liệu không đủ");
            }
            else if (SessionStatic.GetPhoneNumber != null && SessionStatic.Customer == null && SessionStatic.GetOrdereds != null)
            {
                if (PaymentOrderViewModel.IsDirectPaymentChecked == true && PaymentOrderViewModel.Total != 0)
                {
                    ReceiptModel receiptModel = new ReceiptModel(PaymentOrderViewModel.CurrentDate, 
                        null, tables, PaymentOrderViewModel.StaffPhoneNumber, dishesdb, discounts, "Thanh toán bằng tiền mặt");
                    receiptDAO.AddReceipt(receiptModel);
                    MessageBox.Show("Thanh toán thành công");
                    SessionStatic.SetTables = null;
                    SessionStatic.SetOrdereds = null;
                    SessionStatic.Customer = null;
                }
                else MessageBox.Show("Dữ liệu không đủ");
            }
            else MessageBox.Show("Dữ liệu không đủ");
        }

    }
}
