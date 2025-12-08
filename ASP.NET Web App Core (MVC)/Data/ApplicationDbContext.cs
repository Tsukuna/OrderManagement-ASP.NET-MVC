using Microsoft.EntityFrameworkCore;
using ASP.NET_Web_App_Core__MVC_.Models;

namespace ASP.NET_Web_App_Core__MVC_.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Agent)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AgentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Item)
                .WithMany(i => i.OrderDetails)
                .HasForeignKey(od => od.ItemID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure decimal precision
            modelBuilder.Entity<Item>()
                .Property(i => i.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitAmount)
                .HasPrecision(18, 2);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, UserName = "admin", Email = "admin@example.com", Password = "admin123", Role = "Admin", CreatedDate = new DateTime(2024, 1, 1) },
                new User { UserID = 2, UserName = "john_doe", Email = "john@example.com", Password = "user123", Role = "User", CreatedDate = new DateTime(2024, 1, 2) },
                new User { UserID = 3, UserName = "jane_smith", Email = "jane@example.com", Password = "user123", Role = "User", CreatedDate = new DateTime(2024, 1, 3) }
            );

            // Seed Items (20 items)
            var items = new[]
            {
                new Item { ItemID = 1, ItemName = "Laptop Dell XPS 13", Size = "13 inch", Price = 1299.99m, Description = "High-performance ultrabook", StockQuantity = 50, CreatedDate = new DateTime(2024, 1, 1) },
                new Item { ItemID = 2, ItemName = "iPhone 15 Pro", Size = "Medium", Price = 999.99m, Description = "Latest Apple smartphone", StockQuantity = 100, CreatedDate = new DateTime(2024, 1, 2) },
                new Item { ItemID = 3, ItemName = "Samsung Galaxy S24", Size = "Large", Price = 899.99m, Description = "Android flagship phone", StockQuantity = 80, CreatedDate = new DateTime(2024, 1, 3) },
                new Item { ItemID = 4, ItemName = "iPad Pro 12.9", Size = "Large", Price = 1099.99m, Description = "Professional tablet", StockQuantity = 60, CreatedDate = new DateTime(2024, 1, 4) },
                new Item { ItemID = 5, ItemName = "MacBook Air M2", Size = "13 inch", Price = 1199.99m, Description = "Lightweight laptop", StockQuantity = 45, CreatedDate = new DateTime(2024, 1, 5) },
                new Item { ItemID = 6, ItemName = "Sony WH-1000XM5", Size = "Small", Price = 399.99m, Description = "Noise-cancelling headphones", StockQuantity = 120, CreatedDate = new DateTime(2024, 1, 6) },
                new Item { ItemID = 7, ItemName = "Apple Watch Series 9", Size = "Small", Price = 429.99m, Description = "Smartwatch", StockQuantity = 90, CreatedDate = new DateTime(2024, 1, 7) },
                new Item { ItemID = 8, ItemName = "Dell UltraSharp Monitor", Size = "27 inch", Price = 549.99m, Description = "4K professional monitor", StockQuantity = 70, CreatedDate = new DateTime(2024, 1, 8) },
                new Item { ItemID = 9, ItemName = "Logitech MX Master 3", Size = "Small", Price = 99.99m, Description = "Wireless mouse", StockQuantity = 150, CreatedDate = new DateTime(2024, 1, 9) },
                new Item { ItemID = 10, ItemName = "Mechanical Keyboard RGB", Size = "Medium", Price = 149.99m, Description = "Gaming keyboard", StockQuantity = 110, CreatedDate = new DateTime(2024, 1, 10) },
                new Item { ItemID = 11, ItemName = "Samsung SSD 1TB", Size = "Small", Price = 129.99m, Description = "NVMe solid state drive", StockQuantity = 200, CreatedDate = new DateTime(2024, 1, 11) },
                new Item { ItemID = 12, ItemName = "Canon EOS R6", Size = "Large", Price = 2499.99m, Description = "Mirrorless camera", StockQuantity = 30, CreatedDate = new DateTime(2024, 1, 12) },
                new Item { ItemID = 13, ItemName = "Nintendo Switch OLED", Size = "Medium", Price = 349.99m, Description = "Gaming console", StockQuantity = 85, CreatedDate = new DateTime(2024, 1, 13) },
                new Item { ItemID = 14, ItemName = "PlayStation 5", Size = "Large", Price = 499.99m, Description = "Next-gen console", StockQuantity = 40, CreatedDate = new DateTime(2024, 1, 14) },
                new Item { ItemID = 15, ItemName = "Xbox Series X", Size = "Large", Price = 499.99m, Description = "Gaming console", StockQuantity = 55, CreatedDate = new DateTime(2024, 1, 15) },
                new Item { ItemID = 16, ItemName = "AirPods Pro 2", Size = "Small", Price = 249.99m, Description = "Wireless earbuds", StockQuantity = 130, CreatedDate = new DateTime(2024, 1, 16) },
                new Item { ItemID = 17, ItemName = "Kindle Paperwhite", Size = "Small", Price = 139.99m, Description = "E-reader", StockQuantity = 95, CreatedDate = new DateTime(2024, 1, 17) },
                new Item { ItemID = 18, ItemName = "GoPro Hero 12", Size = "Small", Price = 399.99m, Description = "Action camera", StockQuantity = 75, CreatedDate = new DateTime(2024, 1, 18) },
                new Item { ItemID = 19, ItemName = "Dyson V15 Vacuum", Size = "Large", Price = 649.99m, Description = "Cordless vacuum cleaner", StockQuantity = 50, CreatedDate = new DateTime(2024, 1, 19) },
                new Item { ItemID = 20, ItemName = "Ring Video Doorbell", Size = "Small", Price = 99.99m, Description = "Smart doorbell", StockQuantity = 140, CreatedDate = new DateTime(2024, 1, 20) }
            };
            modelBuilder.Entity<Item>().HasData(items);

            // Seed Agents (15 agents)
            var agents = new[]
            {
                new Agent { AgentID = 1, AgentName = "TechWorld Solutions", Address = "123 Main St, New York, NY 10001", PhoneNumber = "555-0101", Email = "contact@techworld.com", CreatedDate = new DateTime(2024, 1, 1) },
                new Agent { AgentID = 2, AgentName = "Global Electronics", Address = "456 Oak Ave, Los Angeles, CA 90001", PhoneNumber = "555-0102", Email = "info@globalelec.com", CreatedDate = new DateTime(2024, 1, 2) },
                new Agent { AgentID = 3, AgentName = "Digital Hub Inc", Address = "789 Pine Rd, Chicago, IL 60601", PhoneNumber = "555-0103", Email = "sales@digitalhub.com", CreatedDate = new DateTime(2024, 1, 3) },
                new Agent { AgentID = 4, AgentName = "Smart Devices Co", Address = "321 Elm St, Houston, TX 77001", PhoneNumber = "555-0104", Email = "orders@smartdevices.com", CreatedDate = new DateTime(2024, 1, 4) },
                new Agent { AgentID = 5, AgentName = "Prime Technology", Address = "654 Maple Dr, Phoenix, AZ 85001", PhoneNumber = "555-0105", Email = "support@primetech.com", CreatedDate = new DateTime(2024, 1, 5) },
                new Agent { AgentID = 6, AgentName = "Metro Electronics", Address = "987 Cedar Ln, Philadelphia, PA 19101", PhoneNumber = "555-0106", Email = "contact@metroelec.com", CreatedDate = new DateTime(2024, 1, 6) },
                new Agent { AgentID = 7, AgentName = "Tech Express", Address = "147 Birch Blvd, San Antonio, TX 78201", PhoneNumber = "555-0107", Email = "info@techexpress.com", CreatedDate = new DateTime(2024, 1, 7) },
                new Agent { AgentID = 8, AgentName = "Innovation Store", Address = "258 Spruce Way, San Diego, CA 92101", PhoneNumber = "555-0108", Email = "sales@innovstore.com", CreatedDate = new DateTime(2024, 1, 8) },
                new Agent { AgentID = 9, AgentName = "Future Electronics", Address = "369 Willow Ct, Dallas, TX 75201", PhoneNumber = "555-0109", Email = "orders@futureelec.com", CreatedDate = new DateTime(2024, 1, 9) },
                new Agent { AgentID = 10, AgentName = "Elite Tech Group", Address = "741 Ash Ave, San Jose, CA 95101", PhoneNumber = "555-0110", Email = "contact@elitetech.com", CreatedDate = new DateTime(2024, 1, 10) },
                new Agent { AgentID = 11, AgentName = "NextGen Supplies", Address = "852 Poplar St, Austin, TX 78701", PhoneNumber = "555-0111", Email = "info@nextgensup.com", CreatedDate = new DateTime(2024, 1, 11) },
                new Agent { AgentID = 12, AgentName = "Quantum Electronics", Address = "963 Walnut Rd, Jacksonville, FL 32099", PhoneNumber = "555-0112", Email = "sales@quantumelec.com", CreatedDate = new DateTime(2024, 1, 12) },
                new Agent { AgentID = 13, AgentName = "Apex Technology", Address = "159 Cherry Ln, Fort Worth, TX 76101", PhoneNumber = "555-0113", Email = "orders@apextech.com", CreatedDate = new DateTime(2024, 1, 13) },
                new Agent { AgentID = 14, AgentName = "Vision Tech Co", Address = "357 Hickory Dr, Columbus, OH 43085", PhoneNumber = "555-0114", Email = "contact@visiontech.com", CreatedDate = new DateTime(2024, 1, 14) },
                new Agent { AgentID = 15, AgentName = "Summit Electronics", Address = "486 Chestnut Way, Charlotte, NC 28201", PhoneNumber = "555-0115", Email = "info@summitelec.com", CreatedDate = new DateTime(2024, 1, 15) }
            };
            modelBuilder.Entity<Agent>().HasData(agents);

            // Seed Orders (25 orders)
            var orders = new[]
            {
                new Order { OrderID = 1, OrderDate = new DateTime(2024, 2, 1), AgentID = 1, UserID = 1, OrderStatus = "Completed", TotalAmount = 2599.98m, Notes = "Bulk order" },
                new Order { OrderID = 2, OrderDate = new DateTime(2024, 2, 2), AgentID = 2, UserID = 2, OrderStatus = "Pending", TotalAmount = 999.99m, Notes = "Priority delivery" },
                new Order { OrderID = 3, OrderDate = new DateTime(2024, 2, 3), AgentID = 3, UserID = 1, OrderStatus = "Completed", TotalAmount = 1799.97m, Notes = "Regular order" },
                new Order { OrderID = 4, OrderDate = new DateTime(2024, 2, 4), AgentID = 4, UserID = 3, OrderStatus = "Processing", TotalAmount = 549.99m, Notes = "Express shipping" },
                new Order { OrderID = 5, OrderDate = new DateTime(2024, 2, 5), AgentID = 5, UserID = 2, OrderStatus = "Completed", TotalAmount = 3299.97m, Notes = "Large order" },
                new Order { OrderID = 6, OrderDate = new DateTime(2024, 2, 6), AgentID = 1, UserID = 1, OrderStatus = "Shipped", TotalAmount = 1529.97m, Notes = "Standard shipping" },
                new Order { OrderID = 7, OrderDate = new DateTime(2024, 2, 7), AgentID = 6, UserID = 3, OrderStatus = "Completed", TotalAmount = 829.98m, Notes = "Gift wrap requested" },
                new Order { OrderID = 8, OrderDate = new DateTime(2024, 2, 8), AgentID = 7, UserID = 2, OrderStatus = "Pending", TotalAmount = 2049.97m, Notes = "Corporate order" },
                new Order { OrderID = 9, OrderDate = new DateTime(2024, 2, 9), AgentID = 8, UserID = 1, OrderStatus = "Completed", TotalAmount = 499.99m, Notes = "Regular customer" },
                new Order { OrderID = 10, OrderDate = new DateTime(2024, 2, 10), AgentID = 9, UserID = 3, OrderStatus = "Processing", TotalAmount = 1449.97m, Notes = "Urgent order" },
                new Order { OrderID = 11, OrderDate = new DateTime(2024, 2, 11), AgentID = 10, UserID = 2, OrderStatus = "Completed", TotalAmount = 999.96m, Notes = "Wholesale" },
                new Order { OrderID = 12, OrderDate = new DateTime(2024, 2, 12), AgentID = 2, UserID = 1, OrderStatus = "Shipped", TotalAmount = 679.98m, Notes = "Standard order" },
                new Order { OrderID = 13, OrderDate = new DateTime(2024, 2, 13), AgentID = 11, UserID = 3, OrderStatus = "Completed", TotalAmount = 1899.96m, Notes = "Bulk discount" },
                new Order { OrderID = 14, OrderDate = new DateTime(2024, 2, 14), AgentID = 12, UserID = 2, OrderStatus = "Pending", TotalAmount = 349.99m, Notes = "Valentine special" },
                new Order { OrderID = 15, OrderDate = new DateTime(2024, 2, 15), AgentID = 13, UserID = 1, OrderStatus = "Completed", TotalAmount = 2599.95m, Notes = "Premium items" },
                new Order { OrderID = 16, OrderDate = new DateTime(2024, 2, 16), AgentID = 3, UserID = 3, OrderStatus = "Processing", TotalAmount = 799.98m, Notes = "Fast delivery" },
                new Order { OrderID = 17, OrderDate = new DateTime(2024, 2, 17), AgentID = 14, UserID = 2, OrderStatus = "Completed", TotalAmount = 1299.98m, Notes = "Regular order" },
                new Order { OrderID = 18, OrderDate = new DateTime(2024, 2, 18), AgentID = 15, UserID = 1, OrderStatus = "Shipped", TotalAmount = 549.98m, Notes = "Standard shipping" },
                new Order { OrderID = 19, OrderDate = new DateTime(2024, 2, 19), AgentID = 4, UserID = 3, OrderStatus = "Completed", TotalAmount = 1949.96m, Notes = "Corporate purchase" },
                new Order { OrderID = 20, OrderDate = new DateTime(2024, 2, 20), AgentID = 5, UserID = 2, OrderStatus = "Pending", TotalAmount = 429.99m, Notes = "Gift order" },
                new Order { OrderID = 21, OrderDate = new DateTime(2024, 2, 21), AgentID = 6, UserID = 1, OrderStatus = "Completed", TotalAmount = 3199.96m, Notes = "High value order" },
                new Order { OrderID = 22, OrderDate = new DateTime(2024, 2, 22), AgentID = 7, UserID = 3, OrderStatus = "Processing", TotalAmount = 899.97m, Notes = "Express delivery" },
                new Order { OrderID = 23, OrderDate = new DateTime(2024, 2, 23), AgentID = 8, UserID = 2, OrderStatus = "Completed", TotalAmount = 1599.97m, Notes = "Bulk order" },
                new Order { OrderID = 24, OrderDate = new DateTime(2024, 2, 24), AgentID = 9, UserID = 1, OrderStatus = "Shipped", TotalAmount = 749.98m, Notes = "Regular shipping" },
                new Order { OrderID = 25, OrderDate = new DateTime(2024, 2, 25), AgentID = 10, UserID = 3, OrderStatus = "Completed", TotalAmount = 2249.96m, Notes = "Premium order" }
            };
            modelBuilder.Entity<Order>().HasData(orders);

            // Seed OrderDetails (sample data for orders)
            var orderDetails = new List<OrderDetail>();
            int detailId = 1;
            
            // Order 1: 2 items
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 1, ItemID = 1, Quantity = 2, UnitAmount = 1299.99m });
            
            // Order 2: 1 item
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 2, ItemID = 2, Quantity = 1, UnitAmount = 999.99m });
            
            // Order 3: 2 items
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 3, ItemID = 3, Quantity = 2, UnitAmount = 899.99m });
            
            // Order 4: 1 item
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 4, ItemID = 8, Quantity = 1, UnitAmount = 549.99m });
            
            // Order 5: 3 items
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 5, ItemID = 1, Quantity = 1, UnitAmount = 1299.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 5, ItemID = 4, Quantity = 1, UnitAmount = 1099.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 5, ItemID = 3, Quantity = 1, UnitAmount = 899.99m });
            
            // Order 6: 2 items
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 6, ItemID = 6, Quantity = 2, UnitAmount = 399.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 6, ItemID = 16, Quantity = 3, UnitAmount = 249.99m });
            
            // Order 7: 2 items
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 7, ItemID = 7, Quantity = 1, UnitAmount = 429.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 7, ItemID = 6, Quantity = 1, UnitAmount = 399.99m });
            
            // Order 8: 3 items
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 8, ItemID = 12, Quantity = 1, UnitAmount = 2499.99m });
            
            // Order 9: 1 item
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 9, ItemID = 14, Quantity = 1, UnitAmount = 499.99m });
            
            // Order 10: 2 items
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 10, ItemID = 10, Quantity = 3, UnitAmount = 149.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 10, ItemID = 9, Quantity = 10, UnitAmount = 99.99m });
            
            // Continue with more order details
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 11, ItemID = 16, Quantity = 4, UnitAmount = 249.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 12, ItemID = 11, Quantity = 2, UnitAmount = 129.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 12, ItemID = 7, Quantity = 1, UnitAmount = 429.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 13, ItemID = 2, Quantity = 2, UnitAmount = 999.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 14, ItemID = 13, Quantity = 1, UnitAmount = 349.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 15, ItemID = 15, Quantity = 5, UnitAmount = 499.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 16, ItemID = 6, Quantity = 2, UnitAmount = 399.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 17, ItemID = 1, Quantity = 1, UnitAmount = 1299.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 18, ItemID = 19, Quantity = 1, UnitAmount = 649.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 19, ItemID = 18, Quantity = 2, UnitAmount = 399.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 19, ItemID = 5, Quantity = 1, UnitAmount = 1199.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 20, ItemID = 7, Quantity = 1, UnitAmount = 429.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 21, ItemID = 14, Quantity = 4, UnitAmount = 499.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 21, ItemID = 15, Quantity = 2, UnitAmount = 499.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 22, ItemID = 3, Quantity = 1, UnitAmount = 899.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 23, ItemID = 8, Quantity = 2, UnitAmount = 549.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 23, ItemID = 15, Quantity = 1, UnitAmount = 499.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 24, ItemID = 20, Quantity = 5, UnitAmount = 99.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 24, ItemID = 16, Quantity = 1, UnitAmount = 249.99m });
            orderDetails.Add(new OrderDetail { ID = detailId++, OrderID = 25, ItemID = 4, Quantity = 2, UnitAmount = 1099.99m });

            modelBuilder.Entity<OrderDetail>().HasData(orderDetails);
        }
    }
}

