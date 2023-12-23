using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            SupplyModel model = adminSupplyViewModel.newsupply;
            model.Date = date;
            model.Status = "Unused";
            supplyDAO.createSupply(model);
            adminSupplyViewModel.Supplies.Add(model);
            adminSupplyViewModel.Supplies = adminSupplyViewModel.Supplies;
            MessageBox.Show("Đã thêm hàng hoá");
        }
    }
}
