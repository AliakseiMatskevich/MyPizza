using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPizza.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.Infrastructure.Data
{
    public class PizzaContextSeed
    {
        public static async Task SeedAsync(PizzaContext context, ILogger logger)
        {
            if (!await context.Categories.AnyAsync())
            {
                logger.LogInformation("Database is empty");
                var categories = GetInitialCategories().ToList();
                await context.Categories.AddRangeAsync(categories);
                

                var weightTypes = GetInitialWeightTypes(categories).ToList();
                await context.WeightTypes.AddRangeAsync(weightTypes);

                var productTypes = GetInitialProductTypes(categories).ToList();
                await context.ProductTypes.AddRangeAsync(productTypes);

                var products = GetInitialProducts(productTypes, weightTypes).ToList();
                await context.Products.AddRangeAsync(products);

                await context.SaveChangesAsync();

                logger.LogInformation("Seed database complete");
            }
        }

        private static IEnumerable<Category> GetInitialCategories()
        {
            return new List<Category>
            {
                new Category { Id = new Guid(), Name = "Pizza", Measure = "g" },
                new Category { Id = new Guid(), Name = "Salad", Measure = "g"  },
                new Category { Id = new Guid(), Name = "Drink", Measure = "ml" },
            };
        }

        private static IEnumerable<WeightType> GetInitialWeightTypes(List<Category> categories)
        {
            return new List<WeightType>
            {
                new WeightType { Id = new Guid(), Name = "Small", CategoryId = categories[0].Id },
                new WeightType { Id = new Guid(), Name = "Medium", CategoryId = categories[0].Id  },
                new WeightType { Id = new Guid(), Name = "Big", CategoryId = categories[0].Id },

                new WeightType { Id = new Guid(), Name = "Small", CategoryId = categories[2].Id },
                new WeightType { Id = new Guid(), Name = "Big", CategoryId = categories[2].Id },
            };
        }

        private static IEnumerable<ProductType> GetInitialProductTypes(List<Category> categories)
        {
            return new List<ProductType>
            {
                new ProductType { Id = new Guid(), Name = "Сarbonara", CategoryId = categories[0].Id, 
                    Description = "Onion, bacon, fresh cream, ham, mushrooms, mozzarella cheese"},
                new ProductType { Id = new Guid(), Name = "4 seasons", CategoryId = categories[0].Id,
                    Description = "Ranch sauce, mushrooms, tomatoes, pepperoni, feta, pineapple, mozzarella cheese, shrimps"},
                new ProductType { Id = new Guid(), Name = "Meatbollo", CategoryId = categories[0].Id,
                    Description = "Burger sauce, beef meatballs, cucumber, mozzarella cheese, chicken, onion, sweet pepper"},
                new ProductType { Id = new Guid(), Name = "Bolognese", CategoryId = categories[0].Id,
                    Description = "Ham, beef meatballs, tomatoes, domino's tomato sauce, onion, mushrooms, mozzarella cheese"},
                new ProductType { Id = new Guid(), Name = "Meat&Cheese", CategoryId = categories[0].Id,
                    Description = "Bacon, chicken, mozzarella cheese, tomatoes, ham, sweet peppers, cheese sauce"},
                new ProductType { Id = new Guid(), Name = "Munich", CategoryId = categories[0].Id,
                    Description = "Bavarian sausages, ham, tomatoes, mozzarella cheese, mustard, barbecue sauce, Munich sausages"},
                new ProductType { Id = new Guid(), Name = "Beef Burger", CategoryId = categories[0].Id,
                    Description = "Champignons, veal, mozzarella cheese, burger sauce, onion, cucumbers"},
                new ProductType { Id = new Guid(), Name = "Cheddaroni", CategoryId = categories[0].Id,
                    Description = "Pepperoni, mozzarella cheese, cheddar, ham, cream fresh"},

                new ProductType { Id = new Guid(), Name = "Greek salad", CategoryId = categories[1].Id,
                    Description = "Olive oil, olives, cherry tomatoes, feta"},
                new ProductType { Id = new Guid(), Name = "Caesar salad", CategoryId = categories[1].Id,
                    Description = "Курица, черри, пармезан, соус цезарь"},

                new ProductType { Id = new Guid(), Name = "Coca Cola Classic", CategoryId = categories[2].Id},
                new ProductType { Id = new Guid(), Name = "Coca-Cola without sugar", CategoryId = categories[2].Id},
                new ProductType { Id = new Guid(), Name = "Coca-Cola Vanilla", CategoryId = categories[2].Id},
                new ProductType { Id = new Guid(), Name = "Fanta Orange", CategoryId = categories[2].Id},
                new ProductType { Id = new Guid(), Name = "Fanta Lemon", CategoryId = categories[2].Id},
                new ProductType { Id = new Guid(), Name = "Sprite", CategoryId = categories[2].Id},
                new ProductType { Id = new Guid(), Name = "Bonaqua Still", CategoryId = categories[2].Id},
                new ProductType { Id = new Guid(), Name = "Bonaqua Medium carbonated", CategoryId = categories[2].Id},
            };
        }

        private static IEnumerable<Product> GetInitialProducts(List<ProductType> productTypes, List<WeightType> weightTypes)
        {
            return new List<Product>
            {
                #region Pizza
                new Product { Id = new Guid(), ProductTypeId = productTypes[0].Id, WeightTypeId = weightTypes[0].Id, Price = 18.49M, Weight = 360 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[0].Id, WeightTypeId = weightTypes[1].Id, Price = 25.99M, Weight = 545 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[0].Id, WeightTypeId = weightTypes[2].Id, Price = 30.49M, Weight = 805 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[1].Id, WeightTypeId = weightTypes[0].Id, Price = 21.49M, Weight = 380 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[1].Id, WeightTypeId = weightTypes[1].Id, Price = 28.49M, Weight = 600 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[1].Id, WeightTypeId = weightTypes[2].Id, Price = 32.49M, Weight = 815 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[2].Id, WeightTypeId = weightTypes[0].Id, Price = 18.99M, Weight = 400 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[2].Id, WeightTypeId = weightTypes[1].Id, Price = 25.99M, Weight = 605 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[2].Id, WeightTypeId = weightTypes[2].Id, Price = 30.49M, Weight = 930 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[3].Id, WeightTypeId = weightTypes[0].Id, Price = 18.49M, Weight = 475 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[3].Id, WeightTypeId = weightTypes[1].Id, Price = 25.99M, Weight = 680 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[3].Id, WeightTypeId = weightTypes[2].Id, Price = 30.49M, Weight = 1060 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[4].Id, WeightTypeId = weightTypes[0].Id, Price = 20.49M, Weight = 405 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[4].Id, WeightTypeId = weightTypes[1].Id, Price = 28.49M, Weight = 585 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[4].Id, WeightTypeId = weightTypes[2].Id, Price = 32.49M, Weight = 890 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[5].Id, WeightTypeId = weightTypes[0].Id, Price = 22.49M, Weight = 405 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[5].Id, WeightTypeId = weightTypes[1].Id, Price = 30.49M, Weight = 665 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[5].Id, WeightTypeId = weightTypes[2].Id, Price = 33.49M, Weight = 855 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[6].Id, WeightTypeId = weightTypes[0].Id, Price = 18.49M, Weight = 405 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[6].Id, WeightTypeId = weightTypes[1].Id, Price = 25.49M, Weight = 585 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[6].Id, WeightTypeId = weightTypes[2].Id, Price = 30.49M, Weight = 890 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[7].Id, WeightTypeId = weightTypes[0].Id, Price = 18.49M, Weight = 385 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[7].Id, WeightTypeId = weightTypes[1].Id, Price = 25.49M, Weight = 580 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[7].Id, WeightTypeId = weightTypes[2].Id, Price = 30.49M, Weight = 865 },
                #endregion

                #region Salad
                new Product { Id = new Guid(), ProductTypeId = productTypes[8].Id, Price = 10.99M, Weight = 180 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[9].Id, Price = 10.99M, Weight = 200 },
                #endregion

                #region Drink
                new Product { Id = new Guid(), ProductTypeId = productTypes[10].Id, WeightTypeId = weightTypes[3].Id, Price = 2.99M, Weight = 500 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[10].Id, WeightTypeId = weightTypes[4].Id, Price = 3.49M, Weight = 1000 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[11].Id, WeightTypeId = weightTypes[3].Id, Price = 2.99M, Weight = 500 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[11].Id, WeightTypeId = weightTypes[4].Id, Price = 3.49M, Weight = 1000 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[12].Id, WeightTypeId = weightTypes[3].Id, Price = 2.99M, Weight = 500 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[12].Id, WeightTypeId = weightTypes[4].Id, Price = 3.49M, Weight = 1000 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[13].Id, WeightTypeId = weightTypes[3].Id, Price = 2.99M, Weight = 500 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[13].Id, WeightTypeId = weightTypes[4].Id, Price = 3.49M, Weight = 1000 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[14].Id, WeightTypeId = weightTypes[3].Id, Price = 2.99M, Weight = 500 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[14].Id, WeightTypeId = weightTypes[4].Id, Price = 3.49M, Weight = 1000 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[15].Id, WeightTypeId = weightTypes[3].Id, Price = 2.99M, Weight = 500 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[15].Id, WeightTypeId = weightTypes[4].Id, Price = 3.49M, Weight = 1000 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[16].Id, WeightTypeId = weightTypes[3].Id, Price = 1.49M, Weight = 500 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[16].Id, WeightTypeId = weightTypes[4].Id, Price = 2.49M, Weight = 1000 },

                new Product { Id = new Guid(), ProductTypeId = productTypes[17].Id, WeightTypeId = weightTypes[3].Id, Price = 1.49M, Weight = 500 },
                new Product { Id = new Guid(), ProductTypeId = productTypes[17].Id, WeightTypeId = weightTypes[4].Id, Price = 2.49M, Weight = 1000 },
                #endregion
            };
        }
    }
}
