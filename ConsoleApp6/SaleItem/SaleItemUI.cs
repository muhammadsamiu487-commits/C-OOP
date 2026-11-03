using ConsoleApp6.Saleitem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp6.SaleItem
{
    internal class SaleItemUI
    {
        private readonly SaleItemService _SaleItemService = new SaleItemService();

        public void ViewSaleItems(int SaleId) // Sirf Sale ke search a sale and view all sales mein use hua he
        {
            List<SaleItemModel> SaleItems = _SaleItemService.GetSaleItemsBySaleId(SaleId); // it will return list of sale items in 
                                                                                           // given SaleId .
            if (SaleItems.Count == 0)
            {
                Console.WriteLine("No items found for this sale.");          
            }

            Console.WriteLine("\nSale ID\tProduct ID\tQuantity\tPrice");
            foreach (var item in SaleItems)
            {
                Console.WriteLine($"{item.SALE_ID}\t{item.PRODUCT_ID}\t{item.QUANTITY}\t{item.PRICE}");
            }
        }
    }
}
