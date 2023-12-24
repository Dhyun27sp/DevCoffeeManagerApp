using DevCoffeeManagerApp.Commands.CommandSupply;
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
    public class AdminSupplyViewModel : BaseViewModel
    {

        SupplyDAO supplyDAO = new SupplyDAO();

        ProductDAO productDAO = new ProductDAO();

        private ObservableCollection<SupplyModel> _supplies;
        public ObservableCollection<SupplyModel> Supplies
        {
            get
            {
                return _supplies; ;
            }
            set
            {
                _supplies = value;
                int index = 1;
                CombineList.Clear();
                foreach (var O in Supplies)
                {
                    CombineList.Add(new Tuple<SupplyModel, int>(O, index));
                    index++;
                }
                OnPropertyChanged(nameof(Supplies));

            }
        }
        private ObservableCollection<Tuple<SupplyModel, int>> _combineList = new ObservableCollection<Tuple<SupplyModel, int>>();
        public ObservableCollection<Tuple<SupplyModel, int>> CombineList
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
        public ObservableCollection<SupplyModel> AllSupplies { get; set; }
        public List<string> SupplyStatus { get; set; }
        public List<string> UnitList { get; set; }
        public List<string> ProductList { get; set; }
        public List<string> MonthFilter { get; set; }
        public SupplyModel newsupply { get; set; }
        public ICommand UpDateCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public DateTime Date { get; set; }
        public AdminSupplyViewModel()
        {
            AllSupplies = supplyDAO.GetAllSupplies();
            Supplies = AllSupplies;
            newsupply = new SupplyModel();
            SupplyStatus = AddStatus();
            UnitList = AddUnit();
            ProductList = AddProductName();
            MonthFilter = GenerateMonthList();
            Date = DateTime.Now;
            UpDateCommand = new UpdateStatusCommand(this);
            AddCommand = new AddSupplyCommand(this);

        }

        static List<string> GenerateMonthList()
        {
            List<string> monthList = new List<string>();
            DateTime currentMonth = DateTime.Now;
            int numMonths = 12; // Số tháng cần tạo

            for (int i = 0; i < numMonths; i++)
            {
                string formattedMonth = "Tháng " + currentMonth.ToString("MM/yyyy");
                monthList.Add(formattedMonth);
                currentMonth = currentMonth.AddMonths(-1);
            }

            return monthList;
        }
        static List<String> AddStatus()
        {
            List<String> supplyStatus = new List<string>();
            supplyStatus.Add("Unused");
            supplyStatus.Add("In-use");
            supplyStatus.Add("Exhausted");
            supplyStatus.Add("Out of date");
            return supplyStatus;
        }
        static List<String> AddUnit()
        {
            List<String> unitlist = new List<string>();
            unitlist.Add("Kg");
            unitlist.Add("L");
            return unitlist;
        }

        private List<String> AddProductName()
        {
            List<String> products = new List<string>();
            products = productDAO.GetAllProductName();
            return products;
        }


        public List<SupplyModel> LoadSuppliesByStatus(List<SupplyModel> AllSupply, string Type) // 
        {
            List<SupplyModel> supplyList = new List<SupplyModel>();
            if (Type != null && Type != "All Status")
            {
                foreach (var dish in AllSupply)
                {
                    if (dish.Status == Type)
                    {
                        supplyList.Add(dish);
                    }
                }
                return supplyList;
            }
            else { return AllSupply; }
        }
    }
}
