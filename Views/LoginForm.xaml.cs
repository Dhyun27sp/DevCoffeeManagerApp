using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DevCoffeeManagerApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void CloseButton_Click(object sender, MouseButtonEventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?",
                                                      "Xác nhận thoát",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            // Kiểm tra kết quả của hộp thoại xác nhận
            if (result == MessageBoxResult.Yes)
            {
                // Thoát ứng dụng
                Application.Current.Shutdown();
            }
        }

        private void ContentSite_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
