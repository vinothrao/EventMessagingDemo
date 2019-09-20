using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMessagingDemo.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }       
        public int Quantity { get; set; }
        public string Status { get; set; } = "Ordered";
    }
}
