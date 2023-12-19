using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.Views;
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
        private static string _staff_name;
        private static string _task;
        private static int _password;
        private static ObservableCollection<TableModel> _tables;
        private static ObservableCollection<DishModel> _ordered;
        private static ReceiptModel _receipt;

        public static string GetPhoneNumber { get { return _phone_number; } }
        public static string SetPhoneNumber { set { _phone_number = value; } }
        public static string GetStaffName { get { return _staff_name; } }
        public static string SetStaffName { set { _staff_name = value; } }
        public static int GetPassWord { get { return _password; } }
        public static int SetPassWord { set { _password = value; } }

        public static string GetTask { get { return _task; } }
        public static string SetTask { set { _task = value; } }
        public static ObservableCollection<TableModel> GetTables { get { return _tables; } }
        public static ObservableCollection<TableModel> SetTables { set { _tables = value; } }

        public static ObservableCollection<DishModel> GetOrdereds { get { return _ordered; } }
        public static ObservableCollection<DishModel> SetOrdereds { set { _ordered = value; } }

        public static ReceiptModel GetReceipt { get { return _receipt; } }
        public static ReceiptModel SetReceipt { set { _receipt = value; } }

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
