﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EcommerceShoppingStore.Models
{
    public partial class Category
    {
        //public Category()
        //{
        //    Products = new HashSet<Product>();
        //}

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Category_Description { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
    }
}
