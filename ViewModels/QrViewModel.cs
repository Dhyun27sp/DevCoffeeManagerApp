using DevCoffeeManagerApp.Commands.CommandQr;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Xamarin.Forms;

namespace DevCoffeeManagerApp.ViewModels
{
    public class QrViewModel : BaseViewModel
    {        

        private string _customerName = "";
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }

        private DateTime _currentDate;
        public DateTime CurrentDate
        {
            get
            {
                return _currentDate;
            }

            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        private string _receiptCode;
        public string ReceiptCode
        {
            get { return _receiptCode; }
            set
            {
                if (_receiptCode != value)
                {
                    _receiptCode = value;
                    OnPropertyChanged(nameof(ReceiptCode));
                }
            }
        }

        private string _staffPhoneNumber;
        public string StaffPhoneNumber
        {
            get
            {
                return _staffPhoneNumber;
            }

            set
            {
                _staffPhoneNumber = value;
                OnPropertyChanged(nameof(StaffPhoneNumber));
            }
        }

        private string _table = "";
        public string Table
        {
            get
            {
                return _table;
            }

            set
            {
                _table = value;
                OnPropertyChanged(nameof(Table));
            }
        }

        private string _usedPoint = "0";
        public string UsedPoint
        {
            get { return _usedPoint; }
            set
            {
                if (_usedPoint != value)
                {
                    _usedPoint = value;
                    OnPropertyChanged(nameof(UsedPoint));
                }
            }
        }

        private int _totalAmount = 0;
        public int TotalAmount
        {
            get
            {
                return _totalAmount;
            }

            set
            {
                _totalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }
        private BitmapImage _img = new BitmapImage();
        public BitmapImage Img
        {
            get
            {
                return _img;
            }

            set
            {
                _img = value;
                OnPropertyChanged(nameof(Img));
            }
        }
        public ICommand CloseCommand { get; }

        public QrViewModel()
        {
            CloseCommand = new CloseCommand();            
            if (SessionStatic.Customer != null)
            {
                CustomerName = SessionStatic.GetReceipt.customer.name;
            }
            if (SessionStatic.GetReceipt != null) {
                ReceiptCode = SessionStatic.GetReceipt.receipt_code;
                UsedPoint = SessionStatic.GetReceipt.used_point.ToString();
                CurrentDate = SessionStatic.GetReceipt.time;
                StaffPhoneNumber = SessionStatic.GetReceipt.staff_phone;
                TotalAmount = SessionStatic.GetReceipt.total_amount;                
            }

            BitmapImage image = new BitmapImage();
            MemoryStream ms = new MemoryStream();
            SessionStatic.Img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            Img = image;


            if (SessionStatic.GetTables != null)
            {
                string tables = "";
                foreach (var i in SessionStatic.GetTables)
                {
                    if (tables == "")
                    {
                        tables = i.No_.ToString();
                    }
                    else
                        tables = tables + ", " + i.No_.ToString();
                }
                Table = tables;
            }
        }
    }
}
