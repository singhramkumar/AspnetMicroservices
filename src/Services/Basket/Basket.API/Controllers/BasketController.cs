using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcServicesClient _discountGrpcServicesClient;
        public BasketController(IBasketRepository repository, DiscountGrpcServicesClient discountGrpcServicesClient)
        {
            _repository = repository;
            _discountGrpcServicesClient = discountGrpcServicesClient;
        }

        [HttpGet("{userName}", Name ="GetBasket")]
        [ProducesResponseType( typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //Communicate with Discount.Grpc
            // Calculate latest prices of products into the shopping cart.
            // Consume Discount Gprc

            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcServicesClient.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            
            return Ok( await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name ="DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }
    }
}
