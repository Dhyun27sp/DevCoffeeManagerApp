using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.ObjectModel;

namespace DevCoffeeManagerApp.Commands.CommandSupply
{
    public class SearchCommand : CommandBase
    {
        private AdminSupplyViewModel adminSupplyViewModel;
        public SearchCommand(AdminSupplyViewModel adminSupplyViewModel)
        {
            this.adminSupplyViewModel = adminSupplyViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            adminSupplyViewModel.Supplies = filter(adminSupplyViewModel.AllSupplies, adminSupplyViewModel.Search, adminSupplyViewModel.Month_Type, adminSupplyViewModel.Status_Type);
        }

        // hàm fiter cho search, loại món, loại món đặt biệt
        public ObservableCollection<SupplyModel> filter(ObservableCollection<SupplyModel> AllSupplys, string Search, string Month, string Status)
        {
            ObservableCollection<SupplyModel> Supplys_Search = new ObservableCollection<SupplyModel>();
            if (Search != "")
            {
                foreach (var item in AllSupplys)
                {
                    bool contains = SearchMethod.Search_have_key(item.Product_name, Search);

                    if (contains)// trường hợp key có trong món ăn
                    {
                        Supplys_Search.Add(item);
                    }

                }
                if (Supplys_Search.Count == 0)//trương hợp key ko có trong món nào
                {
                    return null;
                }
                else
                {
                    Supplys_Search = LoadSupplyByMonth(Supplys_Search, Month);
                    return LoadSupplyByStatus(Supplys_Search, Status);
                }
            }
            else // trương hợ search rỗng 
            {
                AllSupplys = LoadSupplyByMonth(AllSupplys, Month);
                return LoadSupplyByStatus(AllSupplys, Status);
            }
        }

        public ObservableCollection<SupplyModel> LoadSupplyByMonth(ObservableCollection<SupplyModel> AllSupplys, string Type) // 
        {
            if (Type == null || !Type.Contains(" "))
                return AllSupplys;
            ObservableCollection<SupplyModel> SupplysLocal = new ObservableCollection<SupplyModel>();
            //tạo filter
            string datetime = Type.Split(' ')[1];
            int month = int.Parse(datetime.Split('/')[0]);
            int year = int.Parse(datetime.Split('/')[1]);
            DateTime monthfilter = new DateTime(year, month, 1);
            foreach (var supply in AllSupplys)
            {
                DateTime date = supply.Date;
                if (date != null)
                {
                    if (date.Month == monthfilter.Month && date.Year == monthfilter.Year)
                    {
                        SupplysLocal.Add(supply);
                    }
                }
            }
            return SupplysLocal;


            /*switch (Type)
            {
                case "In-use":
                    foreach (var product in AllSupplys)
                    {
                        if (product.Status = )
                        {
                            SupplysLocal.Add(product);
                        }
                    }
                    return SupplysLocal;
                case "Exhausted":
                    foreach (var product in AllSupplys)
                    {
                        if (product.EXP_date >= DateTime.Now && product.Stock < 100)
                        {
                            SupplysLocal.Add(product);
                        }
                    }
                    return SupplysLocal;
                case "Out of date":
                    foreach (var product in AllSupplys)
                    {
                        if (product.EXP_date <= DateTime.Now && product.Stock > 100)
                        {
                            SupplysLocal.Add(product);
                        }
                    }
                    return SupplysLocal;
                case "Un-update":
                    foreach (var product in AllSupplys)
                    {
                        if (product.EXP_date == null)
                        {
                            SupplysLocal.Add(product);
                        }
                    }
                    return SupplysLocal;
                default:
                    return AllSupplys;
            }*/
        }

        public ObservableCollection<SupplyModel> LoadSupplyByStatus(ObservableCollection<SupplyModel> AllSupplys, string Type) // 
        {
            if (Type == null || Type == "All")
                return AllSupplys;
            ObservableCollection<SupplyModel> SupplysLocal = new ObservableCollection<SupplyModel>();
            foreach (var product in AllSupplys)
            {
                if (product.Status == Type)
                {
                    SupplysLocal.Add(product);
                }
            }
            return SupplysLocal;                
        }
    }
}
