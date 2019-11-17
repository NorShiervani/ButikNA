﻿using System;
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
                    MessageBox.Show(ex.Message, "Error adding order to list");
                }            
            }
        }
    }
}
