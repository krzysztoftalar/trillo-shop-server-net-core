using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Application.Interfaces;
using Domain.Entities.BuyerAggregate;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebUI.Infrastructure
{
    public class SessionService : ISessionService
    {
        private readonly ISession _session;
        private const string CartKey = "cart";

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public string GetId()
        {
            return _session.Id;
        }

        public void AddToCart(CartProduct cartProduct)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(CartKey);

            if (!string.IsNullOrWhiteSpace(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartList.Any(x => x.StockId == cartProduct.StockId))
            {
                cartList.Find(x => x.StockId == cartProduct.StockId)!.Quantity += cartProduct.Quantity;
            }
            else
            {
                cartList.Add(cartProduct);
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString(CartKey, stringObject);
        }

        public IEnumerable<TResult> GetCart<TResult>(Expression<Func<CartProduct, TResult>> projection)
        {
            var stringObject = _session.GetString(CartKey);

            if (string.IsNullOrWhiteSpace(stringObject)) return new List<TResult>();

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            return cartList.AsQueryable().Select(projection).ToList();
        }
    }
}