using System;
using System.Linq.Expressions;
using Domain.Entities.StockAggregate;

namespace Application.Dtos
{
    public class ProductTagDto
    {
        public string TagId { get; set; }

        public static readonly Expression<Func<ProductTag, ProductTagDto>> Projection =
            tag => new ProductTagDto { TagId = tag.TagId };
    }
}