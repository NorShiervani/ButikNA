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
    /// Interaction logic for CouponsWindow.xaml
    /// </summary>
    public partial class CouponsWindow : Page
    {
        public CouponsWindow()
        {
            InitializeComponent();
            dgvCoupons.Items.Add(new Coupon("WEJIOQWEWE", 0.22));
        }
    }
}
