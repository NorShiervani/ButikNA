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
        private string pid;
        private string name;
        private double price;
        public const string FILE_PATH = "C:\\Windows\\Temp\\Products.xml";

        public Product(string pid, string name, double price)
        {
            PID = pid;
            Name = name;
            Price = price;
        }

        public Product()
        {

        }

        public string PID { get => pid; set => pid = value; }
        public string Name { get => name; set => name = value; }
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The price must be at least 1 or higher.");
                }
                else
                {
                    price = value;
                }
            }
        }

        public static void SaveProducts(List<Product> products)
        {
            using (var stream = new FileStream(FILE_PATH, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(List<Product>));
                XML.Serialize(stream, products);
            }
        }

        private static XmlDocument GetProductXmlDocument()
        {
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(FILE_PATH))
            {
                using (var stream = new FileStream(FILE_PATH, FileMode.Create))
                {
                    XmlSerializer XML = new XmlSerializer(typeof(List<Product>));
                    XML.Serialize(stream, new List<Product>());
                }
            }

            doc.Load(FILE_PATH);

            return doc;
        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            XmlDocument doc = GetProductXmlDocument();

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string pid = node["PID"].InnerText;
                string name = node["Name"].InnerText;
                double price = double.Parse(node["Price"].InnerText);
                products.Add(new Product(pid, name, price));
            }

            return products;
        }

        public static List<Product> GetProductsByFilter(string productname)
        {
            List<Product> filteredProducts = GetProducts().Where(x => x.Name.ToLower().Contains(productname.ToLower())).ToList();

            return filteredProducts;
        }

        public static void Save(List<Product> products)
        {
            using (var stream = new FileStream(FILE_PATH, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(List<Product>));
                XML.Serialize(stream, products);
            }
        }

        public static bool productExist(string pid)
        {
            Product product =  GetProducts().Where(x => x.PID == pid).FirstOrDefault();

            return product != null;
        }

        public void AddToFile()
        {
            List<Product> savedProducts = GetProducts();
            savedProducts.Add(this);
            Save(savedProducts);
        }

        public static void RemoveFromFile(string pid)
        {
            List<Product> updatedProducts = GetProducts();
            updatedProducts.RemoveAll(x => x.PID == pid);
            Save(updatedProducts);
        }
    }
}
