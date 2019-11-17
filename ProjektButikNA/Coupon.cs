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
    public class Coupon
    {
        private string code;
        private double discount;
        public const string FILE_PATH = "C:\\Windows\\Temp\\Coupons.xml";

        public Coupon(string code, double discount)
        {
            this.code = code;
            this.discount = discount;
        }

        public Coupon()
        {

        }

        public string Code { get => code; set => code = value; }
        public double Discount { get => discount; set => discount = value; }

        public static void SaveCoupons(List<Coupon> coupons)
        {
            using (var stream = new FileStream(FILE_PATH, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(List<Coupon>));
                XML.Serialize(stream, coupons);
            }
        }

        private static XmlDocument GetCouponXmlDocument()
        {
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(FILE_PATH))
            {
                using (var stream = new FileStream(FILE_PATH, FileMode.Create))
                {
                    XmlSerializer XML = new XmlSerializer(typeof(List<Coupon>));
                    XML.Serialize(stream, new List<Coupon>());
                }
            }

            doc.Load(FILE_PATH);

            return doc;
        }

        public static List<Coupon> GetCoupons()
        {
            List<Coupon> coupons = new List<Coupon>();
            XmlDocument doc = GetCouponXmlDocument();

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string code = node["Code"].InnerText;
                double discount = double.Parse(node["Discount"].InnerText);
                coupons.Add(new Coupon(code, discount));
            }

            return coupons;
        }

        public static Coupon GetCoupon(string code)
        {
            Coupon coupon = GetCoupons().Where(x => x.Code == code).FirstOrDefault();
            return coupon;
        }
    }
}
