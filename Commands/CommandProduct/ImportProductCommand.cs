using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
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
            if (parameter is ProductModel data && data.Stock >= 100 && data.EXP_date >= DateTime.Now)
            {
                return false;
            }
            return true;
        }
        public override void Execute(object parameter)
        {
            if (parameter is ProductModel data)
            {

                SupplyModel firstsupply = supplyDAO.GetUnUsingSupplyByName(data.Product_name);
                if (firstsupply == null || firstsupply.EXP_date <= DateTime.Now)
                {
                    MessageBox.Show("Hàng chưa được nhập");
                    return;
                }
                data.EXP_date = firstsupply.EXP_date;
                SupplyModel secondsupply = supplyDAO.GetUsingSupplyByName(data.Product_name);
                if (secondsupply != null)
                {
                    if (secondsupply.EXP_date <= DateTime.Now)
                    {
                        supplyDAO.SetStatus(secondsupply.Product_name, "Out of date");
                        data.Stock = firstsupply.Quantity * 1000;
                    }
                        
                    else
                    {
                        supplyDAO.SetStatus(secondsupply.Product_name, "Exhausted");
                        data.Stock += firstsupply.Quantity * 1000;
                    }
                }
                else
                    data.Stock = firstsupply.Quantity * 1000;

                supplyDAO.SetStatus(firstsupply.Product_name, "In-use");
                productDAO.UpdateProduct(data);
                adminProductViewModel.Products = adminProductViewModel.Products;
                MessageBox.Show("Đã nhập hàng");
            }
        }
    }
}
