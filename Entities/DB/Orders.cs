using System;
using System.Collections.Generic;

namespace Entities.DB
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public string Status { get; set; }
    }
}
