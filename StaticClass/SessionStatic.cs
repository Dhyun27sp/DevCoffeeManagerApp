using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.Shipping;
using DevCoffeeManagerApp.Views;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
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
                source.Select(d => new DishModel(d._id, d.dish_name, d.Quantity, d.Saleprice, d.price, d.ingredient, d.Sugar, d.Ice))
            );
        }
        public static List<DishModel> Dishs { get; set; }

        public static CustomerModel Customer { get; set; }

        public static ObservableCollection<DishModel> copy = new ObservableCollection<DishModel>();
        public static Image Img { get; set; }

        private static string partnerCode = "MOMO"; // thay bang key cua minh
        private static string accessKey = "F8BBA842ECF85";   // thay bang key cua minh
        private static string serectkey = "K951B6PE1waDMi640xX08PD3vg6EkVlz"; // thay bang key cua minh

        public static string PartnerCode { get { return partnerCode; } }
        public static string AccessKey { get { return accessKey; } }
        public static string SerectKey { get { return serectkey; } }

        public static Stop CusStop = new Stop();

        public static Stop ShopStop = new Stop
        {
            coordinates = new Coordinates { latitude = "10.8284142", longitude = "106.8130875" },
            address = "9/4 Đường số 2, KP Phước Hiệp, Trường Thạnh, Q9"
        };
        public static ObservableCollection<Stop> stops = new ObservableCollection<Stop>() {ShopStop, CusStop };
        
    }
}
