using DeliverySystemAPI.Models;
using Microsoft.EntityFrameworkCore;


public class onlineDeliveryDbContext : DbContext
{
    public onlineDeliveryDbContext(DbContextOptions<onlineDeliveryDbContext> options)
      : base(options) { }

    public DbSet<CustomerAccount> customeraccount { get; set; }
    public DbSet<DriverAccount> driveraccount { get; set; }
    public DbSet<AdminAccount> adminaccount { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminAccount>().HasNoKey();

        modelBuilder.Entity<Category>().HasData(
             new Category { CategoryId = 1, Name = "Cheese" },
             new Category { CategoryId = 2, Name = "Meat" },
             new Category { CategoryId = 3, Name = "Fish" },
             new Category { CategoryId = 4, Name = "Bread" });

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, CategoryId = 1, Name = "Cheddar" },
            new Product { ProductId = 2, CategoryId = 1, Name = "Brie" },
            new Product { ProductId = 3, CategoryId = 1, Name = "Stilton" },
            new Product { ProductId = 4, CategoryId = 1, Name = "Cheshire" },
            new Product { ProductId = 5, CategoryId = 1, Name = "Swiss" },
            new Product { ProductId = 6, CategoryId = 1, Name = "Gruyere" },
            new Product { ProductId = 7, CategoryId = 1, Name = "Colby" },
            new Product { ProductId = 8, CategoryId = 1, Name = "Mozzela" },
            new Product { ProductId = 9, CategoryId = 1, Name = "Ricotta" },
            new Product { ProductId = 10, CategoryId = 1, Name = "Parmesan" },
            new Product { ProductId = 11, CategoryId = 2, Name = "Ham" },
            new Product { ProductId = 12, CategoryId = 2, Name = "Beef" },
            new Product { ProductId = 13, CategoryId = 2, Name = "Chicken" },
            new Product { ProductId = 14, CategoryId = 2, Name = "Turkey" },
            new Product { ProductId = 15, CategoryId = 2, Name = "Prosciutto" },
            new Product { ProductId = 16, CategoryId = 2, Name = "Bacon" },
            new Product { ProductId = 17, CategoryId = 2, Name = "Mutton" },
            new Product { ProductId = 18, CategoryId = 2, Name = "Pastrami" },
            new Product { ProductId = 19, CategoryId = 2, Name = "Hazlet" },
            new Product { ProductId = 20, CategoryId = 2, Name = "Salami" },
            new Product { ProductId = 21, CategoryId = 3, Name = "Salmon" },
            new Product { ProductId = 22, CategoryId = 3, Name = "Tuna" },
            new Product { ProductId = 23, CategoryId = 3, Name = "Mackerel" },
            new Product { ProductId = 24, CategoryId = 4, Name = "Rye" },
            new Product { ProductId = 25, CategoryId = 4, Name = "Wheat" },
            new Product { ProductId = 26, CategoryId = 4, Name = "Brioche" },
            new Product { ProductId = 27, CategoryId = 4, Name = "Naan" },
            new Product { ProductId = 28, CategoryId = 4, Name = "Focaccia" },
            new Product { ProductId = 29, CategoryId = 4, Name = "Malted" },
            new Product { ProductId = 30, CategoryId = 4, Name = "Sourdough" },
            new Product { ProductId = 31, CategoryId = 4, Name = "Corn" },
            new Product { ProductId = 32, CategoryId = 4, Name = "White" },
            new Product { ProductId = 33, CategoryId = 4, Name = "Soda" });

        modelBuilder.Entity<Driver>().HasData(
      new Driver { DriverId = 1, firstName = "Robert", lastName = "Jacobs", age = 34, vehicleType = "Truck" },
      new Driver { DriverId = 2, firstName = "Jack", lastName = "William", age = 55, vehicleType = "Train" },
      new Driver { DriverId = 3, firstName = "Bob", lastName = "Adams", age = 23, vehicleType = "Truck" },
      new Driver { DriverId = 4, firstName = "Will", lastName = "Smith", age = 45, vehicleType = "Truck" },
      new Driver { DriverId = 5, firstName = "Doom", lastName = "Slayer", age = 56, vehicleType = "Car" },
      new Driver { DriverId = 6, firstName = "Space", lastName = "Marine", age = 33, vehicleType = "Train" });




        modelBuilder.Entity<Customer>();

        modelBuilder.Entity<Order>().HasKey(o => o.OrdersId);

        modelBuilder.Entity<Order>().HasOne(o => o.Customer)
        .WithMany(c => c.Orders)
        .HasForeignKey(o => o.CustomerId);


        modelBuilder.Entity<Delivery>().HasData();

        modelBuilder.Entity<Delivery>()
            .HasOne(dd => dd.Order)
            .WithMany()
            .HasForeignKey(dd => dd.OrdersId);

        modelBuilder.Entity<Delivery>()
            .HasOne(dd => dd.Driver)
            .WithMany()
            .HasForeignKey(dd => dd.DriverId);

    }
}

