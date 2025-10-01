using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPAssignment
{
    public class InventoryManager
    {
        private List<Product> _products;
        private List<Order> _orders;

        public InventoryManager(List<Product> products, List<Order> orders)
        {
            _products = products;
            _orders = orders;
        }

        public void ProcessOrders()
        {
            int successfulOrders = 0;
            int failedOrders = 0;

            foreach (var order in _orders)
            {
                var product = _products.FirstOrDefault(p => p.Name == order.ProductName);
                if (product == null)
                {
                    Console.WriteLine($"✗ Order från {order.CustomerName} misslyckades: {order.ProductName} finns inte i lagret");
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
                    Console.WriteLine($"✗ Order från {order.CustomerName} misslyckades: Otillräckligt lager ({product.Quantity} kvar)");
                    failedOrders++;
                }
            }

            Console.WriteLine("\nOrderbearbetning slutförd!");
            Console.WriteLine($"- {successfulOrders} ordrar skickade");
            Console.WriteLine($"- {failedOrders} ordrar misslyckades");
        }

        public List<Product> GetUpdatedProducts()
        {
            return _products;
        }

        // 🔹 Manuell påfyllning
        public void RestockProduct(string productName, int quantityToAdd)
        {
            var product = _products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
            if (product != null)
            {
                product.Quantity += quantityToAdd;
                Console.WriteLine($"✔ {productName} har fyllts på med {quantityToAdd} st. Nytt saldo: {product.Quantity}");
            }
            else
            {
                Console.WriteLine($"✗ Produkten {productName} hittades inte i lagret!");
            }
        }

        // 🔹 Automatisk påfyllning från Restock.csv
        public void RestockFromList(List<(string ProductName, int Quantity)> restocks)
        {
            foreach (var restock in restocks)
            {
                var product = _products.FirstOrDefault(p => p.Name.Equals(restock.ProductName, StringComparison.OrdinalIgnoreCase));
                if (product != null)
                {
                    product.Quantity += restock.Quantity;
                    Console.WriteLine($"✔ {product.Name} har fyllts på med {restock.Quantity} st. Nytt saldo: {product.Quantity}");
                }
                else
                {
                    Console.WriteLine($"✗ Produkten {restock.ProductName} finns inte i lagret och kunde inte fyllas på.");
                }
            }
        }
    }
}
