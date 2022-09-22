using System;
using System.Collections.Generic;

#nullable disable

namespace EcommerceShoppingStore.Models
{
    public partial class Customer
    {
        //public Customer()
        //{
        //    Orders = new HashSet<Order>();
        //}

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DeliveryAddress { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }
    }
}
