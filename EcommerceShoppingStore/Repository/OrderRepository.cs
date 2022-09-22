using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceShoppingStore.Interface;
using EcommerceShoppingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceShoppingStore.Repository
{
    public class OrderRepository : IOrderRepository
    {
        EcommerceDBContext db;
        public OrderRepository(EcommerceDBContext _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            if (db != null)
            {
                return await db.Customers.ToListAsync();
            }

            return null;
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrders()
        {
            if (db != null)
            {
                return await (from o in db.Orders
                              from c in db.Customers
                              where o.CustomerId == c.CustomerId
                              select new OrderViewModel
                              {
                                  OrderId = o.OrderId,
                                  OrderDate = o.OrderDate,
                                  ShipDate = o.ShipDate,
                                  CustomerId = o.CustomerId,
                                  FullName = c.FullName,
                                  Email = c.Email,
                                  DeliveryAddress = c.DeliveryAddress
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<OrderViewModel> GetOrder(int? OrderId)
        {
            if (db != null)
            {
                return await (from o in db.Orders
                              from c in db.Customers
                              where o.OrderId == OrderId
                              select new OrderViewModel
                              {
                                  OrderId = o.OrderId,
                                  OrderDate = o.OrderDate,
                                  ShipDate = o.ShipDate,
                                  CustomerId = o.CustomerId,
                                  FullName = c.FullName,
                                  Email = c.Email,
                                  DeliveryAddress = c.DeliveryAddress
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddCustomer(Customer cust)
        {
            if (db != null)
            {
                await db.Customers.AddAsync(cust);
                await db.SaveChangesAsync();

                return cust.CustomerId;
            }

            return 0;
        }

        public async Task<int> AddOrder(Order order)
        {
            if (db != null)
            {
                await db.Orders.AddAsync(order);
                await db.SaveChangesAsync();

                return order.OrderId;
            }

            return 0;
        }

        public async Task UpdateOrder(Order order)
        {
            if (db != null)
            {
                //Delete that post
                db.Orders.Update(order);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteOrder(int? OrderId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var order = await db.Orders.FirstOrDefaultAsync(x => x.OrderId == OrderId);

                if (order != null)
                {
                    //Delete that post
                    db.Orders.Remove(order);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }
    }
}
