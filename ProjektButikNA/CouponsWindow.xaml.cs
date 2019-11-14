using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

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
            LoadCoupons();
        }

        void LoadCoupons()
        {
            foreach (Coupon coupon in Coupon.GetCoupons())
            {
                dgvCoupons.Items.Add(coupon);
            }
        }
    }
}
