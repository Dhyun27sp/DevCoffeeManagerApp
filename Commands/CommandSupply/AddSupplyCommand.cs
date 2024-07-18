using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandSupply
{
    public class AddSupplyCommand : CommandBase
    {
        AdminSupplyViewModel adminSupplyViewModel;
        SupplyDAO supplyDAO = new SupplyDAO();

        public AddSupplyCommand(AdminSupplyViewModel adminSupplyViewModel)
        {
            this.adminSupplyViewModel = adminSupplyViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            DateTime date = DateTime.Now;
            SupplyModel model = new SupplyModel(adminSupplyViewModel.newsupply);
            if (model.Product_name == null || model.Product_name == "" || model.Manufacturer == null || model.Manufacturer == "" || model.Unit == null || model.Unit == "" || model.Date == null || model.EXP_date == null || model.MFG_date == null || model.Price < 1000 || model.Quantity <= 0)
            {
                MessageBox.Show("Đơn hàng chưa đủ thông tin");
                return;
            }
            if (model.EXP_date <= date)
            {
                MessageBox.Show("Hàng nhập đã hết hạn");
                return;
            }
            model.Date = date;
            model.Status = "Unused";
            supplyDAO.createSupply(model);
            adminSupplyViewModel.Supplies.Add(model);
            adminSupplyViewModel.Supplies = adminSupplyViewModel.Supplies;
            MessageBox.Show("Đã thêm hàng hoá");
            return;
        }
    }
}
