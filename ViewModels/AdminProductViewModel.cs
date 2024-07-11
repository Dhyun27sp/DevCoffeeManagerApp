using DevCoffeeManagerApp.Commands.CommandProduct;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminProductViewModel : BaseViewModel
    {
        ProductDAO productDAO = new ProductDAO();
        public ObservableCollection<ProductModel> AllProducts;

        private ObservableCollection<ProductModel> _products;
        public ObservableCollection<ProductModel> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                int index = 1;
                CombineList.Clear();
                if (value == null)
                    return;
                foreach (var O in Products)
                {
                    CombineList.Add(new Tuple<ProductModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(Products));

            }
        }
        private string _search = "";
        public string Search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
            }
        }

        private string _products_name;
        public string Products_name
        {
            get
            {
                return _products_name;
            }
            set
            {
                _products_name = value;
                OnPropertyChanged(nameof(Products_name));
            }
        }

        private string _products_unit;
        public string Products_unit
        {
            get
            {
                return _products_unit;
            }
            set
            {
                _products_unit = value;
                OnPropertyChanged(nameof(Products_unit));
            }
        }
        private ObservableCollection<Tuple<ProductModel, int>> _combineList = new ObservableCollection<Tuple<ProductModel, int>>();
        public ObservableCollection<Tuple<ProductModel, int>> CombineList
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
        public ProductModel Newproduct { get; set; }

        public DateTime Date { get; set; }
        public List<string> types_dish { get; }
        public string Type { get; set; }
        public List<string> UnitList { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand DeleteProductCommand { get; }
        public ICommand ChooseCommand {  get; set; }
        public ICommand UpdateCommand { get; set; }
        public AdminProductViewModel()
        {
            AllProducts = productDAO.GetAllProducts();
            Products = AllProducts;
            UnitList = AddUnit();
            Date = DateTime.Now;
            types_dish = AddType();
            SearchCommand = new SearchCommand(this);
            FilterCommand = new SearchCommand(this);
            AddCommand = new AddProductCommand(this);
            ChooseCommand = new UpdateProductCommand(this, "choose");
            UpdateCommand = new UpdateProductCommand(this, "update");
            ImportCommand = new ImportProductCommand(this);
            DeleteProductCommand = new DeleteProductCommand(this);
        }
        static List<String> AddUnit()
        {
            List<String> unitlist = new List<string>();
            unitlist.Add("g");
            unitlist.Add("ml");
            return unitlist;
        }

        static List<String> AddType()
        {
            List<String> supplyStatus = new List<string>();
            supplyStatus.Add("All Products");
            supplyStatus.Add("In-use");
            supplyStatus.Add("Exhausted");
            supplyStatus.Add("Un-update");
            supplyStatus.Add("Out of date");
            return supplyStatus;
        }
    }
}
