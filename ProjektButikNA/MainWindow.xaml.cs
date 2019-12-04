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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektButikNA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadExampleData(20);
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                         ? WindowState.Normal
                         : WindowState.Maximized;
        }

        private void ShowCoupons(object sender, RoutedEventArgs e)
        {
            PageContainer.Content = new CouponsWindow();
        }

        private void ShowNewOrder(object sender, RoutedEventArgs e)
        {
            PageContainer.Content = new OrderWindow(PageContainer);
        }

        private void ShowOrderHistory(object sender, RoutedEventArgs e)
        {
            PageContainer.Content = new OrderHistory();
        }

        private void ShowProducts(object sender, RoutedEventArgs e)
        {
            PageContainer.Content = new ManageProducts();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }

        private void LoadExampleData(int amount)
        {
            Random random = new Random();

            if (Product.GetProducts().Count() < 1)
            {
                string[] productNames = new string[] { "HP", "Computer", "Clean", "Game", "Screen", "Controller", "Camera", "Console", "Mouse", "Keyboard", "Device" };

                for (int i = 0; i < amount; i++)
                {
                    string PID = Guid.NewGuid().ToString().Substring(1, 6).ToUpper();
                    string productName = productNames[random.Next(productNames.Count())] + " " + productNames[random.Next(productNames.Count())];
                    double price = random.Next(10, 10000);
                    Product product = new Product(PID, productName, price);

                    product.AddToFile();
                }

            }

            if (Coupon.GetCoupons().Count() < 1)
            {
                for (int i = 0; i < amount; i++)
                {
                    string code = Guid.NewGuid().ToString().Substring(1, 4).ToUpper();
                    double discount = Math.Round(random.NextDouble(), 2);
                    Coupon coupon = new Coupon(code, discount);

                    coupon.AddToFile();
                }
            }
        }
    }
}
