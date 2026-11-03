using ConsoleApp6.Saleitem;
using ConsoleApp6.SaleItem;
using ConsoleApp6.Shop_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApp6.Product;
using System.Xml.Linq;

namespace ConsoleApp6.Sale
{
    internal class SaleService
    {
        private readonly SaleRepo _SaleRepo = new SaleRepo();
        private readonly SaleItemService _SaleItemService = new SaleItemService();
        private readonly ProductRepo _ProductRepo = new ProductRepo(); // use hogi for showing products available in main product list 

        //List of products and Sales function
        public List<SaleModel> GetAllSales()
        {
            return _SaleRepo.GetallSales();
        }

        public List<ProductModel> GetAllProducts()
        {
            return _ProductRepo.GetAllProducts();
        }

        //VAlidations
        public bool UniqueSaleIdOrNot(int SaleId)
        {
            foreach (var Sale in GetAllSales())
            {
                if (Sale.SALE_ID == SaleId)
                    return false;
            }

            return true;
        }

        public bool ValidSaleIdOrNot(int SaleId)
        {
            return SaleId > 0;
        }

        public bool ValidQuantityOrNot(int Quantity)
        {
            return Quantity > 0;
        }

        //CRUD and other operations
        public bool AddSaleItemToCart(SaleModel Sale, int ProductId, int Quantity)
        {
            ProductModel Product = null; 
            // ager quantity less than 0 hoi , Quantity stock se less hui ya product exist na krta hua then false
            if (!ValidQuantityOrNot(Quantity))
            {
                return false;
            }

            foreach (var Prod in GetAllProducts())
            {
                if (Prod.ID == ProductId)
                {
                    Product = Prod;
                    break;
                }
            }

            if (Product == null) // ager user nein wo wali product id choose ki jo product list mein exist hi ni krti
            {
                return false;
            }

            int AlreadyThere = 0; // ager list mein user ne phle hi se same product buy kiya ho uski quantity calculate krnein ke liye 

            foreach (var Sale_Item in Sale.LIST_OF_ITEMS)
            {
                if (Sale_Item.PRODUCT_ID == ProductId)
                    AlreadyThere += Sale_Item.QUANTITY;
            }

            if (Product.STOCK < Quantity + AlreadyThere)
            {
                return false;
            }

            Sale.SaleItemConstructorCall(Sale.SALE_ID, Product.ID,Quantity,Product.PRICE); 
            // the Sale is the object and the method is in model class (which will call constructor pf SaleItem model
            // will also add the salitem to sale items list of a sale) - COMPOSITIN

            return true;
        }

        public bool CreateSale(SaleModel Sale)
        {
            if(!ValidSaleIdOrNot(Sale.SALE_ID))
            {
                return false;
            }

            if(!UniqueSaleIdOrNot(Sale.SALE_ID)) // agere false return ho it means sale phle exist krti he so it is not unique
            {
                return false; 
            }

            if(Sale.LIST_OF_ITEMS.Count == 0) // ye check krnein ke liye he sale mein koi item karida bhi he ya cart khali he
            {
                return false;
            }

            List<ProductModel> Products = GetAllProducts();
            foreach (var item in Sale.LIST_OF_ITEMS)
            {
                ProductModel Product = null;
                foreach (var P in Products)
                {
                    if (P.ID == item.PRODUCT_ID)
                    {
                        Product = P;
                        break;
                    }
                }
                if (Product == null || Product.STOCK < item.QUANTITY)
                    return false;
            }

            Sale.TotalAmountCalculate();
            _SaleRepo.CreateSale(Sale); // saves Sale in Sale Repo 

            foreach (var Item in Sale.LIST_OF_ITEMS)
            {
                _SaleItemService.AddSaleItem(Item); // ye SaleItem service ka call kre gi and that 
                foreach (var Product in Products)
                {
                    if (Product.ID == Item.PRODUCT_ID)
                    {
                        Product.STOCK -= Item.QUANTITY;
                        break;
                    }
                }
            }

            _ProductRepo.CreateAllProducts(Products); // Save products in product repo kyui ke stocks mein kami ayui he
            return true;
        }

        public SaleModel SearchSale(int SaleId)
        {
            foreach (var Sale in GetAllSales())
            {
                if (Sale.SALE_ID == SaleId)
                    return Sale;
            }

            return null;
        }

        public bool DeleteSale(int SaleId)
        {
            SaleModel Sale = SearchSale(SaleId);//serach kre ga ke sale id exist krti he ya nahin 
            List<SaleModel> Sales = GetAllSales();

            if (Sale == null)
            {
                return false;
            }

            for (int i = 0; i < Sales.Count; i++)
            {
                if (Sales[i].SALE_ID == SaleId)
                {
                    Sales.RemoveAt(i);
                    break;
                }
            }

            _SaleRepo.CreateAllSales(Sales); 
            _SaleItemService.DeleteSaleItems(SaleId);

            return true;
        
        }
    }
}
