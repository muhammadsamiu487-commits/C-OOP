using ConsoleApp6.Sale;
using ConsoleApp6.Shop_Management;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ConsoleApp6.Product
{
    internal class ProductRepo
    {
        private readonly string _file = "Product.txt";

        public void CreateProduct(ProductModel Product)
        {
            //return $"{ID},{NAME},{CATEGORY},{PRICE},{STOCK}";
            using (StreamWriter stream = new StreamWriter(_file, true))
            {
                stream.WriteLine(Product.ToString());
            }
        }

        public void CreateAllProducts(List<ProductModel> Products)
        {
            //return $"{ID},{NAME},{CATEGORY},{PRICE},{STOCK}";
            using (StreamWriter stream = new StreamWriter(_file))
            {
                foreach (var Product in Products)
                {
                    stream.WriteLine(Product.ToString());
                }
            }
        }

        public List<ProductModel> GetAllProducts()
        {
            //return $"{ID},{NAME},{CATEGORY},{PRICE},{STOCK}";
            List<ProductModel> ProductsList = new List<ProductModel>();

            if (!File.Exists(_file))
            {
                return ProductsList; // khali list
            }

            using (StreamReader stream = new StreamReader(_file))
            {
                string SaleLine = "";
                while ((SaleLine = stream.ReadLine()) != null)
                {
                    string[] parts = SaleLine.Split(',');
                    ProductsList.Add(new ProductModel(int.Parse(parts[0]), parts[1], parts[2], float.Parse(parts[3]), int.Parse(parts[4])));
                }
            }

            return ProductsList;
        }

        public void ClearAll()
        {
            File.WriteAllText(_file, "");
        }
    }
}
