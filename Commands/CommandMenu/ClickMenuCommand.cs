using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandMenu
{
    public class ClickMenuCommand:CommandBase
    {
        private AdminMenuViewModel viewmodel;
        private string action;
        MenuDAO menuDAO = new MenuDAO();
        ProductDAO productDAO = new ProductDAO();
        public ClickMenuCommand(AdminMenuViewModel viewmodel, string action) {
            this.viewmodel = viewmodel;
            this.action = action;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            switch (action)
            {
                case "chooseimage":
                    chooseimage(parameter);
                    return;
                case "addIngredient":
                    addIngredient(parameter);
                    return;
                case "adddish":
                    add_dish(parameter);
                    return;
                case "addmenu":
                    add_menu(parameter);
                    return;
                case "deleteMenu":
                    delete_menu(parameter);
                    return;
                case "deleteDish":
                    delete_dish(parameter);
                    return;
            }
        }

        // hàm chọn ảnh 
        private void chooseimage(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? success = fileDialog.ShowDialog();
            if(success == true)
            {
                string path = fileDialog.FileName;
                string fileName = fileDialog.SafeFileName;
                viewmodel.Pathimage = path;
            }
        }
        // hàm thêm nguyên liệu
        private void addIngredient(object parameter)
        {
            ProductModel product = new ProductModel();
            product.Product_name = viewmodel.Product_name;
            product.Stock = viewmodel.Stock;
            product.Unit = productDAO.GetProductbyName(viewmodel.Product_name).Unit;
            viewmodel.Ingredient.Add(product);
            viewmodel.Ingredient = viewmodel.Ingredient;
        }

        // hàm thêm món 
        private void add_dish(object parameter)
        {
            DishModel dish = new DishModel();
            if(checkInputDish())
            {
                dish._id = ObjectId.GenerateNewId();
                dish.dish_name = viewmodel.DishName;
                dish.price = viewmodel.PriceDish;
                dish.image = encode();
                dish.date_add = DateTime.Now.ToString("yyyy/MM/dd");
                dish.ingredient = new List<ProductModel>(viewmodel.Ingredient);

                menuDAO.AddDishInMenu(viewmodel.ItemMenu, dish);

                MessageBox.Show("Thêm món thành công");

                // sau khi thêm món thành công kiểm tra combobox xem đang ở trạng thái All hay loại món bất kỳ 
                if (viewmodel.Type == "All")
                {
                    viewmodel.Dishes = new ObservableCollection<DishModel>(viewmodel.LoadAllDish());
                }
                else
                {
                    LoadTypeDish();
                }

            // thêm món xong làm mới Property 
            viewmodel.Ingredient = new ObservableCollection<ProductModel>();
            viewmodel.DishName = "";
            viewmodel.Pathimage = "";
            viewmodel.Stock = 0;
            viewmodel.PriceDish = 0;
        }

        // mã hóa ảnh từ byte sang string 
        private string encode() {
            // Read image data from an image file
            byte[] imageData = File.ReadAllBytes(viewmodel.Pathimage); // Đọc dữ liệu hình ảnh từ tệp
            string imageString;
            return imageString = Convert.ToBase64String(imageData);
        }

        // làm gán category cho một món ăn duy nhất 
        private void LoadTypeDish()
        {
            ObservableCollection<DishModel> dishs = new ObservableCollection<DishModel>();
            foreach (DishModel d in menuDAO.ReadOnetype(viewmodel.Type).dish)
            {
                d.category = viewmodel.Type;
                dishs.Add(d);
            }
            viewmodel.Dishes = dishs;
        }

        //thêm loại món
        private void add_menu(object parameter)
        {
            MenuModel menu = new MenuModel();
            menu.type_of_dish = viewmodel.MenuName;
            menu.detail = viewmodel.DescriptionMenu;
            menu.dish = new List<DishModel>();
            if (menuDAO.findMenubyname(viewmodel.MenuName) != null)
            {
                MessageBox.Show("Loại món đã tồn tại");
            }
            else
            {
                menuDAO.CreateMenu(menu);
                viewmodel.LoadMenuInCombobox();
                viewmodel.Menus = menuDAO.ReadAll_Type_dish();
                MessageBox.Show("Thêm món thành công");
            }    

            // thêm món xong làm mới Property 
            viewmodel.MenuName = "";
            viewmodel.DescriptionMenu = "";
        }

        // xóa loại món
        private void delete_menu(object parameter) 
        {
            if (parameter is MenuModel menu)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    menuDAO.DeleteMenuByMenuName(menu.type_of_dish);
                    MessageBox.Show("Xóa thành công");
                    viewmodel.Menus = menuDAO.ReadAll_Type_dish();
                    viewmodel.LoadMenuInCombobox();
                }
            }
        }

        //kiểm tra null
        public bool checkInputDish()
        {
            if (string.IsNullOrWhiteSpace(viewmodel.DishName) || string.IsNullOrWhiteSpace(viewmodel.Pathimage) || string.IsNullOrWhiteSpace(viewmodel.ItemMenu) || viewmodel.Ingredient.Count == 0 || viewmodel.PriceDish == 0)
            {
                MessageBox.Show("Dữ liệu còn thiếu hoặc không hợp lệ, hãy nhập lại!");
                return false;
            }    
            return true;
        }

        //xóa món ăn
        public void delete_dish(object parameter)
        {
            if (parameter is DishModel dish)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    menuDAO.DeleteDishByID(dish._id, dish.category);
                    MessageBox.Show("Xóa thành công");
                    // sau khi thêm món thành công kiểm tra combobox xem đang ở trạng thái All hay loại món bất kỳ 
                    if (viewmodel.Type == "All")
                    {
                        viewmodel.Dishes = new ObservableCollection<DishModel>(viewmodel.LoadAllDish());
                    }
                    else
                    {
                        LoadTypeDish();
                    }
                }
            }
        }
    }
}
