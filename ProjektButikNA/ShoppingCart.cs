using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProjektButikNA
{
    public class ShoppingCart : INotifyPropertyChanged
    {
        private string shoppingCartId;
        private ObservableCollection<ProductInCart> products = new ObservableCollection<ProductInCart>();
        private Coupon coupon;
        private DateTime dateRegistered;
        public event PropertyChangedEventHandler PropertyChanged;
        public const string FILE_PATH = "C:\\Windows\\Temp\\ShoppingCart.xml";

        public ShoppingCart(string shoppingCartId, ObservableCollection<ProductInCart> products, Coupon coupon, DateTime dateRegistered)
        {
            this.shoppingCartId = shoppingCartId;
            this.products = products;
            this.coupon = coupon;
            this.dateRegistered = dateRegistered;
        }

        public ShoppingCart()
        {

        }

        public string ShoppingCartId { get => shoppingCartId; set => shoppingCartId = value; }
        public ObservableCollection<ProductInCart> Products
        {
            get
            {
                return products;
            }
            set
            {
                if (products != value)
                {
                    products = value;
                    OnPropertyChanged("Products");
                }
            }
        }
        [XmlElement(IsNullable = true)]
        public Coupon Coupon { get => coupon; set => coupon = value; }
        public DateTime DateRegistered { get => dateRegistered; set => dateRegistered = value; }
        public double TotalCostInclCoupon { get => GetTotalCost(true); }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public double GetTotalCost(bool includingcoupon)
        {
            double totalCost = 0.00;
            totalCost = products.Select(x => x.Price * x.Amount).Sum();

            if (includingcoupon && coupon != null)
            {
                totalCost *= (1 - coupon.Discount);
            }

            return totalCost;
        }

        private static XmlDocument GetXmlDocument()
        {
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(FILE_PATH))
            {
                using (var stream = new FileStream(FILE_PATH, FileMode.Create))
                {
                    XmlSerializer XML = new XmlSerializer(typeof(List<ShoppingCart>));
                    XML.Serialize(stream, new List<ShoppingCart>());
                }
            }

            doc.Load(FILE_PATH);

            return doc;
        }

        public static List<ShoppingCart> GetShoppingCarts()
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            XmlDocument doc = GetXmlDocument();

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string shoppingCartId = node["ShoppingCartId"].InnerText;

                ObservableCollection<ProductInCart> products = new ObservableCollection<ProductInCart>();
                foreach (XmlNode collectionNode in node["Products"].ChildNodes)
                {
                    string pid = collectionNode["PID"].InnerText;
                    string name = collectionNode["Name"].InnerText;
                    double price = double.Parse(collectionNode["Price"].InnerText);
                    int amount = int.Parse(collectionNode["Amount"].InnerText);
                    products.Add(new ProductInCart(pid, name, price, amount));
                }

                Coupon coupon = null;

                if (node["Coupon"].HasChildNodes)
                {
                    coupon = new Coupon(node["Coupon"]["Code"].InnerText, double.Parse(node["Coupon"]["Discount"].InnerText));
                }
                
                DateTime dateRegistered = DateTime.Parse(node["DateRegistered"].InnerText);
                shoppingCarts.Add(new ShoppingCart(shoppingCartId, products, coupon, dateRegistered));
            }

            return shoppingCarts;
        }

        public static List<ShoppingCart> FilterShoppingList(DateTime datefrom, DateTime dateto)
        {
            List<ShoppingCart> filteredCarts = GetShoppingCarts().Where(x => x.DateRegistered >= datefrom && x.DateRegistered <= dateto).ToList();
            return filteredCarts;
        }

        public static void Save(List<ShoppingCart> shoppingCarts)
        {
            using (var stream = new FileStream(FILE_PATH, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(List<ShoppingCart>));
                XML.Serialize(stream, shoppingCarts);
            }
        }

        public void AddToFile()
        {
            List<ShoppingCart> savedShoppingCarts = GetShoppingCarts();
            savedShoppingCarts.Add(this);
            Save(savedShoppingCarts);
        }
    }
}
