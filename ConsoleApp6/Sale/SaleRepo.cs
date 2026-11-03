using ConsoleApp6.Shop_Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp6.Sale
{
    internal class SaleRepo
    {
        private readonly string _file = "Sales.txt";

        public void CreateSale(SaleModel Sale)
        { // return $"{SALE_ID},{DATE},{TOTAL_AMOUNT}"
            using (StreamWriter stream = new StreamWriter(_file, true))
            {
                stream.WriteLine(Sale.ToString());
            }
        }

        public void CreateAllSales(List<SaleModel> Sales)
        { // return $"{SALE_ID},{DATE},{TOTAL_AMOUNT}"
            using (StreamWriter stream = new StreamWriter(_file))
            {
                foreach (var Sale in Sales)
                {
                    stream.WriteLine(Sale.ToString());
                }
            }
        }

        public List<SaleModel> GetallSales()
        { // return $"{SALE_ID},{DATE},{TOTAL_AMOUNT}"
            List<SaleModel> Sale_list = new List<SaleModel>();

            if (!File.Exists(_file))
            {
                return Sale_list; // khali list
            }

            using (StreamReader stream = new StreamReader(_file))
            {
                string SaleLine="";
                while ((SaleLine = stream.ReadLine()) != null)
                {
                    string[] parts = SaleLine.Split(',');
                    Sale_list.Add(new SaleModel(int.Parse(parts[0]),parts[1],float.Parse(parts[2])));
                }
            }

            return Sale_list;
        }

        public void ClearAll()
        {
            File.WriteAllText(_file, "");
        }
    }
}
