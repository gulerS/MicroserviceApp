using WebPage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebPage.ApiCollection.Interfaces
{
    public interface IOrderApi
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
