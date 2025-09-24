using OOPAssignment;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LagerHanteringsSystem
{
    class Program
    {
        private static List<Product> products = new();
        private static List<Order> orders = new();

        static void Main(string[] args)
        {
            // 1. Läs in produkter och ordrar
            LoadProductsFromCsv(@"C:\Users\AHMOH\source\repos\assignment-oop-84ahmmoh\OOPAssignment\obj\Debug\net10.0\lager.csv");
            LoadOrdersFromCsv(@"C:\temp\LagerHanteringsSystem\LagerHanteringsSystem\obj\Debug\net8.0\Order.csv");

            // 2. Bearbeta ordrar
            ProcessOrders();

            // 3. Spara uppdaterat lager
            SaveUpdatedProductsToCsv("lager_uppdaterat.csv");
        }

        static void LoadProductsFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Filen {filePath} hittades inte!");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1)) // Skippa header
            {
                var parts = line.Split(';');
                var product = new Product
                {
                    Name = parts[0],
                    Category = parts[1],
                    Price = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
                    Quantity = int.Parse(parts[3])
                };
                products.Add(product);
            }
            Console.WriteLine($"Laddade {products.Count} produkter.");
        }

        static void LoadOrdersFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Filen {filePath} hittades inte!");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1)) // Skippa header
            {
                var parts = line.Split(';');
                var order = new Order
                {
                    CustomerId = parts[0],
                    CustomerName = parts[1],
                    ProductName = parts[2],
                    QuantityOrdered = int.Parse(parts[3])
                };
                orders.Add(order);
            }
            Console.WriteLine($"Laddade {orders.Count} ordrar.");
        }

        static void ProcessOrders()
        {
            foreach (var order in orders)
            {
                var product = products.FirstOrDefault(p => p.Name == order.ProductName);
                if (product == null)
                {
                    Console.WriteLine($"Order misslyckades: {order.ProductName} finns inte i lagret.");
                    continue;
                }

                if (product.CanFulfillOrder(order.QuantityOrdered))
                {
                    product.ReduceQuantity(order.QuantityOrdered);
                    Console.WriteLine($"Order OK: {order.CustomerName} köpte {order.QuantityOrdered} st {order.ProductName}.");
                }
                else
                {
                    Console.WriteLine($"Order misslyckades: {order.CustomerName} ville ha {order.QuantityOrdered} st {order.ProductName}, men endast {product.Quantity} finns kvar.");
                }
            }
        }

        static void SaveUpdatedProductsToCsv(string filePath)
        {
            var lines = new List<string> { "Name;Category;Price;Quantity" };
            foreach (var product in products)
            {
                lines.Add($"{product.Name};{product.Category};{product.Price};{product.Quantity}");
            }
            File.WriteAllLines(filePath, lines);
            Console.WriteLine($"Uppdaterat lager sparat till {filePath}");
        }
    }
}
