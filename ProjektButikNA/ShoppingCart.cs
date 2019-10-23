using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektButikNA
{
    class ShoppingCart
    {
        private List<Product> products;
        private Coupon coupon;
        private DateTime date;

        public ShoppingCart(List<Product> products, Coupon coupon, DateTime date)
        {
            this.products = products;
            this.coupon = coupon;
            this.date = date;
        }

        public ShoppingCart(List<Product> products, DateTime date)
        {
            this.products = products;
            this.date = date;
        }

        public List<Product> Products { get => products; set => products = value; }
        public Coupon Coupon { get => coupon; set => coupon = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
