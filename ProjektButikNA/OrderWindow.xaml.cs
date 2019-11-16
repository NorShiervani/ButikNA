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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Page
    {
        public OrderWindow()
        {
            InitializeComponent();
            LoadProducts();
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

            foreach (Product product in Product.GetProductsByFilter(txtbFilterProducts.Text))
            {
                dgvProducts.Items.Add(product);
            }
        }
    }
}
