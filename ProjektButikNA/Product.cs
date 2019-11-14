using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProjektButikNA
{
    public class Product
    {
        private string name;
        private double price;
        public const string FILE_PATH = "Products.xml";

        public Product(string name, double price)
        {
            this.name = name;
            this.price = price;
        }

        public Product()
        {

        }

        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }

        public static void SaveProducts(List<Product> products)
        {
            using (var stream = new FileStream(FILE_PATH, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(List<Product>));
                XML.Serialize(stream, products);
            }
        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(FILE_PATH))
            {
                using (var stream = new FileStream(FILE_PATH, FileMode.Create))
                {
                    XmlSerializer XML = new XmlSerializer(typeof(List<Product>));
                    XML.Serialize(stream, products);
                }
            }

            doc.Load(FILE_PATH);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string name = node["Name"].InnerText;
                double price = double.Parse(node["Price"].InnerText);
                products.Add(new Product(name, price));
            }

            return products;
        }

        public static List<Product> GetProductsByFilter(string filter)
        {
            List<Product> coupons = new List<Product>();
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(FILE_PATH))
            {
                using (var stream = new FileStream(FILE_PATH, FileMode.Create))
                {
                    XmlSerializer XML = new XmlSerializer(typeof(List<Product>));
                    XML.Serialize(stream, coupons);
                }
            }

            doc.Load(FILE_PATH);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string name = node["Name"].InnerText;
                double price = double.Parse(node["Price"].InnerText);
                coupons.Add(new Product(name, price));
            }

            return coupons;
        }
    }
}
