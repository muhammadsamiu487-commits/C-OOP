using ConsoleApp6.Product;
using ConsoleApp6.Sale;
using ConsoleApp6.Shop_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp6
{
    internal class Shop
    {
        private ProductUI _ProductUi = new ProductUI();
        private SaleUI _SaleUi = new SaleUI();
        private ProductService _ProductService = new ProductService();
        public void CallMainMenu()
        {
            MainMenu();
        }


        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----------------------------");
                Console.WriteLine("- Shop Management  System  -");
                Console.WriteLine("----------------------------");
                Console.WriteLine("1.) Product Management");
                Console.WriteLine("2.) Sell Products");
                Console.WriteLine("3.) View Sales History");
                Console.WriteLine("4.) Check Low Stock");
                Console.WriteLine("5.) Exit");
                Console.Write("Choose an option: ");

                string Option = Console.ReadLine();

                if (Option == "1")
                {
                    Console.Clear();
                    _ProductUi.ProductMenu();
                }
                else if (Option == "2")
                {
                    Console.Clear();
                    _SaleUi.SaleMenu();
                }
                else if (Option == "3")
                {
                    Console.Clear();
                    _SaleUi.ViewAllSales();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (Option == "4")
                {
                    Console.Clear();
                    List<ProductModel> Products = _ProductService.Product_list();
       
                    bool found = false;
                    int k = 0;
                    foreach (var Product in Products)
                    {
                        if (Product.STOCK <= 5)
                        {
                            k++;
                            if (k == 1)
                            {
                                Console.WriteLine("-------- Low Stock Products --------");
                            }
                            Console.WriteLine($"ID: {Product.ID} | Name: {Product.NAME} | Stock: {Product.STOCK}");
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("No low stock products found.");
                    }

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (Option == "5")
                {
                    //Exit 
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                    Console.ReadKey();
                }
            }
        }
    }
}
