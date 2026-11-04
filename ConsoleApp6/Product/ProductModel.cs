using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp6.Shop_Management
{
    internal class ProductModel
    {
        public int ID;
        public string NAME;
        public string CATEGORY;
        public float PRICE;
        public int STOCK;


        public ProductModel( int Id, string Name, string Category, float Price, int Stock)
        {
           
            ID = Id;
            NAME = Name;
            CATEGORY = Category;
            PRICE = Price;
            STOCK = Stock;
        }
        public ProductModel( )
        {
       
        }

        public override string ToString()
        {
            return $"{ID},{NAME},{CATEGORY},{PRICE},{STOCK}";
        }

    }
}
