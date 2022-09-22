using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShoppingStore.Models
{
    public class ProductViewModel
    {
        public int ProductsId { get; set; }
        public string ModelNumber { get; set; }
        public string ModelName { get; set; }
        public long? UnitCost { get; set; }
        public string Product_Description { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Category_Description { get; set; }
    }
}
