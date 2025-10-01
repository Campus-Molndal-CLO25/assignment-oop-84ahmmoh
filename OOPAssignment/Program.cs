using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace OOPAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            string productCsvPath = @"C:\Users\AHMOH\source\repos\assignment-oop-84ahmmoh\OOPAssignment\obj\Debug\net9.0\lager.csv";
            string orderCsvPath = @"C:\Users\AHMOH\source\repos\assignment-oop-84ahmmoh\OOPAssignment\obj\Debug\net9.0\Order.csv";
            string restockCsvPath = @"C:\Users\AHMOH\source\repos\assignment-oop-84ahmmoh\OOPAssignment\obj\Debug\net9.0\Restock.csv";

            // Läs in
            var products = CsvHandler.LoadProducts(productCsvPath);
            var orders = CsvHandler.LoadOrders(orderCsvPath);

            Console.WriteLine($"{products.Count} produkter inlästa.");
            Console.WriteLine($"{orders.Count} ordrar inlästa.\n");

            // Bearbeta ordrar
            var manager = new InventoryManager(products, orders);
            manager.ProcessOrders();

            // 🔹 Automatisk påfyllning från Restock.csv
            var restocks = CsvHandler.LoadRestocks(restockCsvPath);
            if (restocks.Count > 0)
            {
                Console.WriteLine("\nBearbetar restocks från Restock.csv...");
                manager.RestockFromList(restocks);
            }
            else
            {
                Console.WriteLine("\nIngen restock hittades.");
            }

            // 🔹 Manuell extra påfyllning (frivillig)
            Console.WriteLine("\nVill du fylla på en produkt manuellt? (ja/nej)");
            string answer = Console.ReadLine();
            while (answer?.ToLower() == "ja")
            {
                Console.Write("Ange produktnamn: ");
                string name = Console.ReadLine();

                Console.Write("Ange antal att fylla på: ");
                if (int.TryParse(Console.ReadLine(), out int qty))
                {
                    manager.RestockProduct(name, qty);
                }

                Console.WriteLine("Vill du fylla på fler produkter? (ja/nej)");
                answer = Console.ReadLine();
            }

            // Spara uppdaterat lager
            CsvHandler.SaveProducts("lager_uppdaterat.csv", manager.GetUpdatedProducts());
            Console.WriteLine("\nUppdaterat lager sparat i lager_uppdaterat.csv");
        }
    }
}
