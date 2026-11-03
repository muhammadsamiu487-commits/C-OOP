using ConsoleApp6.Saleitem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp6.SaleItem
{
    internal class SaleItemRepo
    {
        private readonly string _file = "SaleItems.txt";
        public void SaveSaleItem(SaleItemModel SaleItem)
        {
            using (StreamWriter stream = new StreamWriter(_file, true))
            {
                stream.WriteLine(SaleItem.ToString());
            }
        }

        public void SaveAllSaleItems(List<SaleItemModel> SaleItems)
        {
            using (StreamWriter stream = new StreamWriter(_file))
            {
                foreach (var item in SaleItems)
                {
                    stream.WriteLine(item.ToString());
                }
            }
        }

        public List<SaleItemModel> GetAllSaleItems()
        {
            List<SaleItemModel> SaleItem_list = new List<SaleItemModel>();
            if (!File.Exists(_file)) // khali list 
                return SaleItem_list;

            using (StreamReader stream = new StreamReader(_file))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    SaleItem_list.Add(new SaleItemModel(int.Parse(parts[0]),int.Parse(parts[1]),int.Parse(parts[2]),float.Parse(parts[3])));
                }
            }
            return SaleItem_list;
        }

        public void ClearAll()
        {
            File.WriteAllText(_file, "");
        }
    }
}
