namespace ServerEternalRelics.Migrations
{
    using ServerEternalRelics.DAL;
using ServerEternalRelics.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ServerEternalRelics.DAL.WebshopContext>
    {

        private WebshopContext db = new WebshopContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ServerEternalRelics.DAL.WebshopContext context)
        {

            /* Create some products/categories/offers */
            var products = new List<Product>{
                new Product{
                    ProductID=1,
                    Name="Iron sword",
                    Description="Just a rusty old sword.",
                    Image="~/Images/rustysword.png",
                    Price=15.50m,
                    Categories=new List<Category>()
                },
                new Product{
                    ProductID=2,
                    Name="Iron axe",
                    Description="Just a rusty old axe.",
                    Image="~/Images/rustyaxe.png",
                    Price=25.50m,
                    Categories=new List<Category>()
                },
                new Product{
                    ProductID=3,
                    Name="Chainmail",
                    Description="A knights armor.",
                    Image="~/Images/chainmail.png",
                    Price=45.50m,
                    Categories=new List<Category>()
                },
                new Product{
                    ProductID=4,
                    Name="Wizard hat",
                    Description="A hat suitable for an old and wise man.",
                    Image="~/Images/wizardhat.png",
                    Price=15.50m,
                    Categories=new List<Category>()
                },
                new Product{
                    ProductID=5,
                    Name="Florals Frostblade",
                    Description="Wielded by the frost mage Floral, the bringer of eternal winter.",
                    Image="~/Images/florals.png",
                    Price=245.50m,
                    Categories=new List<Category>()
                },
                new Product{
                    ProductID=6,
                    Name="Magmus",
                    Description="Forged in the magma of the black volcano. Magmus turned many battlefields into a sea of flame.",
                    Image="~/Images/magmus.png",
                    Price=275.00m,
                    Categories=new List<Category>()
                },
                new Product{
                    ProductID=7,
                    Name="Master Sword",
                    Description="The sword of the legendary warrior Link.",
                    Image="~/Images/mastersword.png",
                    Price=575.50m,
                    Categories=new List<Category>()
                },
                new Product{
                    ProductID=8,
                    Name="Battleplate of the Immortal",
                    Description="An armor so strong, those who wield it were deemed immortal.",
                    Image="~/Images/battleplateoftheimmortal.png",
                    Price=635.00m,
                    Categories=new List<Category>()
                }
            };

            products.ForEach(p => db.Product.AddOrUpdate(p));
            db.SaveChanges();

            var categories = new List<Category>{
                new Category{
                    CategoryID = 1,
                    Name = "Weapons",
                    Image = "~/Images/weapons.png",
                    Description = "A collection of weapons.",
                    Products = new List<Product>()
                },
                new Category{
                    CategoryID = 2,
                    Name = "Armor",
                    Image = "~/Images/armor.png",
                    Description = "A collection of armor.",
                    Products = new List<Product>()
                },
                new Category{
                    CategoryID = 3,
                    Name = "Epic",
                    Image = "~/Images/epic.png",
                    Description = "A collection of rare and valuable items.",
                    Products = new List<Product>()
                },
                new Category{
                    CategoryID = 4,
                    Name = "Legendary",
                    Image = "~/Images/legendary.png",
                    Description = "A collection of items with historic value.",
                    Products = new List<Product>()
                },
                new Category{
                    CategoryID = 5,
                    Name = "New",
                    Image = "~/Images/new.png",
                    Description = "A collection of new items.",
                    Products = new List<Product>()
                }
            };

            categories.ForEach(c => db.Category.AddOrUpdate(c));
            db.SaveChanges();

            var offers = new List<Offer>(){
                new Offer{
                    OfferID = 1,
                    OfferPrice = 10.00m,
                    DateFrom = new DateTime(2016,4,20),
                    DateTill = new DateTime(2016,5,20),
                    OfferText = "Get your swords now!",
                    ProductID = 1
                },
                new Offer{
                    OfferID = 2,
                    OfferPrice = 270.00m,
                    DateFrom = new DateTime(2016,4,20),
                    DateTill = new DateTime(2016,5,20),
                    OfferText = "Feel like setting the world aflame? Get your hands on this sword right now.",
                    ProductID = 6
                }
            };

            offers.ForEach(o => db.Offer.AddOrUpdate(o));
            db.SaveChanges();

            AddProductToCategory(db, "Iron Sword", "Weapons");
            AddProductToCategory(db, "Iron Axe", "Weapons");
            AddProductToCategory(db, "Wizard hat", "Armor");
            AddProductToCategory(db, "Chainmail", "Armor");

            AddProductToCategory(db, "Florals Frostblade", "Weapons");
            AddProductToCategory(db, "Florals Frostblade", "Epic");
            AddProductToCategory(db, "Magmus", "Weapons");
            AddProductToCategory(db, "Magmus", "Epic");

            AddProductToCategory(db, "Master Sword", "Weapons");
            AddProductToCategory(db, "Master Sword", "Legendary");
            AddProductToCategory(db, "Battleplate of the Immortal", "Legendary");
            AddProductToCategory(db, "Battleplate of the Immortal", "Armor");

            db.SaveChanges();
        }

        private void AddProductToCategory(WebshopContext db, string product, string category)
        {
            var cat = db.Category.SingleOrDefault(c => c.Name == category);
            var pro = cat.Products.SingleOrDefault(p => p.Name == product);
            if (pro == null)
                cat.Products.Add(db.Product.Single(p => p.Name == product));
        }
    }
}
