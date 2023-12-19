using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.AdminCommand.DiscountCommands
{
    public class ClickButtonCommand: CommandBase
    {
        private AdminDiscountViewModel viewModel;
        private string action;
        DiscountDAO discountDAO = new DiscountDAO();
        public ClickButtonCommand(AdminDiscountViewModel viewModel, string action) { 
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
                    chooseDiscount(parameter);
                    return;
                case "add":
                    addDiscount(parameter);
                    return;
                case "deletef":
                    DeletefieldDiscount(parameter);
                    return;
                case "update":
                    UpdateDistcount(parameter);
                    return;
                case "deleted":
                    DeleteDiscount(parameter);
                    return;
            }
        }

        private void chooseDiscount(object parameter) {
            if (parameter is ListView listdiscount)
            {
                if (listdiscount.SelectedItem != null)
                {
                    Tuple<DiscountModel, int> selectedItem = (Tuple<DiscountModel, int>)listdiscount.SelectedItem;
                    viewModel.NotallowedAdd = true;
                    viewModel.DiscountID = selectedItem.Item1.discountid;
                    viewModel.Discountname = selectedItem.Item1.name;
                    viewModel.Description = selectedItem.Item1.detail;
                    viewModel.Daystart = selectedItem.Item1.daystart;
                    viewModel.Dayend = selectedItem.Item1.dayend;
                    viewModel.Value = selectedItem.Item1.value_dis;
                }
            }

        }
        private void addDiscount(object parameter) {
            if (viewModel.NotallowedAdd == true)
            {
                MessageBox.Show("Khuyến mã tồn tại");
            }
            else
            {
                if (discountDAO.findisbyId(viewModel.DiscountID) != null)
                {
                    MessageBox.Show("Khuyến mã tồn tại");
                }
                else
                {
                    ApplyModel applyModel = new ApplyModel();
                    DiscountModel newdiscount = new DiscountModel(viewModel.DiscountID, viewModel.Discountname, viewModel.Description, applyModel, viewModel.Daystart.Value, viewModel.Dayend.Value, viewModel.Value);
                    discountDAO.createdistcount(newdiscount);
                    MessageBox.Show("Thêm Khuyến mãi thành công");
                    viewModel.Discounts.Add(newdiscount);
                    viewModel.Discounts = viewModel.Discounts;
                }
            }
        }
        private void DeletefieldDiscount(object parameter) {
            viewModel.NotallowedAdd = false;
            viewModel.DiscountID = "";
            viewModel.Discountname = "";
            viewModel.Description = "";
            viewModel.Daystart = null ;
            viewModel.Dayend = null;
            viewModel.Value = "";

        }
        private void UpdateDistcount(object parameter) {
            DiscountModel discountModel = discountDAO.findisbyId(viewModel.DiscountID);
            if (discountModel != null)
            {
                ApplyModel applyModel = new ApplyModel();
                DiscountModel newdiscount = new DiscountModel(viewModel.DiscountID, viewModel.Discountname, viewModel.Description, applyModel, viewModel.Daystart.Value, viewModel.Dayend.Value, viewModel.Value);
                discountDAO.UpdateDiscount(newdiscount);
                MessageBox.Show("Cập nhật thành công");
                viewModel.Discounts = new System.Collections.ObjectModel.ObservableCollection<DiscountModel>(discountDAO.ReadDiscountAll());
            }
            else
            {
                MessageBox.Show("Ko thể cập nhật khi vì discount ko tồn tại");
            }
        }
        private void DeleteDiscount(object parameter) {
            if (parameter is DiscountModel discount)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    discountDAO.DeleteDiscountBydiscountid(discount.discountid);
                    MessageBox.Show("Xóa thành công");
                    viewModel.Discounts.Remove(discount);
                    viewModel.Discounts = viewModel.Discounts;
                }
            }
        }
    }
}
