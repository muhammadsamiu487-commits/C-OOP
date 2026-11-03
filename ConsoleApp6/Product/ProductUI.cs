using ConsoleApp6.Product;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp6.Shop_Management
{
    internal class ProductUI
    {
        private ProductService _ProductService = new ProductService();
      
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
                Console.WriteLine("7.) Back to Main Menu ");
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
