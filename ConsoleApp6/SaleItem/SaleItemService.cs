using ConsoleApp6.Saleitem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp6.SaleItem
{
    internal class SaleItemService
    {
        private readonly SaleItemRepo _SaleItemRepo = new SaleItemRepo();
        public List<SaleItemModel> GetAllSaleItems()
        {
            return _SaleItemRepo.GetAllSaleItems();
        }

        public List<SaleItemModel> GetSaleItemsBySaleId(int SaleId)
        {
            List<SaleItemModel> SaleItems = new List<SaleItemModel>();
            foreach (var item in GetAllSaleItems())
            {
                if (item.SALE_ID == SaleId)
                {
                    SaleItems.Add(item);
                }
            }
            return SaleItems;
        }

        public bool AddSaleItem(SaleItemModel SaleItem)
        {
            //if (!IsValidSaleId(SaleItem.SALE_ID) || !IsValidProductId(SaleItem.PRODUCT_ID) ||
            //    !IsValidQuantity(SaleItem.QUANTITY) || !IsValidPrice(SaleItem.PRICE) ){
            //    return false;
            //}

            _SaleItemRepo.SaveSaleItem(SaleItem); // Saves Saleitem in Sale_Item_Repo
            return true;
        }

        //public bool IsValidSaleId(int SaleId)
        //{
        //    return SaleId > 0;
        //}

        //public bool IsValidProductId(int ProductId)
        //{
        //    return ProductId > 0;
        //}

        //public bool IsValidQuantity(int Quantity)
        //{
        //    return Quantity > 0;
        //}

        //public bool IsValidPrice(float Price)
        //{
        //    return Price > 0;
        //}

        public void DeleteSaleItems(int SaleId)
        {
            List<SaleItemModel> SaleItems = GetAllSaleItems();
            for (int i = 0; i < SaleItems.Count; i++)
            {
                if (SaleItems[i].SALE_ID == SaleId)
                {
                    SaleItems.RemoveAt(i);
                    //i-- --------------------------------------->>>>>>>
                }
            }
            _SaleItemRepo.SaveAllSaleItems(SaleItems); // append kr di updated saleitem list
        }

    }
}
