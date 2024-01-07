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
    internal class AddProductCommand : CommandBase
    {
        AdminProductViewModel adminProductViewModel;
        ProductDAO productDAO = new ProductDAO();

        public AddProductCommand(AdminProductViewModel adminProductViewModel)
        {
            this.adminProductViewModel = adminProductViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            DateTime date = DateTime.Now;
            ProductModel model = new ProductModel(adminProductViewModel.newproduct);
            if (model.Product_name == null || model.Product_name == "" || model.Unit == null || model.Unit == "")
            {
                MessageBox.Show("Hàm hoá rõng không thể thêm");
                return;
            }                
            productDAO.CreateProduct(model);
            adminProductViewModel.Products.Add(model);
            adminProductViewModel.Products = adminProductViewModel.Products;
            MessageBox.Show("Đã thêm hàng hoá");
            return;
        }
    }
}
