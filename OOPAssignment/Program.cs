using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace OOPAssignment
{
    class Program
    {
        private static List<Product> products = new List<Product>();
        private static List<Order> orders = new List<Order>();

        static void Main(string[] args)
        {
            // Use absolute paths for both CSV files, or ensure they are copied to the output directory.
            string productCsvPath = @"C:\Users\AHMOH\source\repos\assignment-oop-84ahmmoh\OOPAssignment\obj\Debug\net9.0\lager.csv";
            string orderCsvPath = @"C:\Users\AHMOH\source\repos\assignment-oop-84ahmmoh\OOPAssignment\obj\Debug\net9.0\Order.csv";

            Console.WriteLine(productCsvPath + "...");
            LoadProductsFromCsv(productCsvPath);
            Console.WriteLine($"{products.Count} produkter inlästa.\n");

            Console.WriteLine("Bearbetar ordrar från ordrar.csv...\n");
            LoadOrdersFromCsv(orderCsvPath);

            ProcessOrders();

            Console.WriteLine("\nSparar uppdaterat lager till lager_uppdaterat.csv...");
            SaveUpdatedProductsToCsv("lager_uppdaterat.csv");
            Console.WriteLine("Klart!");
        }

        static void LoadProductsFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Filen {filePath} hittades inte!");
                return;
            }

            var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);

            foreach (var line in lines.Skip(1)) // Skip header
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim())
                                .ToArray();

                if (parts.Length < 4)
                {
                    Console.WriteLine($"Ogiltig rad i CSV: {line}");
                    continue;
                }

                if (!decimal.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) ||
                    !int.TryParse(parts[3], out int quantity))
                {
                    Console.WriteLine($"Ogiltig rad i CSV (fel format): {line}");
                    continue;
                }

                products.Add(new Product
                {
                    Name = parts[0],
                    Category = parts[1],
                    Price = price,
                    Quantity = quantity
                });
            }
        }

        static void LoadOrdersFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Filen {filePath} hittades inte!");
                return;
            }

            var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim())
                                .ToArray();

                if (parts.Length < 4 || !int.TryParse(parts[3], out int quantity))
                {
                    Console.WriteLine($"Ogiltig orderrad: {line}");
                    continue;
                }

                orders.Add(new Order
                {
                    CustomerId = parts[0],
                    CustomerName = parts[1],
                    ProductName = parts[2],
                    QuantityOrdered = quantity
                });
            }

            Console.WriteLine($"Laddade {orders.Count} ordrar.\n");
        }

        static void ProcessOrders()
        {
            int successfulOrders = 0;
            int failedOrders = 0;

            foreach (var order in orders)
            {
                var product = products.FirstOrDefault(p => p.Name == order.ProductName);
                if (product == null)
                {
                    Console.WriteLine($"✗ Order från {order.CustomerName} kunde inte skickas: {order.ProductName} finns inte i lagret");
                    failedOrders++;
                    continue;
                }

                if (product.Quantity >= order.QuantityOrdered)
                {
                    product.Quantity -= order.QuantityOrdered;
                    Console.WriteLine($"✓ Order från {order.CustomerName} skickad: {order.QuantityOrdered}x {order.ProductName}");
                    successfulOrders++;
                }
                else
                {
                    Console.WriteLine($"✗ Order från {order.CustomerName} kunde inte skickas: Otillräckligt lager för {order.ProductName} (begärt: {order.QuantityOrdered}, finns: {product.Quantity})");
                    failedOrders++;
                }
            }

            Console.WriteLine("\nOrderbearbetning slutförd!");
            Console.WriteLine($"- {successfulOrders} ordrar skickade");
            Console.WriteLine($"- {failedOrders} ordrar kunde inte skickas");
        }

        static void SaveUpdatedProductsToCsv(string filePath)
        {
            var lines = new List<string> { "Name,Category,Price,Quantity" };
            foreach (var product in products)
            {
                lines.Add($"{product.Name},{product.Category},{product.Price.ToString(CultureInfo.InvariantCulture)},{product.Quantity}");
            }
            File.WriteAllLines(filePath, lines, System.Text.Encoding.UTF8);
        }
    }
}
