using System;

namespace Domain.Entities.StockAggregate
{
    public class StockCostHistory
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}