using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDbControllers
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Customer Customer { get; set; }
    }
}
