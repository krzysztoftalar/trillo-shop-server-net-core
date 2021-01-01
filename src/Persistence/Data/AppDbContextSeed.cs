using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Domain.Entities.StockAggregate;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Data
{
    public static class AppDbContextSeed
    {
        public static async Task SeedData(AppDbContext context, UserManager<AppUser> userManager)
        {
            if (!context.Products.Any())
            {
                foreach (var user in GetPreconfiguredUsers())
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                await context.PaymentMethods.AddRangeAsync(GetPreconfiguredPaymentMethods());

                await context.DeliveryMethods.AddRangeAsync(GetPreconfiguredDeliveryMethods());

                await context.ProductCategories.AddRangeAsync(GetPreconfiguredCategories());
                await context.SaveChangesAsync();

                await context.Products.AddRangeAsync(GetPreconfiguredProducts());
                await context.SaveChangesAsync();

                await context.Reviews.AddRangeAsync(GetPreconfiguredReviews());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<PaymentMethod> GetPreconfiguredPaymentMethods()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod
                {
                    Name = "Direct bank transfer",
                    Description =
                        "Make your payment directly into our bank account. Please use your Order ID as the payment reference. Your order will not be shipped until the funds have cleared in our account."
                },
                new PaymentMethod
                {
                    Name = "Cash on delivery",
                    Description = "Pay with cash upon delivery."
                },
                new PaymentMethod
                {
                    Name = "Credit and debit cards",
                    Description =
                        "Pay with Visa, Mastercard, American Express, Discover and Diners, China UnionPay, JCB, Cartes Bancaires, Interac."
                }
            };
        }

        private static IEnumerable<DeliveryMethod> GetPreconfiguredDeliveryMethods()
        {
            return new List<DeliveryMethod>
            {
                new DeliveryMethod
                {
                    Name = "Free shipping",
                    DeliveryTime = "3 Days",
                    Description = "Free shipping",
                    Price = 0
                },
                new DeliveryMethod
                {
                    Name = "Flat rate",
                    DeliveryTime = "2 Days",
                    Description = "Flat rate",
                    Price = 15
                },
                new DeliveryMethod
                {
                    Name = "Local pickup",
                    DeliveryTime = "1 Day",
                    Description = "Local pickup",
                    Price = 8
                }
            };
        }

        private static IEnumerable<AppUser> GetPreconfiguredUsers()
        {
            return new List<AppUser>
            {
                new AppUser
                {
                    Id = "a",
                    UserName = "bob",
                    Email = "bob@test.com",
                    EmailConfirmed = true,
                },
                new AppUser
                {
                    Id = "b",
                    UserName = "agata",
                    Email = "agata@test.com",
                    EmailConfirmed = true,
                },
                new AppUser
                {
                    Id = "c",
                    UserName = "tom",
                    Email = "tom@test.com",
                    EmailConfirmed = true,
                },
                new AppUser
                {
                    Id = "d",
                    UserName = "jack",
                    Email = "jack@test.com",
                    EmailConfirmed = true,
                },
            };
        }

        private static IEnumerable<Review> GetPreconfiguredReviews()
        {
            return new List<Review>
            {
                new Review
                {
                    Comment =
                        "Sit hoc ultimum bonorum, quod nunc a me defenditur; Sed ad haec, nisi molestum est, habeo quae velim. Sed venio ad inconstantiae crimen, ne saepius dicas me aberrare.",
                    Rating = 4,
                    AuthorId = "a",
                    ProductId = 1
                },
                new Review
                {
                    Comment =
                        "Sit hoc ultimum bonorum, quod nunc a me defenditur; Sed ad haec, nisi molestum est, habeo quae velim. Sed venio ad inconstantiae crimen, ne saepius dicas me aberrare.",
                    Rating = 3,
                    AuthorId = "b",
                    ProductId = 1
                },
                new Review
                {
                    Comment =
                        "Sit hoc ultimum bonorum, quod nunc a me defenditur; Sed ad haec, nisi molestum est, habeo quae velim. Sed venio ad inconstantiae crimen, ne saepius dicas me aberrare.",
                    Rating = 1,
                    AuthorId = "c",
                    ProductId = 1
                },
                new Review
                {
                    Comment =
                        "Sit hoc ultimum bonorum, quod nunc a me defenditur; Sed ad haec, nisi molestum est, habeo quae velim. Sed venio ad inconstantiae crimen, ne saepius dicas me aberrare.",
                    Rating = 1,
                    AuthorId = "d",
                    ProductId = 1
                }
            };
        }

        private static IEnumerable<ProductCategory> GetPreconfiguredCategories()
        {
            return new List<ProductCategory>
            {
                new ProductCategory { Name = "Accessories" },
                new ProductCategory { Name = "Jacket" },
                new ProductCategory { Name = "Trousers" },
                new ProductCategory { Name = "Clothes" },
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Bond Drawstring Bucket Bag",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod",
                    IsFeatured = true,
                    BgColor = "#f3eeee",
                    ProductType = ProductType.Woman,
                    CategoryId = 1,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            Quantity = 100,
                            Price = 120,
                            ProductId = 1,
                        },
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = "a",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601538152/1-4_p4gevi.webp",
                            IsMain = true,
                        },
                        new Photo
                        {
                            Id = "b",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601649245/2-4_atjdur.webp",
                            IsMain = false,
                        },
                        new Photo
                        {
                            Id = "c",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601649246/3-4_bzqruy.webp",
                            IsMain = false,
                        },
                        new Photo
                        {
                            Id = "d",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601649246/4-4_spacbn.webp",
                            IsMain = false,
                        },
                    }
                },
                new Product
                {
                    Name = "Bomber Jacket",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod",
                    IsFeatured = true,
                    BgColor = "#f2ede9",
                    ProductType = ProductType.Men,
                    CategoryId = 2,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            Quantity = 100,
                            Price = 390,
                            ProductSize = "XL",
                            ProductColor = "#f2ede9",
                            ProductId = 2,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 390,
                            ProductSize = "L",
                            ProductColor = "#f2ede9",
                            ProductId = 2,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 390,
                            ProductSize = "M",
                            ProductColor = "#f2ede9",
                            ProductId = 2,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 390,
                            ProductSize = "L",
                            ProductColor = "#161619",
                            ProductId = 2,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 390,
                            ProductSize = "XL",
                            ProductColor = "#ececf2",
                            ProductId = 2,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 390,
                            ProductSize = "XXL",
                            ProductColor = "#ececf2",
                            ProductId = 2,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 390,
                            ProductSize = "M",
                            ProductColor = "#ececf2",
                            ProductId = 2,
                        },
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = "e",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601538152/1-2_bx5j40.webp",
                            IsMain = true,
                            ProductId = 2,
                        },
                        new Photo
                        {
                            Id = "f",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601650049/2-2_scvfqv.webp",
                            IsMain = false,
                            ProductId = 2,
                        },
                        new Photo
                        {
                            Id = "g",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601650049/3-2_u2d9en.webp",
                            IsMain = false,
                            ProductId = 2,
                        }
                    },
                },
                new Product
                {
                    Name = "Eye Mesh Boat Shoes",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod",
                    IsFeatured = true,
                    BgColor = "#edf6ff",
                    ProductType = ProductType.Woman,
                    CategoryId = 1,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            Quantity = 100,
                            Price = 420,
                            ProductSize = "40",
                            ProductColor = "#ececf2",
                            ProductId = 3,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 420,
                            ProductSize = "41",
                            ProductColor = "#ececf2",
                            ProductId = 3,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 420,
                            ProductSize = "42",
                            ProductColor = "#ececf2",
                            ProductId = 3,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 420,
                            ProductSize = "43",
                            ProductColor = "#eaa69a",
                            ProductId = 3,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 420,
                            ProductSize = "44",
                            ProductColor = "#eaa69a",
                            ProductId = 3,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 420,
                            ProductSize = "45",
                            ProductColor = "#eaa69a",
                            ProductId = 3,
                        },
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = "h",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601538152/1-7_luhetd.webp",
                            IsMain = true,
                            ProductId = 3,
                        },
                        new Photo
                        {
                            Id = "i",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601650920/2-7_urvdsr.webp",
                            IsMain = false,
                            ProductId = 3,
                        },
                        new Photo
                        {
                            Id = "j",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601650919/3-7_y4y8y7.webp",
                            IsMain = false,
                            ProductId = 3,
                        },
                        new Photo
                        {
                            Id = "k",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601650975/4-7_rsqf9z.webp",
                            IsMain = false,
                            ProductId = 3,
                        },
                    },
                },
                new Product
                {
                    Name = "VIA Backpack",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod",
                    IsFeatured = true,
                    BgColor = "#eef4e8",
                    ProductType = ProductType.Men,
                    CategoryId = 1,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            Quantity = 100,
                            Price = 155,
                            ProductColor = "#eef4e8",
                            ProductId = 4,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 155,
                            ProductColor = "#161619",
                            ProductId = 4,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 155,
                            ProductColor = "#eaa69a",
                            ProductId = 4,
                        },
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = "l",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601538152/1-1_hne1ht.webp",
                            IsMain = true,
                            ProductId = 4,
                        },
                        new Photo
                        {
                            Id = ",",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601651360/2-1_ouocyw.webp",
                            IsMain = false,
                            ProductId = 4,
                        },
                        new Photo
                        {
                            Id = "m",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601651360/4-1_ogmwym.webp",
                            IsMain = false,
                            ProductId = 4,
                        },
                        new Photo
                        {
                            Id = "o",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601651360/3-1_faun38.webp",
                            IsMain = false,
                            ProductId = 4,
                        },
                    },
                },
                new Product
                {
                    Name = "Soho Print Shirt",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod",
                    IsFeatured = true,
                    BgColor = "#f2f3f7",
                    ProductType = ProductType.Men,
                    CategoryId = 1,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            Quantity = 100,
                            Price = 155,
                            ProductSize = "XL",
                            ProductColor = "#f2f3f7",
                            ProductId = 5,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 155,
                            ProductSize = "L",
                            ProductColor = "#f2f3f7",
                            ProductId = 5,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 155,
                            ProductSize = "M",
                            ProductColor = "#f2f3f7",
                            ProductId = 5,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 155,
                            ProductSize = "S",
                            ProductColor = "#f2f3f7",
                            ProductId = 5,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 155,
                            ProductSize = "XL",
                            ProductColor = "#606f70",
                            ProductId = 5,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 155,
                            ProductSize = "M",
                            ProductColor = "#606f70",
                            ProductId = 5,
                        },
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = "p",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601538153/1-5_vwcqht.webp",
                            IsMain = true,
                            ProductId = 5,
                        },
                        new Photo
                        {
                            Id = "q",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601651818/2-5_ecaywi.webp",
                            IsMain = false,
                            ProductId = 5,
                        },
                        new Photo
                        {
                            Id = "r",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601651818/3-5_ilfrbi.webp",
                            IsMain = false,
                            ProductId = 5,
                        },
                        new Photo
                        {
                            Id = "s",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601651818/4-5_vqcsaf.webp",
                            IsMain = false,
                            ProductId = 5,
                        },
                    },
                },
                new Product
                {
                    Name = "Slim Trapered Rip Jeans",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod",
                    IsFeatured = true,
                    BgColor = "#eef2f5",
                    ProductType = ProductType.Men,
                    CategoryId = 3,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            Quantity = 100,
                            Price = 169,
                            ProductSize = "40",
                            ProductColor = "#eef2f5",
                            ProductId = 6,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 169,
                            ProductSize = "41",
                            ProductColor = "#eef2f5",
                            ProductId = 6,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 169,
                            ProductSize = "42",
                            ProductColor = "#eef2f5",
                            ProductId = 6,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 169,
                            ProductSize = "43",
                            ProductColor = "#eef2f5",
                            ProductId = 6,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 169,
                            ProductSize = "44",
                            ProductColor = "#eef2f5",
                            ProductId = 6,
                        },
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = "t",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601538152/1-6_hsmaah.webp",
                            IsMain = true,
                            ProductId = 6,
                        },
                        new Photo
                        {
                            Id = "u",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601652210/2-6_civ5sz.webp",
                            IsMain = false,
                            ProductId = 6,
                        },
                        new Photo
                        {
                            Id = "w",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601652210/3-6_bmemk8.webp",
                            IsMain = false,
                            ProductId = 6,
                        },
                    },
                },
                new Product
                {
                    Name = "Bracker Slide Sneakers",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod",
                    IsFeatured = true,
                    BgColor = "#f3f2f7",
                    ProductType = ProductType.Woman,
                    CategoryId = 1,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            Quantity = 100,
                            Price = 79,
                            ProductSize = "37",
                            ProductColor = "#f3f2f7",
                            ProductId = 7,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 79,
                            ProductSize = "38",
                            ProductColor = "#f3f2f7",
                            ProductId = 7,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 79,
                            ProductSize = "39",
                            ProductColor = "#f3f2f7",
                            ProductId = 7,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 79,
                            ProductSize = "40",
                            ProductColor = "#f3f2f7",
                            ProductId = 7,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 79,
                            ProductSize = "40",
                            ProductColor = "#eef2f5",
                            ProductId = 7,
                        },
                        new Stock
                        {
                            Quantity = 100,
                            Price = 79,
                            ProductSize = "37",
                            ProductColor = "#eef2f5",
                            ProductId = 7,
                        },
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = "x",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601538152/1-3_nfv58f.webp",
                            IsMain = true,
                            ProductId = 7,
                        },
                        new Photo
                        {
                            Id = "y",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601652484/2-3_oimr3o.webp",
                            IsMain = false,
                            ProductId = 7,
                        },
                        new Photo
                        {
                            Id = "z",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601652484/3-3_dkyc9v.webp",
                            IsMain = false,
                            ProductId = 7,
                        },
                    },
                },
                new Product
                {
                    Name = "Hulton Overalls",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod",
                    IsPromo = true,
                    BgColor = "#e6f0f8",
                    ProductType = ProductType.Woman,
                    CategoryId = 4,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            Quantity = 100,
                            Price = 315,
                            ProductSize = "38",
                            ProductColor = "#e6f0f8",
                            ProductId = 8,
                        },
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = "aa",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601712800/4-8_y4kbgk.webp",
                            IsMain = true,
                            ProductId = 8,
                        },
                        new Photo
                        {
                            Id = "ab",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601712750/1-8_iraixk.webp",
                            IsMain = false,
                            ProductId = 8,
                        },
                        new Photo
                        {
                            Id = "ac",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601712750/2-8_kwx7x1.webp",
                            IsMain = false,
                            ProductId = 8,
                        },
                        new Photo
                        {
                            Id = "ad",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601712750/3-8_il7cvy.webp",
                            IsMain = false,
                            ProductId = 8,
                        },
                    }
                },
                new Product
                {
                    Name = "Tennis Shoes",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod",
                    IsPromo = true,
                    BgColor = "#f3f2f7",
                    ProductType = ProductType.Woman,
                    CategoryId = 1,
                    Stocks = new List<Stock>
                    {
                        new Stock
                        {
                            Quantity = 100,
                            Price = 240,
                            ProductSize = "38",
                            ProductColor = "#f3f2f7",
                            ProductId = 9,
                        },
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Id = "ae",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601713195/4-9_rxmlwl.webp",
                            IsMain = true,
                            ProductId = 9,
                        },
                        new Photo
                        {
                            Id = "af",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601713195/1-9_i76gh4.webp",
                            IsMain = false,
                            ProductId = 9,
                        },
                        new Photo
                        {
                            Id = "ag",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601713195/2-9_ucbhfj.webp",
                            IsMain = false,
                            ProductId = 9,
                        },
                        new Photo
                        {
                            Id = "ah",
                            Url = "https://res.cloudinary.com/drzyvvdq0/image/upload/v1601713195/3-9_leoxn9.webp",
                            IsMain = false,
                            ProductId = 9,
                        },
                    }
                },
            };
        }
    }
}