using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminProductViewModel : BaseViewModel
    {
        ProductDAO productDAO = new ProductDAO();

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
                foreach (var O in Products)
                {
                    CombineList.Add(new Tuple<ProductModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(Products));

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
        public ProductModel newproduct { get; set; }
        public DateTime Date { get; set; }
        public List<string> UnitList { get; set; }

        public AdminProductViewModel() 
        {
            Products = productDAO.GetAllProducts();
            newproduct = new ProductModel();
            UnitList = AddUnit();
            Date = DateTime.Now;
        }
        static List<String> AddUnit()
        {
            List<String> unitlist = new List<string>();
            unitlist.Add("g");
            unitlist.Add("mL");
            return unitlist;
        }
    }
}
