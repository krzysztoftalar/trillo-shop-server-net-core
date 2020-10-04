using System;
using System.Linq.Expressions;
using Domain.Entities.StockAggregate;

namespace Application.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public static readonly Expression<Func<Review, ReviewDto>> Projection =
            review => new ReviewDto
            {
                Id = review.Id,
                Author = review.Author.DisplayName,
                Comment = review.Comment,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt
            };
    }
}