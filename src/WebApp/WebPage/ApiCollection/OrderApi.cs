using WebPage.ApiCollection.Infrastructure;
using WebPage.ApiCollection.Interfaces;
using WebPage.Models;
using WebPage.Settings;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebPage.ApiCollection
{
    public class OrderApi : BaseHttpClientWithFactory, IOrderApi
    {
        //https://www.danylkoweb.com/downloads/RefactorHttpClientWithAspNetCore.pdf
        private readonly IApiSettings _settings;

        public OrderApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                           .SetPath(_settings.OrderPath)
                           .AddQueryString("username", userName)
                           .HttpMethod(HttpMethod.Get)
                           .GetHttpMessage();

            return await SendRequest<IEnumerable<OrderResponseModel>>(message);
        }
    }
}
