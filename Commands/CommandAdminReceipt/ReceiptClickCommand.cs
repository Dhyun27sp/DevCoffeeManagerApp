using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.AdminCommand.ReceiptCommands
{
    public class ReceiptClickCommand : CommandBase
    {
        private AdminReceiptViewModel viewModel;
        private string action;
        ReceiptDAO receiptDAO = new ReceiptDAO();
        MenuDAO menuDao = new MenuDAO();
        public ReceiptClickCommand(AdminReceiptViewModel viewModel, string action)
        {
            this.action = action;
            this.viewModel = viewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            switch (action)
            {
                case "choose":
                    chooseReceipt(parameter);
                    return;
                case "deleted":
                    deleteReceipt(parameter);
                    return;
            }
        }
        private void chooseReceipt(object parameter) {
            if (parameter is ListView listdish)
            {
                if (listdish.SelectedItem != null)
                {
                    Tuple<ReceiptModel, int, string> selectedItem = (Tuple<ReceiptModel, int, string>)listdish.SelectedItem;
                    if (selectedItem.Item1.customer != null) {
                        viewModel.Customer = new ObservableCollection<CustomerModel>();
                        viewModel.Customer.Add(selectedItem.Item1.customer);
                        viewModel.Customer = viewModel.Customer;
                    }
                    else viewModel.Customer = new ObservableCollection<CustomerModel>();
                    viewModel.ListDish = new ObservableCollection<DishModel>(selectedItem.Item1.Dishes);
                }
            }
        }
        private void deleteReceipt(object parameter) {
            if (parameter is ReceiptModel receipt)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    receiptDAO.DeleteReceiptByID(receipt._id);
                    MessageBox.Show("Xóa thành công");
                    viewModel.Receipts.Remove(receipt);
                    viewModel.Receipts = viewModel.Receipts;
                }
            }
        }
    }
}
