using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Entities.StockAggregate;

namespace Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string BgColor { get; set; }
        public double Rating { get; set; }
        public IEnumerable<PhotoDto> Photos { get; set; }
        public IEnumerable<ProductTagDto> Tags { get; set; }
        public IEnumerable<StockDto> Stocks { get; set; }
        public IEnumerable<ReviewDto> Reviews { get; set; }

        public static readonly Expression<Func<Product, ProductDto>> ProductProjection =
            product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Stocks.Select(y => y.Price).FirstOrDefault(),
                Category = product.Category.Name,
                BgColor = product.BgColor,
                Rating = product.Reviews.Count == 0
                    ? 0
                    : product.Reviews.Sum(y => y.Rating) / (double) product.Reviews.Count,
                Photos = product.Photos.AsQueryable().Select(PhotoDto.Projection),
                Tags = product.ProductTags.AsQueryable().Select(ProductTagDto.Projection),
                Stocks = product.Stocks.AsQueryable().Select(StockDto.Projection),
                Reviews = product.Reviews.AsQueryable().Select(ReviewDto.Projection)
            };
        
        public static readonly Expression<Func<Product, ProductDto>> ProductsProjection =
            product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Stocks.Select(y => y.Price).FirstOrDefault(),
                Category = product.Category.Name,
                Photos = product.Photos.AsQueryable().Select(PhotoDto.Projection),
            };
    }
}