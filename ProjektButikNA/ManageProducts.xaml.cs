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
    /// Interaction logic for ManageProducts.xaml
    /// </summary>
    public partial class ManageProducts : Page
    {
        public ManageProducts()
        {
            InitializeComponent();
            GenerateNewPID();
            LoadProducts();
        }

        void LoadProducts()
        {
            dgvProducts.Items.Clear();

            foreach (Product product in Product.GetProducts())
            {
                dgvProducts.Items.Add(product);
            }
        }

        private void CreateProduct(object sender, RoutedEventArgs e)
        {
            if (Product.productExist(txtBPID.Text))
            {
                MessageBox.Show("Product with " + txtBPID.Text + " already exists.");
                GenerateNewPID();
            }
            else if (txtBPID.Text == "" || txtBName.Text == "" || txtBPrice.Text == "")
            {
                MessageBox.Show("Fill in all the details.");
            }
            else
            {
                try
                {
                    Product product = new Product(txtBPID.Text, txtBName.Text, double.Parse(txtBPrice.Text));
                    product.AddToFile();
                    GenerateNewPID();
                    LoadProducts();
                    MessageBox.Show("Added product.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error creating product");
                }
            }
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            if (!Product.productExist(txtBPIDdelete.Text))
            {
                MessageBox.Show("Product with id '" + txtBPIDdelete.Text + "' doesn't exists.");
                GenerateNewPID();
            }
            else if (txtBPIDdelete.Text == "")
            {
                MessageBox.Show("Fill in all the details.");
            }
            else
            {
                Product.RemoveFromFile(txtBPIDdelete.Text);
                LoadProducts();
                MessageBox.Show("Deleted product with id '" + txtBPIDdelete.Text + "'.");
            }
        }

        private void GenerateNewPID()
        {
            txtBPID.Text = Guid.NewGuid().ToString().Substring(1, 6).ToUpper();
        }

        private void FilterProductList(object sender, TextChangedEventArgs e)
        {
            dgvProducts.Items.Clear();
            List<Product> filteredProducts = Product.GetProductsByFilter(txtbFilterProducts.Text);

            foreach (Product product in filteredProducts)
            {
                dgvProducts.Items.Add(product);
            }
        }
    }
}
