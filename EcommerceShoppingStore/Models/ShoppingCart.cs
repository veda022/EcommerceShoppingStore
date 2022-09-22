﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EcommerceShoppingStore.Models
{
    public partial class ShoppingCart
    {
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public long? Quantity { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? ProductId { get; set; }

        //public virtual Product Product { get; set; }
    }
}
