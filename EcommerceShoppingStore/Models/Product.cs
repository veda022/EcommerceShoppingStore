using System;
using System.Collections.Generic;

#nullable disable

namespace EcommerceShoppingStore.Models
{
    public partial class Product
    {
        //public Product()
        //{
        //    OrderDetails = new HashSet<OrderDetail>();
        //    ShoppingCarts = new HashSet<ShoppingCart>();
        //}

        public int ProductsId { get; set; }
        public string ModelNumber { get; set; }
        public string ModelName { get; set; }
        public long? UnitCost { get; set; }
        public string Product_Description { get; set; }
        public int? CategoryId { get; set; }

        //public virtual Category Category { get; set; }
        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        //public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
