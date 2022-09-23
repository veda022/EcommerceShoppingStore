using System;
using System.Collections.Generic;

#nullable disable

namespace EcommerceShoppingStore.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public int? CustomerId { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}
