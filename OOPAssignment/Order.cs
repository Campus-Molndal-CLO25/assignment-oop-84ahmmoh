using System;
using System.Collections.Generic;

namespace OOPAssignment
{
    // Representerar en enskild kundorder
    class Order
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int QuantityOrdered { get; set; }
    }
}