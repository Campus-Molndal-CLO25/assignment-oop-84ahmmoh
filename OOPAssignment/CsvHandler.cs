using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace OOPAssignment
{
    public static class CsvHandler
    {
        public static List<Product> LoadProducts(string filePath)
        {
            var products = new List<Product>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Filen {filePath} hittades inte!");
                return products;
            }

            var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim())
                                .ToArray();

                if (parts.Length < 4) continue;

                if (!decimal.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) ||
                    !int.TryParse(parts[3], out int quantity))
                    continue;

                products.Add(new Product
                {
                    Name = parts[0],
                    Category = parts[1],
                    Price = price,
                    Quantity = quantity
                });
            }

            return products;
        }

        public static List<Order> LoadOrders(string filePath)
        {
            var orders = new List<Order>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Filen {filePath} hittades inte!");
                return orders;
            }

            var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim())
                                .ToArray();

                if (parts.Length < 4 || !int.TryParse(parts[3], out int quantity))
                    continue;

                orders.Add(new Order
                {
                    CustomerId = parts[0],
                    CustomerName = parts[1],
                    ProductName = parts[2],
                    QuantityOrdered = quantity
                });
            }

            return orders;
        }

        // 🔹 Ny metod för att läsa in påfyllning
        public static List<(string ProductName, int Quantity)> LoadRestocks(string filePath)
        {
            var restocks = new List<(string ProductName, int Quantity)>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Filen {filePath} hittades inte!");
                return restocks;
            }

            var lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);
            foreach (var line in lines.Skip(1)) // hoppa över header
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim())
                                .ToArray();

                if (parts.Length < 2 || !int.TryParse(parts[1], out int qty)) continue;

                restocks.Add((parts[0], qty));
            }

            return restocks;
        }

        public static void SaveProducts(string filePath, List<Product> products)
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
