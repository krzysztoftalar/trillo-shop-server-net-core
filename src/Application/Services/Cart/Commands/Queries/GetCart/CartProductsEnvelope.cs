using System.Collections.Generic;
using Application.Dtos;

namespace Application.Services.Cart.Commands.Queries.GetCart
{
    public class CartProductsEnvelope
    {
        public IEnumerable<CartProductDto> CartProducts { get; set; }
        public decimal TotalValue { get; set; }
        public int TotalQty { get; set; }
    }
}