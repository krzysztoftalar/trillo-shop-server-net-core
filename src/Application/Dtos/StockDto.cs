using System;
using System.Linq.Expressions;
using Domain.Entities.StockAggregate;

namespace Application.Dtos
{
    public class StockDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }

        public static readonly Expression<Func<Stock, StockDto>> Projection =
            stock => new StockDto
            {
                Id = stock.Id,
                Quantity = stock.Quantity,
                Price = stock.Price,
                ProductSize = stock.ProductSize,
                ProductColor = stock.ProductColor,
            };
    }
}