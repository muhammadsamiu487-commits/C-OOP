using ConsoleApp6.Product;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace ConsoleApp6.Shop_Management
{
    internal class ProductUI
    {
        private ProductService _ProductService = new ProductService();

        public void QuickFilter()
        {
            Console.WriteLine("-- QUICK FILTER --");
            Console.Write("Enter the product category : ");
            string Category = Console.ReadLine();

            Console.Write("Enter the product price : ");
            float Price = float.Parse(Console.ReadLine());

            Console.WriteLine("Choose Comparison by option : ");
            Console.WriteLine($"1. Above {Price})");
            Console.WriteLine($"2. Below {Price})");
            Console.WriteLine($"Press Enter to skip both conditions )");
            Console.Write("Choose an option : ");
            string option = Console.ReadLine();

            List<ProductModel> list  = _ProductService.Product_list();
            int k = 0;
            float sum = 0;
            int count = 0;
            bool flag = false;
            foreach (var Product in list)
            {
                k++;
                if (Category.ToLower() == Product.CATEGORY.ToLower())
                {
                    if (k == 1)
                    {
                        Console.WriteLine($"Product Id\tProduct Name\tProduct Stock");
                    }
                    if(option == "1" && (Product.PRICE>=(float)Price))
                    {
                        Console.WriteLine($"{Product.ID}\t{Product.NAME}\t{Product.STOCK}");
                        sum += Product.PRICE;
                        count++;
                    }
                    if(option == "2" && (Product.PRICE <= (float)Price))
                    {
                        Console.WriteLine($"{Product.ID}\t{Product.NAME}\t{Product.STOCK}");
                        sum += Product.PRICE;
                        count++;

                    }
                    if (option == "")
                    {
                        Console.WriteLine($"{Product.ID}\t{Product.NAME}\t{Product.STOCK}");
                        sum += Product.PRICE;
                        count++;
                    }
                    flag = true;
                }

            }


            if (!flag)
                {
                Console.WriteLine("No record found");
                }

            else
               {     
                float max = -999;
                float min = 999;

                foreach(var Product in list)
                {
                    if (Product.PRICE > max)
                    {
                        max = Product.PRICE;
                    }

                    if(Product.PRICE < min)
                    {
                        min = Product.PRICE;
                    }

                }

                Console.WriteLine("Summary : ");
                Console.WriteLine($"Total Records : {count}");
                Console.WriteLine($"Total Price : {sum}");
                Console.WriteLine($"Highest Price : {max}");
                Console.WriteLine($"Lowest Price : {min}");
               }

        }
      
        public bool AddProduct() {
            Console.WriteLine("Add Product : ");

            Console.Write("Enter the product name: ");
            string Name = Console.ReadLine();

            Console.Write("Enter the product id: ");
            int Id = int.Parse(Console.ReadLine());

            Console.Write("Enter the product category : ");
            string Category = Console.ReadLine();

            Console.Write("Enter the prodoct price : ");
            float Price = float.Parse(Console.ReadLine());

            Console.Write("Enter the prodoct stock : ");
            int Stock = int.Parse(Console.ReadLine());
            //(int id, string name, string category, double price, int stock)
            ProductModel obj = new ProductModel(Id, Name, Category, Price, Stock);

            return  _ProductService.CreateProduct(obj); // service mein iska return Type bool hoga
        }

        public bool UpdateProduct(int ProductId)
        {
            Console.WriteLine("Add Product : ");

            Console.Write("Enter the product name: ");
            string Name = Console.ReadLine();

            Console.Write("Enter the product category : ");
            string Category = Console.ReadLine();

            Console.Write("Enter the product price : ");
            float Price = float.Parse(Console.ReadLine());

            Console.Write("Enter the prodoct stock : ");
            int Stock = int.Parse(Console.ReadLine());

            return _ProductService.Update(ProductId,Name, Category, Price, Stock);
        }

        public void ProductMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-------------------------");
                Console.WriteLine("      Product Menu       ");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1.) Add Product ");
                Console.WriteLine("2.) View All Products ");
                Console.WriteLine("3.) Search Product ");
                Console.WriteLine("4.) Update Product ");
                Console.WriteLine("5.) Delelte Product ");
                Console.WriteLine("6.) Delete all Product ");
                Console.WriteLine("7.) Quick filter ");
                Console.WriteLine("8.) Paginated View ");
                Console.WriteLine("9.) Back to Main Menu ");
                Console.Write("Choose an option : ");
                string Option = Console.ReadLine();

                if (Option == "1")
                {
                    // Add products
                    Console.Clear();
                    if (AddProduct())
                    {
                        Console.WriteLine("Product added successfully");
                    }
                    else
                    {
                        Console.WriteLine("Product not added due to any wrong input in details of product");
                    }
                    Console.Write("Press something to continue ");
                    Console.ReadKey();
                }

                else if (Option == "2")
                {
                    // View All Products
                    Console.Clear();
                    if (_ProductService.Product_list().Count == 0)
                    {
                        Console.WriteLine("No product exists");
                    }
                    else
                    {
                        Console.WriteLine("Products : ");
                        Console.WriteLine("ProductId : ProductName : ProductCategory : ProductPrice : ProductStock");

                        foreach(var Product in _ProductService.ViewAllStudents()) //viewallstudents() retrun list of products 
                        {
                            Console.WriteLine($"{Product.ID} : {Product.NAME} : {Product.CATEGORY}: {Product.PRICE} : {Product.STOCK} ");
                        }
                    }
                    Console.Write("Press something to continue ");
                    Console.ReadKey();
                }

                else if (Option == "3")
                {
                    //searchaproduct
                    Console.Clear();
                    if (_ProductService.Product_list().Count == 0)
                    {
                        Console.WriteLine("No product exists");
                    }
                    else
                    {

                        Console.Write("Enter the product id : ");
                        int ProductId = int.Parse(Console.ReadLine());
                        ProductModel Found = _ProductService.SearchAProduct(ProductId);
                        if (Found != null)
                        {
                            Console.WriteLine("Products details : ");
                            Console.WriteLine("ProductId : ProductName : ProductCategory : ProductPrice : ProductStock");
                            Console.WriteLine($"{Found.ID} : {Found.NAME} : {Found.CATEGORY}: {Found.PRICE} : {Found.STOCK} ");
                        }
                        else
                        {
                            Console.WriteLine("Product does not exsit");
                        }
                    }
                    Console.Write("Press something to continue ");
                    Console.ReadKey();
                }

                else if (Option == "4")
                {

                    //Search A product
                    Console.Clear();
                    if (_ProductService.Product_list().Count == 0)
                    {
                        Console.WriteLine("No product exists");
                    }
                    else
                    {

                        Console.Write("Enter the product id : ");
                        int ProductId = int.Parse(Console.ReadLine());
                        ProductModel Found = _ProductService.SearchAProduct(ProductId);
                        if (Found != null)
                        {
                            Console.WriteLine("Products details : ");
                            Console.WriteLine("ProductId : ProductName : ProductCategory : ProductPrice : ProductStock");
                            Console.WriteLine($"{Found.ID} : {Found.NAME} : {Found.CATEGORY}: {Found.PRICE} : {Found.STOCK} ");
                            if (UpdateProduct(ProductId))
                            {
                                Console.WriteLine("Product Updated Successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Product not Updated as you had entered its any detail Wrong.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Product does not exsit");
                        }

                    }
                    Console.Write("Press something to continue ");
                    Console.ReadKey();
                }

                else if (Option == "5")
                {
                    //deleteaproduct
                    Console.Clear();
                    if (_ProductService.Product_list().Count == 0)
                    {
                        Console.WriteLine("No product exists");
                    }
                    else
                    {

                        Console.Write("Enter the product id : ");
                        int ProductId = int.Parse(Console.ReadLine());
                        ProductModel Found = _ProductService.SearchAProduct(ProductId);
                        if (Found != null)
                        {
                            _ProductService.Delete(Found);
                            Console.WriteLine("Product deleted");
                        }
                        else
                        {
                            Console.WriteLine("Product does not exsit");
                        }
                    }
                    Console.Write("Press something to continue ");
                    Console.ReadKey();

                }

                else if (Option == "6")
                {
                    //Clear All products
                    Console.Clear();
                    if (_ProductService.Product_list().Count != 0)
                    {
                        Console.WriteLine("No product exists");
                         Console.WriteLine("All products are cleared ");
                        _ProductService.ClearAllProducts();
                    }
                    else
                    {
                        Console.WriteLine("No product exists");
                    }
                    Console.Write("Press something to continue ");
                    Console.ReadKey();
                }


                else if (Option == "7")
                {
                    //function of quick filter
                    Console.Clear();
                    QuickFilter();
                    Console.Write("Press something to continue");
                    Console.ReadKey();
                }

                else if (Option == "8")
                {
                   // pagianted View function
                }

                else if (Option == "9")
                {
                    return;  // wohi pr control chala gy ga jahn se  call ayi thi
                }

                else
                {
                    Console.Write("Invalid Input");
                    Console.ReadKey();

                }
            }
            
        }
    }
}
