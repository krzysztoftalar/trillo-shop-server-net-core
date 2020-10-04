using System.Collections.Generic;

namespace Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int OrdersCount { get; set; }
        public string Category { get; set; }
        public IEnumerable<PhotoDto> Photos { get; set; }
    }
}