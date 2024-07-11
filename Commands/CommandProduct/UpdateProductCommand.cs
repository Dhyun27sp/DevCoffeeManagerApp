using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandProduct
{
    public class UpdateProductCommand : CommandBase
    {
        private AdminProductViewModel viewModel;
        private string action;
        ProductDAO productDAO = new ProductDAO();
        MenuDAO menuDao = new MenuDAO();
        public UpdateProductCommand(AdminProductViewModel viewModel, string action)
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
                    chooseDiscount(parameter);
                    return;
                case "update":
                    UpdateDistcount(parameter);
                    return;
            }
        }
        private void chooseDiscount(object parameter)
        {
            if (parameter is ListView lvProduct)
            {
                if (lvProduct.SelectedItem != null)
                {
                    Tuple<ProductModel, int> selectedItem = (Tuple<ProductModel, int>)lvProduct.SelectedItem;
                    viewModel.Products_name = selectedItem.Item1.Product_name;
                    viewModel.Products_unit = selectedItem.Item1.Unit;
                    viewModel.Newproduct = selectedItem.Item1;
                }
            }
        }

        private void UpdateDistcount(object parameter)
        {
            if (viewModel.Newproduct == null)
            {
                MessageBox.Show("Chọn sản phảm cần nhập hàng");
                return;
            }
            ProductModel productModel = productDAO.GetProductbyName(viewModel.Newproduct.Product_name);
            if (productModel != null)
            {
                productDAO.DeleteProductByProductName(viewModel.Newproduct.Product_name);
                viewModel.Newproduct.Product_name = viewModel.Products_name;
                viewModel.Newproduct.Unit = viewModel.Products_unit;
                productDAO.CreateProduct(viewModel.Newproduct);
                MessageBox.Show("Cập nhật thành công");
                viewModel.Products = new ObservableCollection<ProductModel>(productDAO.GetAllProducts());
            }
            else
            {
                MessageBox.Show("Ko thể cập nhật khi vì discount ko tồn tại");
            }
        }

    }
}
