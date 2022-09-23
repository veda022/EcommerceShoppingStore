using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceShoppingStore.Interface;
using EcommerceShoppingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceShoppingStore.Repository
{
    public class ProductRepository : IProductRepository
    {
        EcommerceDBContext db;
        public ProductRepository(EcommerceDBContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            if (db != null)
            {
                return await db.Categories.ToListAsync();
            }

            return null;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            if (db != null)
            {
                return await db.Products.ToListAsync();
                //return await (from p in db.Products
                //              from c in db.Categories
                //              where p.ProductsId <= c.CategoryId 
                //              select new ProductViewModel
                //              {
                //                  ProductsId = p.ProductsId,
                //                  ModelName = p.ModelName,
                //                  ModelNumber = p.ModelNumber,
                //                  UnitCost = p.UnitCost,
                //                  Product_Description = p.Product_Description,
                //                  CategoryId = p.CategoryId,
                //              }).ToListAsync();
            }

            return null;
        }

        public async Task<ProductViewModel> GetProduct(int? ProductId)
        {
            if (db != null)
            {
                return await (from p in db.Products
                              from c in db.Categories
                              where p.ProductsId == ProductId
                              select new ProductViewModel
                              {
                                  ProductsId = p.ProductsId,
                                  ModelName = p.ModelName,
                                  ModelNumber = p.ModelNumber,
                                  UnitCost = p.UnitCost,
                                  Product_Description = p.Product_Description,
                                  CategoryId = p.CategoryId,
                                  CategoryName = c.CategoryName,
                                  Category_Description = c.Category_Description
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddCategory(Category cat)
        {
            if (db != null)
            {
                await db.Categories.AddAsync(cat);
                await db.SaveChangesAsync();

                return cat.CategoryId;
            }

            return 0;
        }

        public async Task<int> AddProduct(Product prod)
        {
            if (db != null)
            {
                await db.Products.AddAsync(prod);
                await db.SaveChangesAsync();

                return prod.ProductsId;
            }

            return 0;
        }

        public async Task UpdateProduct(Product prod)
        {
            if (db != null)
            {
                //Delete that post
                db.Products.Update(prod);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteProduct(int? ProductId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var prod = await db.Products.FirstOrDefaultAsync(x => x.ProductsId == ProductId);

                if (prod != null)
                {
                    //Delete that post
                    db.Products.Remove(prod);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        //public async Task<int> DeleteCategory(int? CategoryId)
        //{
        //    int result = 0;

        //    if (db != null)
        //    {
        //        //Find the post for specific post id
        //        var cat = await db.Categories.FirstOrDefaultAsync(x => x.CategoryId == CategoryId);

        //        if (cat != null)
        //        {
        //            //Delete that post
        //            db.Categories.Remove(cat);

        //            //Commit the transaction
        //            result = await db.SaveChangesAsync();
        //        }
        //        return result;
        //    }

        //    return result;
        //}

    }
}
