using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.StockAggregate
{
    public class Tag
    {
        public string Id { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}