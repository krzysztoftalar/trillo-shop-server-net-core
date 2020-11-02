using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.OrderAggregate;
using Domain.Enums;

namespace Domain.Entities.StockAggregate
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsPromo { get; set; }
        public string BgColor { get; set; }
        public ProductType ProductType { get; set; }

        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; }
        public virtual ICollection<RelatedProduct> RelatedProducts { get; set; }

    }
}