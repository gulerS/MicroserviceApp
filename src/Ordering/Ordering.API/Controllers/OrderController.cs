using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Responses;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrderByUserNameQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }


        //Added for testing purpose
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResponse>> CheckoutOrder([FromBody]CheckoutOrderCommand command)
        {
            var result = _mediator.Send(command);
            return Ok(result.Result);
        }
    }
}
