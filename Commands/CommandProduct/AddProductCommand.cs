using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
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
                MessageBox.Show("Hàng hóa rỗng, không thể thêm");
                return;
            }
            ProductModel existmodel = productDAO.GetProductbyName(model.Product_name);
            if (existmodel != null)
            {
                MessageBox.Show("Đã có sản phẩm này");
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
