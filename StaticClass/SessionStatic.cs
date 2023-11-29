using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.StaticClass
{
    static class SessionStatic
    {
        private static string _phone_number;
        private static string _task;
        private static int _password;
        private static List<TableModel> _tables = new List<TableModel>();
        private static ObservableCollection<DishModel> _ordered;
        public static string GetPhoneNumber { get { return _phone_number; } }
        public static string SetPhoneNumber { set { _phone_number = value; } }

        public static int GetPassWord { get { return _password; } }
        public static int SetPassWord { set { _password = value; } }

        public static string GetTask { get { return _task; } }
        public static string SetTask { set { _task = value; } }
        public static ObservableCollection<TableModel> GetTables { get { return new ObservableCollection<TableModel>(_tables); } }
        public static List<TableModel> SetTables { set { _tables = value; } }

        public static ObservableCollection<DishModel> GetOrdereds { get { return _ordered; } }
        public static ObservableCollection<DishModel> SetOrdereds { set { _ordered = value; } }
        public static ObservableCollection<DishModel> DeepCopyObservableCollection(ObservableCollection<DishModel> source)
        {
            return new ObservableCollection<DishModel>(
                source.Select(d => new DishModel(d._id, d.dish_name, d.Quantity, d.Saleprice, d.price))
            );
        }
        public static List<DishModel> Dishs { get; set; }

        public static CustomerModel Customer { get; set; }

        public static ObservableCollection<DishModel> copy = new ObservableCollection<DishModel>();

    }
}
