using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.ObjectModel;

namespace DevCoffeeManagerApp.Commands.CommandProduct
{
    public class SearchCommand : CommandBase
    {
        private AdminProductViewModel adminProductViewModel;
        public SearchCommand(AdminProductViewModel adminProductViewModel)
        {
            this.adminProductViewModel = adminProductViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            adminProductViewModel.Products = filter(adminProductViewModel.AllProducts, adminProductViewModel.Search, adminProductViewModel.Type);
        }

        // hàm fiter cho search, loại món, loại món đặt biệt
        public ObservableCollection<ProductModel> filter(ObservableCollection<ProductModel> AllProducts, string Search, string Type)
        {
            ObservableCollection<ProductModel> Products_Search = new ObservableCollection<ProductModel>();
            if (Search != "")
            {
                foreach (var item in AllProducts)
                {
                    bool contains = SearchMethod.Search_have_key(item.Product_name, Search);

                    if (contains)// trường hợp key có trong món ăn
                    {
                        Products_Search.Add(item);
                    }

                }
                if (Products_Search.Count == 0)//trương hợp key ko có trong món nào
                {
                    return null;
                }
                else
                {
                    return Products_Search = LoadProductWithType(Products_Search, Type);
                }
            }
            else // trương hợ search rỗng 
            {
                return AllProducts = LoadProductWithType(AllProducts, Type);
            }
        }

        public ObservableCollection<ProductModel> LoadProductWithType(ObservableCollection<ProductModel> AllProducts, string Type) // 
        {
            ObservableCollection<ProductModel> ProductsLocal = new ObservableCollection<ProductModel>();
            switch (Type)
            {
                case "In-use":
                    foreach (var product in AllProducts)
                    {
                        if (product.EXP_date >= DateTime.Now && product.Stock >= 100)
                        {
                            ProductsLocal.Add(product);
                        }
                    }
                    return ProductsLocal;
                case "Exhausted":
                    foreach (var product in AllProducts)
                    {
                        if (product.EXP_date >= DateTime.Now && product.Stock < 100)
                        {
                            ProductsLocal.Add(product);
                        }
                    }
                    return ProductsLocal;
                case "Out of date":
                    foreach (var product in AllProducts)
                    {
                        if (product.EXP_date <= DateTime.Now && product.Stock > 100)
                        {
                            ProductsLocal.Add(product);
                        }
                    }
                    return ProductsLocal;
                case "Un-update":
                    foreach (var product in AllProducts)
                    {
                        if (product.EXP_date == null)
                        {
                            ProductsLocal.Add(product);
                        }
                    }
                    return ProductsLocal;
                default:
                    return AllProducts;            
        }
    }
}
}
