using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Entities.BuyerAggregate;

namespace Application.Interfaces
{
    public interface ISessionService
    {
        string GetId();
        void AddToCart(CartProduct product);
        IEnumerable<TResult> GetCart<TResult>(Expression<Func<CartProduct, TResult>> projection);
    }
}