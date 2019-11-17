using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Page
    {

        ShoppingCart shoppingCart = new ShoppingCart();
        Frame parent;

        public OrderWindow(Frame parent)
        {
            InitializeComponent();
            LoadProducts();
            this.parent = parent;
            DataContext = shoppingCart;
        }

        void LoadProducts()
        {
            foreach (Product product in Product.GetProducts())
            {
                dgvProducts.Items.Add(product);
            }
        }

        private void FilterProductList(object sender, TextChangedEventArgs e)
        {
            dgvProducts.Items.Clear();
            List<Product> filteredProducts = Product.GetProductsByFilter(txtbFilterProducts.Text);

            foreach (Product product in filteredProducts)
            {
                try
                {
                    dgvProducts.Items.Add(product);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Failed filtering and adding product to list");
                }
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                Product selectedProduct = (Product)dgvProducts.SelectedItem;
                shoppingCart.Products.Add(new ProductInCart(selectedProduct.PID, selectedProduct.Name, selectedProduct.Price, 1));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add product to your cart. Make sure you have selected a product from the list before clicking 'Add Product'.\n\nError: " + ex.Message, 
                    "Failed to add product");
            }
            
        }

        private void ValidateCouponCode(object sender, TextChangedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(txtCoupon.Text))) {
                Coupon coupon = Coupon.GetCoupon(txtCoupon.Text);

                if (coupon != null)
                {
                    txtBCoupon.Foreground = Brushes.Purple;
                    txtBCoupon.Text = "Coupon (" + (coupon.Discount * 100) + "%)";
                    shoppingCart.Coupon = coupon;
                }
                else
                {
                    txtBCoupon.Foreground = Brushes.Red;
                    txtBCoupon.Text = "Coupon is invalid";
                    shoppingCart.Coupon = null;
                }
            }
            else
            {
                txtBCoupon.Foreground = Brushes.Purple;
                txtBCoupon.Text = "Coupon";
                shoppingCart.Coupon = null;
            }
        }

        private void OrderConfirm(object sender, RoutedEventArgs e)
        {
            shoppingCart.DateRegistered = DateTime.Now;
            shoppingCart.ShoppingCartId = Guid.NewGuid().ToString();
            OrderFinished orderFinished = new OrderFinished(shoppingCart);
            shoppingCart.AddToFile();
            parent.Content = orderFinished;
        }
    }
}
