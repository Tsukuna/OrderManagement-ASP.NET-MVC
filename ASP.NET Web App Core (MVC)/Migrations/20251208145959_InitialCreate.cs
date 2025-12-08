using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASP.NET_Web_App_Core__MVC_.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AgentID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Agents_AgentID",
                        column: x => x.AgentID,
                        principalTable: "Agents",
                        principalColumn: "AgentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "AgentID", "Address", "AgentName", "CreatedDate", "Email", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main St, New York, NY 10001", "TechWorld Solutions", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@techworld.com", "555-0101" },
                    { 2, "456 Oak Ave, Los Angeles, CA 90001", "Global Electronics", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@globalelec.com", "555-0102" },
                    { 3, "789 Pine Rd, Chicago, IL 60601", "Digital Hub Inc", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "sales@digitalhub.com", "555-0103" },
                    { 4, "321 Elm St, Houston, TX 77001", "Smart Devices Co", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "orders@smartdevices.com", "555-0104" },
                    { 5, "654 Maple Dr, Phoenix, AZ 85001", "Prime Technology", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "support@primetech.com", "555-0105" },
                    { 6, "987 Cedar Ln, Philadelphia, PA 19101", "Metro Electronics", new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@metroelec.com", "555-0106" },
                    { 7, "147 Birch Blvd, San Antonio, TX 78201", "Tech Express", new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@techexpress.com", "555-0107" },
                    { 8, "258 Spruce Way, San Diego, CA 92101", "Innovation Store", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "sales@innovstore.com", "555-0108" },
                    { 9, "369 Willow Ct, Dallas, TX 75201", "Future Electronics", new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "orders@futureelec.com", "555-0109" },
                    { 10, "741 Ash Ave, San Jose, CA 95101", "Elite Tech Group", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@elitetech.com", "555-0110" },
                    { 11, "852 Poplar St, Austin, TX 78701", "NextGen Supplies", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@nextgensup.com", "555-0111" },
                    { 12, "963 Walnut Rd, Jacksonville, FL 32099", "Quantum Electronics", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "sales@quantumelec.com", "555-0112" },
                    { 13, "159 Cherry Ln, Fort Worth, TX 76101", "Apex Technology", new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "orders@apextech.com", "555-0113" },
                    { 14, "357 Hickory Dr, Columbus, OH 43085", "Vision Tech Co", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@visiontech.com", "555-0114" },
                    { 15, "486 Chestnut Way, Charlotte, NC 28201", "Summit Electronics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@summitelec.com", "555-0115" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "CreatedDate", "Description", "ItemName", "Price", "Size", "StockQuantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-performance ultrabook", "Laptop Dell XPS 13", 1299.99m, "13 inch", 50 },
                    { 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Latest Apple smartphone", "iPhone 15 Pro", 999.99m, "Medium", 100 },
                    { 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Android flagship phone", "Samsung Galaxy S24", 899.99m, "Large", 80 },
                    { 4, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Professional tablet", "iPad Pro 12.9", 1099.99m, "Large", 60 },
                    { 5, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight laptop", "MacBook Air M2", 1199.99m, "13 inch", 45 },
                    { 6, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Noise-cancelling headphones", "Sony WH-1000XM5", 399.99m, "Small", 120 },
                    { 7, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smartwatch", "Apple Watch Series 9", 429.99m, "Small", 90 },
                    { 8, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "4K professional monitor", "Dell UltraSharp Monitor", 549.99m, "27 inch", 70 },
                    { 9, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wireless mouse", "Logitech MX Master 3", 99.99m, "Small", 150 },
                    { 10, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gaming keyboard", "Mechanical Keyboard RGB", 149.99m, "Medium", 110 },
                    { 11, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "NVMe solid state drive", "Samsung SSD 1TB", 129.99m, "Small", 200 },
                    { 12, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mirrorless camera", "Canon EOS R6", 2499.99m, "Large", 30 },
                    { 13, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gaming console", "Nintendo Switch OLED", 349.99m, "Medium", 85 },
                    { 14, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Next-gen console", "PlayStation 5", 499.99m, "Large", 40 },
                    { 15, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gaming console", "Xbox Series X", 499.99m, "Large", 55 },
                    { 16, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wireless earbuds", "AirPods Pro 2", 249.99m, "Small", 130 },
                    { 17, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "E-reader", "Kindle Paperwhite", 139.99m, "Small", 95 },
                    { 18, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action camera", "GoPro Hero 12", 399.99m, "Small", 75 },
                    { 19, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cordless vacuum cleaner", "Dyson V15 Vacuum", 649.99m, "Large", 50 },
                    { 20, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smart doorbell", "Ring Video Doorbell", 99.99m, "Small", 140 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedDate", "Email", "IsLocked", "LastLoginDate", "Password", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", false, null, "admin123", "Admin", "admin" },
                    { 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "john@example.com", false, null, "user123", "User", "john_doe" },
                    { 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@example.com", false, null, "user123", "User", "jane_smith" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "AgentID", "Notes", "OrderDate", "OrderStatus", "TotalAmount", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "Bulk order", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 2599.98m, 1 },
                    { 2, 2, "Priority delivery", new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 999.99m, 2 },
                    { 3, 3, "Regular order", new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 1799.97m, 1 },
                    { 4, 4, "Express shipping", new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", 549.99m, 3 },
                    { 5, 5, "Large order", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 3299.97m, 2 },
                    { 6, 1, "Standard shipping", new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shipped", 1529.97m, 1 },
                    { 7, 6, "Gift wrap requested", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 829.98m, 3 },
                    { 8, 7, "Corporate order", new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 2049.97m, 2 },
                    { 9, 8, "Regular customer", new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 499.99m, 1 },
                    { 10, 9, "Urgent order", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", 1449.97m, 3 },
                    { 11, 10, "Wholesale", new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 999.96m, 2 },
                    { 12, 2, "Standard order", new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shipped", 679.98m, 1 },
                    { 13, 11, "Bulk discount", new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 1899.96m, 3 },
                    { 14, 12, "Valentine special", new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 349.99m, 2 },
                    { 15, 13, "Premium items", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 2599.95m, 1 },
                    { 16, 3, "Fast delivery", new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", 799.98m, 3 },
                    { 17, 14, "Regular order", new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 1299.98m, 2 },
                    { 18, 15, "Standard shipping", new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shipped", 549.98m, 1 },
                    { 19, 4, "Corporate purchase", new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 1949.96m, 3 },
                    { 20, 5, "Gift order", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 429.99m, 2 },
                    { 21, 6, "High value order", new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 3199.96m, 1 },
                    { 22, 7, "Express delivery", new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", 899.97m, 3 },
                    { 23, 8, "Bulk order", new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 1599.97m, 2 },
                    { 24, 9, "Regular shipping", new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shipped", 749.98m, 1 },
                    { 25, 10, "Premium order", new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 2249.96m, 3 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "ID", "ItemID", "OrderID", "Quantity", "UnitAmount" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, 1299.99m },
                    { 2, 2, 2, 1, 999.99m },
                    { 3, 3, 3, 2, 899.99m },
                    { 4, 8, 4, 1, 549.99m },
                    { 5, 1, 5, 1, 1299.99m },
                    { 6, 4, 5, 1, 1099.99m },
                    { 7, 3, 5, 1, 899.99m },
                    { 8, 6, 6, 2, 399.99m },
                    { 9, 16, 6, 3, 249.99m },
                    { 10, 7, 7, 1, 429.99m },
                    { 11, 6, 7, 1, 399.99m },
                    { 12, 12, 8, 1, 2499.99m },
                    { 13, 14, 9, 1, 499.99m },
                    { 14, 10, 10, 3, 149.99m },
                    { 15, 9, 10, 10, 99.99m },
                    { 16, 16, 11, 4, 249.99m },
                    { 17, 11, 12, 2, 129.99m },
                    { 18, 7, 12, 1, 429.99m },
                    { 19, 2, 13, 2, 999.99m },
                    { 20, 13, 14, 1, 349.99m },
                    { 21, 15, 15, 5, 499.99m },
                    { 22, 6, 16, 2, 399.99m },
                    { 23, 1, 17, 1, 1299.99m },
                    { 24, 19, 18, 1, 649.99m },
                    { 25, 18, 19, 2, 399.99m },
                    { 26, 5, 19, 1, 1199.99m },
                    { 27, 7, 20, 1, 429.99m },
                    { 28, 14, 21, 4, 499.99m },
                    { 29, 15, 21, 2, 499.99m },
                    { 30, 3, 22, 1, 899.99m },
                    { 31, 8, 23, 2, 549.99m },
                    { 32, 15, 23, 1, 499.99m },
                    { 33, 20, 24, 5, 99.99m },
                    { 34, 16, 24, 1, 249.99m },
                    { 35, 4, 25, 2, 1099.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ItemID",
                table: "OrderDetails",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AgentID",
                table: "Orders",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
