using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandProduct
{
    public class DeleteProductCommand : CommandBase
    {
        AdminProductViewModel adminProductViewModel;
        ProductDAO productDAO = new ProductDAO();

        public DeleteProductCommand(AdminProductViewModel adminProductViewModel)
        {
            this.adminProductViewModel = adminProductViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            if (parameter is ProductModel product)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    productDAO.DeleteProductByProductName(product.Product_name);
                    MessageBox.Show("Xóa thành công");
                    adminProductViewModel.Products = productDAO.GetAllProducts();
                }
            }
        }
    }
}
