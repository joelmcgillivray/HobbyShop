using Microsoft.EntityFrameworkCore;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This provides the context for the db for all classes minus the identity related classes
 * This sets up with EF Core to ensure all relations are managed, as well as how they're all built in the db. 
 * It also on first load will inject seed data to all the related classes that I've given seed data to
 */

namespace hobbyshop.Data
{
    /// <summary>
    /// Data context that holds all the necessary set up for the db
    /// All the classes, connection strings and original seed data
    /// </summary>
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Uses the default connection string to the SQL Server to run migrations, and update database
        /// </summary>
        /// <param name="optionsBuilder">Pre scaffolded items</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        // Setting up the data context with the required classes 

        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Condition> Condition { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<TextData> TextDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Status>().ToTable("Status");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Condition>().ToTable("Condition");
            modelBuilder.Entity<CartItem>().ToTable("CartItem");
            modelBuilder.Entity<OrderDetails>().ToTable("OrderDetails");

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Collectable Cards" },
                new Category { CategoryID = 2, CategoryName = "Collectable Cars" },
                new Category { CategoryID = 3, CategoryName = "Trading Cards" },
                new Category { CategoryID = 4, CategoryName = "Funko Pops" },
                new Category { CategoryID = 5, CategoryName = "Board Games" }
            );

            modelBuilder.Entity<Condition>().HasData(
                new Condition { ConditionID = 1, ConditionName = "Perfect" },
                new Condition { ConditionID = 2, ConditionName = "Near Mint" },
                new Condition { ConditionID = 3, ConditionName = "Mint" },
                new Condition { ConditionID = 4, ConditionName = "Played" },
                new Condition { ConditionID = 5, ConditionName = "Damaged" },
                new Condition { ConditionID = 6, ConditionName = "Badly Damaged" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusID = 1, StatusName = "In Progress" },
                new Status { StatusID = 2, StatusName = "Pre-Order" },
                new Status { StatusID = 3, StatusName = "Shipped" },
                new Status { StatusID = 4, StatusName = "Cancelled" },
                new Status { StatusID = 5, StatusName = "Not Started" }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag { TagID = 1, TagName = "New Arrival" },
                new Tag { TagID = 2, TagName = "Pre-Order" },
                new Tag { TagID = 3, TagName = "Sale" },
                new Tag { TagID = 4, TagName = "Singles" }
            );

            // Seed data for Item entity
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    ItemID = 1,
                    SetName = "Yu-Gi-Oh Cards",
                    ItemName = "Legend of Blue Eyes White Dragon",
                    TagID = 1,
                    Description = "24 Packs Per Box. \nThe first set in the Yu-Gi-Oh hit TV show franchise. \nBattle or trade with your friends.",
                    Image = "",
                    Price = 999.99,
                    CategoryID = 1,
                    Stock = 20,
                    Condition = null,
                    Historical = false
                },
                new Item
                {
                    ItemID = 2,
                    SetName = "Legend Of Blue Eyes White Dragon - LOB",
                    ItemName = "Blue Eyes White Dragon",
                    TagID = 4,
                    Description = "The Ultimate Yu-Gi-Oh Card.",
                    Image = "",
                    Price = 349.99,
                    CategoryID = 1,
                    Stock = 1,
                    Condition = 1,
                    Historical = false
                }
            );
        }
    }
}
