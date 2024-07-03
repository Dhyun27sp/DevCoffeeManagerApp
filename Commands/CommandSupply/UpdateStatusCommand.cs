using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandSupply
{
    public class UpdateStatusCommand : CommandBase
    {
        AdminSupplyViewModel adminSupplyViewModel;
        SupplyDAO supplyDAO = new SupplyDAO();

        public UpdateStatusCommand(AdminSupplyViewModel adminSupplyViewModel)
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
            foreach (var supply in adminSupplyViewModel.Supplies)
            {
                if (date >= supply.EXP_date)
                {
                    supply.Status = "Out of date";
                    supplyDAO.SetStatus(supply.Product_name, supply.Status);
                }
            }
            adminSupplyViewModel.Supplies = adminSupplyViewModel.Supplies;
            MessageBox.Show("Đã cập nhật");
        }
    }
}
