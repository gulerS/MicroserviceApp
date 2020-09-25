using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                orderContext.Database.Migrate();

                if (!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreconfiguredOrders());
                    await orderContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 3)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(orderContext, loggerFactory, retryForAvailability);
                }
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() { UserName = "ahmet", FirstName = "Ahmet", LastName = "Demir", EmailAddress = "ahmet@demir.com", AddressLine = "Maslak", TotalPrice = 5239 },
                new Order() { UserName = "selim", FirstName = "Selim", LastName = "Guler", EmailAddress ="selim@guler.com", AddressLine = "Şişli", TotalPrice = 3486 }
            };
        }
    }
}
