using DevCoffeeManagerApp.DAOs;
using System;

namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminDashboardViewModel:BaseViewModel
    {
        MenuDAO menuDAO = new MenuDAO();
        ProductDAO productDAO = new ProductDAO();
        StaffDAO staffDAO = new StaffDAO();
        ReceiptDAO receiptDAO = new ReceiptDAO();
        public DateTime Date { get; set; }

        private int _totalRevenue = 0;
        public int TotalRevenue
        {
            get { return _totalRevenue; }
            set
            {
                if (_totalRevenue != value)
                {
                    _totalRevenue = value;
                    OnPropertyChanged(nameof(TotalRevenue));
                }
            }
        }
        private int _totalProduct = 0;
        public int TotalProduct
        {
            get { return _totalProduct; }
            set
            {
                if (_totalProduct != value)
                {
                    _totalProduct = value;
                    OnPropertyChanged(nameof(TotalProduct));
                }
            }
        }
        private int _totalStaff = 0;
        public int TotalStaff
        {
            get { return _totalStaff; }
            set
            {
                if (_totalStaff != value)
                {
                    _totalStaff = value;
                    OnPropertyChanged(nameof(TotalStaff));
                }
            }
        }
        private int _totalDish = 0;
        public int TotalDish
        {
            get { return _totalDish; }
            set
            {
                if (_totalDish != value)
                {
                    _totalDish = value;
                    OnPropertyChanged(nameof(TotalDish));
                }
            }
        }
        public AdminDashboardViewModel() 
        {
            Date = DateTime.Now;
            TotalRevenue = receiptDAO.GetTotalRevenue();
            TotalProduct = productDAO.CountProducts();
            TotalDish = menuDAO.CountDishesInMenu();
            TotalStaff = staffDAO.CountStaffs();
        }
    }
}
