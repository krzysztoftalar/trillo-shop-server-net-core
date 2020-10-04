using System;
using System.Linq.Expressions;
using Domain.Entities.StockAggregate;

namespace Application.Dtos
{
    public class PhotoDto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }

        public static readonly Expression<Func<Photo, PhotoDto>> Projection =
            photo => new PhotoDto
            {
                Id = photo.Id,
                Url = photo.Url,
                IsMain = photo.IsMain,
            };
    }
}