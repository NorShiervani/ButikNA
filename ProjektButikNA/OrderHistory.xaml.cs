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
    /// Interaction logic for OrderHistory.xaml
    /// </summary>
    public partial class OrderHistory : Page
    {
        ShoppingCart shoppingCart;

        public OrderHistory()
        {
            InitializeComponent();
            LoadShoppingCarts();
        }

        void LoadShoppingCarts()
        {
            foreach (ShoppingCart shoppingCarts in ShoppingCart.GetShoppingCarts())
            {
                try
                {
                    dgvOrderHistory.Items.Add(shoppingCarts);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Failed loading and adding order to list");
                }            
            }
        }

        private void SelectOrder(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                shoppingCart = (ShoppingCart) dgvOrderHistory.SelectedItem;
                DisplayOrderDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed selecting order");
            }
        }

        private void DisplayOrderDetails()
        {
            DataContext = shoppingCart;
            txtBId.Text = shoppingCart.ShoppingCartId;
            txtBOrderDate.Text = shoppingCart.DateRegistered.ToString("yyyyMMdd"); ;
            if (shoppingCart.Coupon != null)
            {
                txtBCouponDetails.Text = shoppingCart.Coupon.Code + " (" + (shoppingCart.Coupon.Discount * 100) + "%)";
            }
            else
            {
                txtBCouponDetails.Text = "None";
            }
            txtBTotalCost.Text = shoppingCart.GetTotalCost(false).ToString();
            txtBTotalCostCoupon.Text = shoppingCart.GetTotalCost(true).ToString();
        }

        private void DateToChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpDateTo != null && dpDateFrom != null)
            {
                if (dpDateTo.SelectedDate < dpDateFrom.SelectedDate)
                {
                    dpDateTo.SelectedDate = dpDateFrom.SelectedDate;
                }
            }

            FilterOrderByDate();
        }

        private void DateFromChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpDateTo != null && dpDateFrom != null)
            {
                if (dpDateTo.SelectedDate < dpDateFrom.SelectedDate)
                {
                    dpDateFrom.SelectedDate = dpDateTo.SelectedDate;
                }
            }

            FilterOrderByDate();
        }

        private void FilterOrderByDate()
        {
            if (dgvOrderHistory != null)
            {
                dgvOrderHistory.Items.Clear();
                List<ShoppingCart> filteredShoppingCart = ShoppingCart.FilterShoppingList(dpDateFrom.SelectedDate ?? DateTime.Now, dpDateTo.SelectedDate ?? DateTime.Now);

                if (filteredShoppingCart != null)
                {
                    foreach (ShoppingCart shoppingCarts in filteredShoppingCart)
                    {
                        try
                        {
                            dgvOrderHistory.Items.Add(shoppingCarts);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Failed filtering order list");
                        }
                    }
                }
            }  
        }
    }
}
