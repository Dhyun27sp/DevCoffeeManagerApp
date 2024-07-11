using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandSupply
{
    public class DeleteSupplyCommand : CommandBase
    {
        AdminSupplyViewModel adminSupplyViewModel;
        SupplyDAO supplytDAO = new SupplyDAO();

        public DeleteSupplyCommand(AdminSupplyViewModel adminSupplyViewModel)
        {
            this.adminSupplyViewModel = adminSupplyViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            if (parameter is SupplyModel supply)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    supplytDAO.DeleteSupplyByProductName(supply.Product_name);
                    adminSupplyViewModel.Supplies.Remove(supply);
                    adminSupplyViewModel.Supplies = adminSupplyViewModel.Supplies;
                    MessageBox.Show("Xóa thành công");
                    adminSupplyViewModel.AllSupplies = supplytDAO.GetAllSupplies();
                }
            }
        }
    }
}
