using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
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
        static TableDAO tableDAO = new TableDAO();
        private static string _phone_number;
        private static string _task;
        private static int _password;
        private static List<TableModel> _tables = new List<TableModel>(tableDAO.ReadAll());
        public static string GetPhoneNumber { get { return _phone_number; } }
        public static string SetPhoneNumber { set { _phone_number = value; } }

        public static int GetPassWord { get { return _password; } }
        public static int SetPassWord { set { _password = value; } }

        public static string GetTask { get { return _task; } }
        public static string SetTask { set { _task = value; } }
        public static ObservableCollection<TableModel> GetTables { get { return new ObservableCollection<TableModel>(_tables); } }
        public static List<TableModel> SetTables { set { _tables = value; } }

        public static ObservableCollection<DishModel> Ordereds { get; set; }

        public static List<DishModel> Dishs { get; set; }
    }
}
