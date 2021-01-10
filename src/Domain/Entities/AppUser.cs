using System.Collections.Generic;
using Domain.Entities.OrderAggregate;
using Domain.Entities.StockAggregate;
using Microsoft.AspNetCore.Identity;
using Address = Domain.Entities.StockAggregate.Address;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}