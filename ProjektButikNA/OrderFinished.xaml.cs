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
    /// Interaction logic for OrderFinished.xaml
    /// </summary>
    public partial class OrderFinished : Page
    {

        ShoppingCart shoppingCart;
        Frame parent;

        public OrderFinished(ShoppingCart shoppingcart, Frame parent)
        {
            InitializeComponent();
            this.shoppingCart = shoppingcart;
            this.parent = parent;
            DataContext = shoppingcart;
            DisplayOrderDetails();
        }

        private void DisplayOrderDetails()
        {
            txtBId.Text = shoppingCart.ShoppingCartId;
            txtBOrderDate.Text = shoppingCart.DateRegistered.ToString("yyyyMMdd"); ;
            if (shoppingCart.Coupon != null)
            {
                txtBCouponDetails.Text = shoppingCart.Coupon.Code + " (" + (shoppingCart.Coupon.Discount * 100) + "%)";
            }
            txtBTotalCost.Text = shoppingCart.GetTotalCost(false).ToString();
            txtBTotalCostCoupon.Text = shoppingCart.GetTotalCost(true).ToString();
        }

        private void NewOrder(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow(parent);
            parent.Content = orderWindow;
        }

        private void ViewOrderHistory(object sender, RoutedEventArgs e)
        {
            OrderHistory orderHistory = new OrderHistory();
            parent.Content = orderHistory;
        }
    }
}