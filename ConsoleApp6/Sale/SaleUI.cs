using ConsoleApp6.SaleItem;
using ConsoleApp6.Shop_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConsoleApp6.Sale
{
    internal class SaleUI
    {
        private readonly SaleService _SaleService = new SaleService();
        private readonly SaleItemUI _SaleItemUI = new SaleItemUI();

        public void SaleMenu()
        {
            while (true)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("        Sale Menu        ");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1.) Create new sale");
                Console.WriteLine("2.) View all sales");
                Console.WriteLine("3.) Search sale by id");
                Console.WriteLine("4.) Delete Sale");
                Console.WriteLine("5.) Back to main menu");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    CreateSale();
                    Console.WriteLine("Press Something to Continue");
                    Console.ReadKey();
                }
                else if (option == "2")
                {
                    ViewAllSales();
                    Console.WriteLine("Press Something to Continue");
                    Console.ReadKey();
                }
                else if (option == "3")
                {
                    SearchSale();
                    Console.WriteLine("Press Something to Continue");
                    Console.ReadKey();
                }
                else if (option == "4")
                {
                    DeleteSale();
                    Console.WriteLine("Press Something to Continue");
                    Console.ReadKey();
                }
                else if (option == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }

        public void CreateSale()
        {
            Console.Write("Enter sale id: ");
            int SaleId = int.Parse(Console.ReadLine());
            Console.Write("Enter the date (yyyy-mm-dd): ");
            string Date = Console.ReadLine();

            SaleModel Sale = new SaleModel(SaleId,Date);

            List<ProductModel> Products = _SaleService.GetAllProducts();

            Console.WriteLine("\nAvailable Products:"); // sare available products show kre ga
            foreach (var Product in Products)
            {
                Console.WriteLine($"ID: {Product.ID} | Name: {Product.NAME} | Price: {Product.PRICE} | Stock: {Product.STOCK}");
            }

            while (true) // ye allow kre ga ke user multiple sale item krta rahe jb tk wo productID 0 enetr na kre
            {

                Console.Write("\nEnter Product ID (0 to finish): ");
                int ProductId = int.Parse(Console.ReadLine());

                if (ProductId == 0)
                    break;

                Console.Write("Enter Quantity: ");
                int Quantity = int.Parse(Console.ReadLine());

                bool Result = _SaleService.AddSaleItemToCart(Sale, ProductId, Quantity); //return true or false(proid false hui,
                                                                                         //qunatity > stock ),quantity < 0) jis base
                                                                                         //pe item sale mein add hoga ya nahin
                if (Result)
                    Console.WriteLine("Item added successfully.");
                else
                    Console.WriteLine("Invalid product or insufficient stock.");
            }

            bool Created = _SaleService.CreateSale(Sale);

            if (Created)
            {
                Console.WriteLine("\nSale created successfully!");
                Console.WriteLine($"Total Amount: {Sale.TOTAL_AMOUNT}");
            }
            else
            {
                Console.WriteLine("Sale could not be created.");
            }
        }

        public void ViewAllSales()
        {
            List<SaleModel> Sales = _SaleService.GetAllSales();

            if (Sales.Count == 0)
            {
                Console.WriteLine("No sales found.");
                return;
            }

            foreach (var Sale in Sales)
            {
                Console.WriteLine("\n--------------------");
                Console.WriteLine($"Sale ID: {Sale.SALE_ID}");
                Console.WriteLine($"Date: {Sale.DATE}");
                Console.WriteLine($"Total Amount: {Sale.TOTAL_AMOUNT}");
                _SaleItemUI.ViewSaleItems(Sale.SALE_ID);
            }
        }

        public void SearchSale()
        {
            Console.Write("Enter Sale ID: ");
            int SaleId = int.Parse(Console.ReadLine());

            SaleModel Sale = _SaleService.SearchSale(SaleId);
            if (Sale == null)
            {
                Console.WriteLine("Sale not found.");
                return;
            }

            Console.WriteLine($"\nSale ID: {Sale.SALE_ID}"); // shows SaleId,SaleDate,SaleAmount
            Console.WriteLine($"Date: {Sale.DATE}");
            Console.WriteLine($"Total Amount: {Sale.TOTAL_AMOUNT}");

            _SaleItemUI.ViewSaleItems(Sale.SALE_ID); // ViewSaleItems is in SaleUI which Shows SaleItems details 
        }

        public void DeleteSale()
        {
            Console.Write("Enter Sale ID: ");
            int SaleId = int.Parse(Console.ReadLine());

            bool Result = _SaleService.DeleteSale(SaleId);

            if (Result)
                Console.WriteLine("Sale deleted successfully.");
            else
                Console.WriteLine("Sale not found.");
        }
    }
}
