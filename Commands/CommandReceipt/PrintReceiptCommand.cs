using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DevCoffeeManagerApp.Commands.CommandReceipt
{
    public class PrintReceiptCommand : CommandBase
    {
        public PrintReceiptCommand()
        {
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            try
            {
                if (parameter is Grid printGrid)
                {
                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == true)
                    {
                        RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)printGrid.ActualWidth, (int)printGrid.ActualHeight, 96, 96, System.Windows.Media.PixelFormats.Default);
                        renderTarget.Render(printGrid);

                        // In hình ảnh
                        printDialog.PrintVisual(printGrid, "Hóa đơn");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi in hóa đơn: " + ex.Message);
            }
        }
    }
}
