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
    public class AdminMenuViewModel : BaseViewModel
    {
        ProductDAO productDAO = new ProductDAO();
        MenuDAO menuDAO = new MenuDAO();

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
        public ObservableCollection<DishModel> AllSupplies { get; set; }
        public DateTime Date { get; set; }

        public AdminMenuViewModel()
        {
            Dishes = new ObservableCollection<DishModel>(LoadAllDish());
            Date = DateTime.Now;

        }
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
