using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektButikNA
{
    public class ProductInCart : Product 
    {
        private int amount;
        
        public ProductInCart(string pid, string name, double price, int amount) : base(pid, name, price)
        {
            this.amount = amount;
        }

        public ProductInCart()
        {

        }

        public int Amount { get => amount; set => amount = value < 0 ? 0 : value; }
    }
}
