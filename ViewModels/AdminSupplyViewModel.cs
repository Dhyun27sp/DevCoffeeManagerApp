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
    public class AdminSupplyViewModel : BaseViewModel
    {
        SupplyDAO supplyDAO = new SupplyDAO();

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
        public List<string> SupplyStatus { get; set; }
        public AdminSupplyViewModel()
        {
            Supplies = supplyDAO.GetAllSupplies();
            SupplyStatus = new List<string>();
            SupplyStatus.Add("Unused");
            SupplyStatus.Add("In-use");
            SupplyStatus.Add("Exhausted");
            SupplyStatus.Add("Out of date");
        }
    }
}
