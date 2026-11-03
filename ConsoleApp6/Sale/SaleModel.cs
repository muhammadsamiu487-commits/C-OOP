using ConsoleApp6.Saleitem;
using ConsoleApp6.Shop_Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;


namespace ConsoleApp6.Sale
{
    internal class SaleModel
    {
        public int SALE_ID;
        public string DATE;
        public float TOTAL_AMOUNT;
        public List<SaleItemModel> LIST_OF_ITEMS = new List<SaleItemModel>();

        public SaleModel(int Id, string Date)
        {
            SALE_ID = Id;
            DATE = Date;
            TOTAL_AMOUNT = 0;
        }

        public SaleModel(int Id, string Date, float Amount)
        {
            SALE_ID = Id;
            DATE = Date;
            TOTAL_AMOUNT = Amount;
        }

        public void SaleItemConstructorCall(int SaleId, int ProductId, int Quantity, float Price)
        {
            SaleItemModel SaleItem = new SaleItemModel(SaleId, ProductId, Quantity, Price);
            AddSaleItem(SaleItem);
        }


        public void AddSaleItem(SaleItemModel Item)
        {
            LIST_OF_ITEMS.Add(Item);
        }

        public void TotalAmountCalculate()
        {
            TOTAL_AMOUNT = 0;

            foreach (var Item in LIST_OF_ITEMS)
            {
                TOTAL_AMOUNT += Item.PRICE_CROSS_QUANTITY;
            }
        }

        public override string ToString()
        {
            return $"{SALE_ID},{DATE},{TOTAL_AMOUNT}";
        }
    }
}
