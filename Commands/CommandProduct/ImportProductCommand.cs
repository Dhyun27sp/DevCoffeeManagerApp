using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandProduct
{
    internal class ImportProductCommand : CommandBase
    {
        AdminProductViewModel adminProductViewModel;
        SupplyDAO supplyDAO = new SupplyDAO();
        ProductDAO productDAO = new ProductDAO();

        public ImportProductCommand(AdminProductViewModel adminProductViewModel)
        {
            this.adminProductViewModel = adminProductViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            if (parameter is ProductModel data)
            {
                SupplyModel firstsupply = supplyDAO.GetSupplyByName(data.Product_name);
                data.EXP_date = firstsupply.EXP_date;
                data.Stock = firstsupply.Quantity*1000;
                supplyDAO.SetStatus(firstsupply.Product_name, "In-use");
                productDAO.UpdateProduct(data);
                adminProductViewModel.Products = adminProductViewModel.Products;
                MessageBox.Show("Đã nhập hàng");
            }
        }
    }
}
