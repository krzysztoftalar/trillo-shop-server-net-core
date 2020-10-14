using System;
using System.Linq.Expressions;
using Domain.Entities.BuyerAggregate;

namespace Application.Dtos
{
    public class CartProductDto
    {
        public int StockId { get; set; }
        public string ProductName { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PhotoUrl { get; set; }

        public static readonly Expression<Func<CartProduct, CartProductDto>> Projection =
            cart => new CartProductDto
            {
                StockId = cart.StockId,
                ProductName = cart.ProductName,
                ProductSize = cart.ProductSize,
                ProductColor = cart.ProductColor,
                Price = cart.Price,
                Quantity = cart.Quantity,
                PhotoUrl = cart.PhotoUrl,
            };
    }
}