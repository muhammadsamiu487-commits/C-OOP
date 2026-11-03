using ConsoleApp6.Shop_Management;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Xml.Linq;


namespace ConsoleApp6.Product
{
    internal class ProductService
    {
        private readonly ProductRepo _ProductRepo = new ProductRepo();
     
        //List function : ye main list kre ga of repo
        public List<ProductModel> Product_list()
        {
            List<ProductModel> Product_list = _ProductRepo.GetAllProducts();
            return Product_list;
        }

        //Validation Operations and all of them should have bool return type
        public bool ValidNameOrNot(string Name)
        {
            return Name != null && Name.Trim() != "";
        }

        public bool ValidCategoryOrNot(string Category)
        {
            return Category != null && Category.Trim() != "";
        }

        public bool ValidPriceOrNot(double Price)
        {
            return Price > 0;
        }

        public bool ValidStockOrNot(int Stock)
        {
            return Stock >= 0;
        }

        public bool UniqueIdOrNot(int ProductId)
        {
            List<ProductModel> ListOfProducts = Product_list();
            foreach (var Product in ListOfProducts)
            {
                if (Product.ID == ProductId)
                {
                    return false;
                }
            }
            return true;
        }
        
        //CRUD and Search Operations
        public bool CreateProduct(ProductModel Product)
        {
            if(ValidNameOrNot(Product.NAME) && ValidCategoryOrNot(Product.CATEGORY) && ValidPriceOrNot(Product.PRICE) &&
                ValidStockOrNot(Product.STOCK) && UniqueIdOrNot(Product.ID)) {


                _ProductRepo.CreateProduct(Product);
                 return true; 

            }
            return false;
        }

        public  List<ProductModel> ViewAllStudents()
        {
            return Product_list();
        }
        public ProductModel SearchAProduct(int ProductId) // object ager mil gya wrna null
        { 
            
            foreach(var Product in Product_list())
            {
                if (Product.ID == ProductId)
                {
                    return Product;
                }
            }
            return null;
        }

        public bool Update(int ProductId, string Name, string Category, float Price, int Stock)
        {
            if (!ValidNameOrNot(Name) || !ValidCategoryOrNot(Category) || !ValidPriceOrNot(Price) || !ValidStockOrNot(Stock))
            {
                return false;
            }

            List<ProductModel> ListOfProducts = Product_list();

            foreach (var Product in ListOfProducts)
            {
                if (Product.ID == ProductId)
                {
                    Product.NAME = Name;
                    Product.CATEGORY = Category;
                    Product.PRICE = Price;
                    Product.STOCK = Stock;
                    _ProductRepo.CreateAllProducts(ListOfProducts);
                    return true;
                }
            }
            return false;    
        }

        public void Delete(ProductModel ProductToBeDel)
        {
            List<ProductModel> ListOfProducts =  Product_list();
             for (int i = 0; i < ListOfProducts.Count; i++)
            {
                if (ProductToBeDel.NAME == ListOfProducts[i].NAME)
                {
                    ListOfProducts.RemoveAt(i);
                    break;
                }
            }
               _ProductRepo.CreateAllProducts(ListOfProducts);
        }

        public void ClearAllProducts()
        {
            _ProductRepo.ClearAll();
        }


        
    }
}

