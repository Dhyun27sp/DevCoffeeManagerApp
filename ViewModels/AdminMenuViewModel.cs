﻿using DevCoffeeManagerApp.Commands.CommandMenu;
using DevCoffeeManagerApp.Commands.CommandOrder;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminMenuViewModel : BaseViewModel
    {
        ProductDAO productDAO = new ProductDAO();
        MenuDAO menuDAO = new MenuDAO();

        // danh sách món ăn
        private ObservableCollection<DishModel> _dishes;
        public ObservableCollection<DishModel> Dishes
        {
            get
            {
                return _dishes;
            }
            set
            {
                _dishes = value;
                int index = 1;
                CombineList.Clear();
                foreach (var O in Dishes)
                {
                    CombineList.Add(new Tuple<DishModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(Dishes));

            }
        }
        private ObservableCollection<Tuple<DishModel, int>> _combineList = new ObservableCollection<Tuple<DishModel, int>>();
        //Kết thúc danh sách món

        //Danh sách món ăn kết hợp số thứ tự 
        public ObservableCollection<Tuple<DishModel, int>> CombineList
        {
            get
            {
                return _combineList;
            }
            set
            {
                _combineList = value;
                OnPropertyChanged(nameof(CombineList));
            }
        }
        // Kết thúc Danh sách món ăn kết hợp số thứ tự 
        /// <summary>
        ///
        /// </summary>
        ///
        // Danh sách nguyên liệu món
        private ObservableCollection<ProductModel> _ingredient;
        public ObservableCollection<ProductModel> Ingredient
        {
            get
            {
                return _ingredient;
            }
            set
            {
                _ingredient = value;
                int index = 1;
                CombineListIngredient.Clear();
                foreach (var O in Ingredient)
                {
                    CombineListIngredient.Add(new Tuple<ProductModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(Ingredient));

            }
        }
        // Kết thúc Danh sách nguyên liệu món

        // Danh sách nguyên liệu món + số thứ tự 
        private ObservableCollection<Tuple<ProductModel, int>> _combineListIngredient = new ObservableCollection<Tuple<ProductModel, int>>();
        public ObservableCollection<Tuple<ProductModel, int>> CombineListIngredient
        {
            get
            {
                return _combineListIngredient;
            }
            set
            {
                _combineListIngredient = value;
                OnPropertyChanged(nameof(CombineListIngredient));
            }
        }
        // Kết thúc Danh sách nguyên liệu món + số thứ tự 

        // Property cho textblock Pathimage lấy dường dẫn ảnh
        private string _pathimage = "";
        public string Pathimage
        {
            get { return _pathimage; }
            set { _pathimage = value; OnPropertyChanged(nameof(Pathimage)); }
        }
        // Kết thúc Property cho textblock Pathimage lấy dường dẫn ảnh

        // Property cho textbox Tên món ăn
        private string _dishName = "";
        public string DishName
        {
            get { return _dishName; }
            set { _dishName = value; OnPropertyChanged(nameof(DishName)); }
        }
        // Kết thúc Property cho textbox Tên món ăn

        public List<string> ProductList { get; set; }// Source của Combb product
        // Property cho SelectedItem của ProductList
        private string _product_name = "";
        public string Product_name
        {
            get { return _product_name; }
            set { _product_name = value; OnPropertyChanged(nameof(Product_name)); }
        }
        // Kết thúc Property cho SelectedItem của ProductList

        // Property textbox Số lượng của 
        private int _stock = 0;
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; OnPropertyChanged(nameof(Stock)); }
        }
        // kết thúc Property của textboxSố lượng

        // Property của textbox mô tả 
        private string _descriptiondish = "";
        public string DescriptionDish
        {
            get { return _descriptiondish; }
            set { _descriptiondish = value; OnPropertyChanged(nameof(DescriptionDish)); }
        }
        // kết thúc Property của textbox mô tả 

        public ObservableCollection<string> MenuList { get; set; }// Property của cbb chọn loại món để thêm món
        // SelectedItem của MenuList 
        private string _itemMenu = "";
        public string ItemMenu
        {
            get { return _itemMenu; }
            set { _itemMenu = value; OnPropertyChanged(nameof(ItemMenu)); }
        }
        // kết thúc SelectedItem của MenuList 

        // giá của Món ăn 
        private int _priceDish = 0;
        public int PriceDish
        {
            get { return _priceDish; }
            set { _priceDish = value; OnPropertyChanged(nameof(PriceDish)); }
        }
        // kết thúc giá của Món ăn 

        public ObservableCollection<string> types_dish { get; set; }// món theo loại

        // SelectedItem của types_dish 
        private string _type = "";
        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(nameof(Type)); }
        }
        // kết thúc SelectedItem của types_dish 

        public ObservableCollection<DishModel> AllSupplies { get; set; }
        public DateTime Date { get; set; }
        public ICommand ChooseimageCommand { get; set; }
        public ICommand AddIngredientCommand { get; set; }
        public ICommand AddDishCommand { get; set; }
        public ICommand SelectionChangeType { get; set; }

        public AdminMenuViewModel()
        {
            Ingredient = new ObservableCollection<ProductModel>();
            Dishes = new ObservableCollection<DishModel>(LoadAllDish());
            Date = DateTime.Now;

            ChooseimageCommand = new ClickMenuCommand(this, "chooseimage");
            AddIngredientCommand = new ClickMenuCommand(this, "addIngredient");
            AddDishCommand = new ClickMenuCommand(this, "adddish");
            SelectionChangeType = new FilterMenuCommand(this, "cbb");

            //nạp danh sách sản phẩm vào ProductList chỉ lấy tên
            ProductList = new List<string>();
            foreach (ProductModel p in productDAO.GetAllProducts())
            {
                ProductList.Add(p.Product_name);
            }
            //nạp danh loại món vào MenuList,types_dish chỉ lấy tên
            MenuList = new ObservableCollection<string>();
            types_dish = new ObservableCollection<string>();
            ObservableCollection<MenuModel> menuModels = menuDAO.ReadAll_Type_dish();
            foreach (var item in menuModels)
            {
                MenuList.Add(item.type_of_dish);
                types_dish.Add(item.type_of_dish);
            }
            types_dish.Add("All");
        }

        // hàm gán category cho món ăn
        public List<DishModel> LoadAllDish()
        {
            List<DishModel> DishsLocal = new List<DishModel>();
            foreach (var type in load_types_dish())
            {
                List<DishModel> DishsTemp = new List<DishModel>();

                DishsTemp = menuDAO.ReadOnetype(type).dish;
                foreach (var dishtemp in DishsTemp)
                {
                    dishtemp.category = type;
                }
                DishsLocal.AddRange(DishsTemp);
            }
            return DishsLocal;
        }
        // hàm lấy danh sách loại món chỉ lấy tên
        public List<string> load_types_dish()
        {
            ObservableCollection<MenuModel> menuModels = menuDAO.ReadAll_Type_dish();
            List<string> temp = new List<string>();
            foreach (var item in menuModels)
            {
                temp.Add(item.type_of_dish);
            }
            return temp;
        }

    }
}
