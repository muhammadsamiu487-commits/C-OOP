using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp6.Saleitem
{
    internal class SaleItemModel
    {
        public int SALE_ID;
        public int PRODUCT_ID;
        public int QUANTITY;
        public float PRICE;
        public float PRICE_CROSS_QUANTITY;

        public SaleItemModel(int sale_id, int product_id, int quantity, float price)
        {
            SALE_ID = sale_id;
            PRODUCT_ID = product_id;
            QUANTITY = quantity;
            PRICE = price;
            PRICE_CROSS_QUANTITY = quantity * price;
        }

        public override string ToString()
        {
            return $"{SALE_ID},{PRODUCT_ID},{QUANTITY},{PRICE}";
        }
    }
}
