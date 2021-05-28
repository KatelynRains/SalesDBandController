using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDbControllers
{
    class OrderLine
    {
        public int Id { get; set; }
        public int OrdersId { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; }
    }
}
