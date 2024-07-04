using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DevCoffeeManagerApp.Views.UserControlStaff
{
    /// <summary>
    /// Interaction logic for CheckPage.xaml
    /// </summary>
    public partial class CheckPage : UserControl
    {
        public CheckPage()
        {
            InitializeComponent();
            this.Loaded += UserControlB_Loaded;
            this.Unloaded += UserControlB_Unloaded;
        }

        private async void UserControlB_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize WebView2 if not already initialized
            if (webview.CoreWebView2 == null)
            {
                await webview.EnsureCoreWebView2Async();
            }

            // Navigate to default page
            webview.CoreWebView2.Navigate("https://www.google.com/maps/@41.2950114,-91.9826847,14z");
        }

        private void UserControlB_Unloaded(object sender, RoutedEventArgs e)
        {
            // Optionally, you can pause or clean up WebView2 here
            // For example, you might clear cache or cookies if needed
            webview.CoreWebView2.Stop();
        }
        

    }
}
