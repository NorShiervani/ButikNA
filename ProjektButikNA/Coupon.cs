using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektButikNA
{
    class Coupon
    {
        private string code;
        private double discount;

        public Coupon(string code, double discount)
        {
            this.code = code;
            this.discount = discount;
        }

        public string Code { get => code; set => code = value; }
        public double Discount { get => discount; set => discount = value; }
    }
}
