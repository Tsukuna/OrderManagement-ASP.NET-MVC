-- ====================================================
-- Order Management System - Database Creation Script
-- Exercise 4 - ASP.NET Core MVC
-- Server: DESKTOP-295DC70\SQLEXPRESS
-- ====================================================

USE master;
GO

-- Drop database if exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'OrderManagementDB')
BEGIN
    ALTER DATABASE OrderManagementDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE OrderManagementDB;
END
GO

-- Create new database
CREATE DATABASE OrderManagementDB;
GO

USE OrderManagementDB;
GO

-- ====================================================
-- Table: Users
-- Description: Stores user account information
-- ====================================================
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    IsLocked BIT NOT NULL DEFAULT 0,
    Role NVARCHAR(50) NOT NULL DEFAULT 'User',
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastLoginDate DATETIME NULL,
    CONSTRAINT UK_Users_UserName UNIQUE (UserName),
    CONSTRAINT UK_Users_Email UNIQUE (Email)
);
GO

-- ====================================================
-- Table: Items
-- Description: Stores product/item information
-- ====================================================
CREATE TABLE Items (
    ItemID INT PRIMARY KEY IDENTITY(1,1),
    ItemName NVARCHAR(100) NOT NULL,
    Size NVARCHAR(50) NULL,
    Price DECIMAL(18, 2) NOT NULL,
    Description NVARCHAR(500) NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CK_Items_Price CHECK (Price >= 0),
    CONSTRAINT CK_Items_StockQuantity CHECK (StockQuantity >= 0)
);
GO

-- ====================================================
-- Table: Agents
-- Description: Stores agent/supplier information
-- ====================================================
CREATE TABLE Agents (
    AgentID INT PRIMARY KEY IDENTITY(1,1),
    AgentName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    PhoneNumber NVARCHAR(20) NULL,
    Email NVARCHAR(100) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ====================================================
-- Table: Orders
-- Description: Stores order header information
-- ====================================================
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    AgentID INT NOT NULL,
    UserID INT NULL,
    OrderStatus NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    TotalAmount DECIMAL(18, 2) NOT NULL DEFAULT 0,
    Notes NVARCHAR(500) NULL,
    CONSTRAINT FK_Orders_Agents FOREIGN KEY (AgentID) REFERENCES Agents(AgentID),
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT CK_Orders_TotalAmount CHECK (TotalAmount >= 0)
);
GO

-- ====================================================
-- Table: OrderDetails
-- Description: Stores order line items
-- ====================================================
CREATE TABLE OrderDetails (
    ID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT NOT NULL,
    ItemID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitAmount DECIMAL(18, 2) NOT NULL,
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    CONSTRAINT FK_OrderDetails_Items FOREIGN KEY (ItemID) REFERENCES Items(ItemID),
    CONSTRAINT CK_OrderDetails_Quantity CHECK (Quantity > 0),
    CONSTRAINT CK_OrderDetails_UnitAmount CHECK (UnitAmount >= 0)
);
GO

-- ====================================================
-- Create Indexes for better query performance
-- ====================================================
CREATE INDEX IX_Orders_AgentID ON Orders(AgentID);
CREATE INDEX IX_Orders_UserID ON Orders(UserID);
CREATE INDEX IX_Orders_OrderDate ON Orders(OrderDate);
CREATE INDEX IX_Orders_OrderStatus ON Orders(OrderStatus);
CREATE INDEX IX_OrderDetails_OrderID ON OrderDetails(OrderID);
CREATE INDEX IX_OrderDetails_ItemID ON OrderDetails(ItemID);
CREATE INDEX IX_Items_ItemName ON Items(ItemName);
GO

-- ====================================================
-- Insert Sample Data - Users
-- ====================================================
SET IDENTITY_INSERT Users ON;

INSERT INTO Users (UserID, UserName, Email, Password, Role, CreatedDate, IsLocked) VALUES
(1, 'admin', 'admin@example.com', 'admin123', 'Admin', '2024-01-01', 0),
(2, 'john_doe', 'john@example.com', 'user123', 'User', '2024-01-02', 0),
(3, 'jane_smith', 'jane@example.com', 'user123', 'User', '2024-01-03', 0);

SET IDENTITY_INSERT Users OFF;
GO

-- ====================================================
-- Insert Sample Data - Items (20 items)
-- ====================================================
SET IDENTITY_INSERT Items ON;

INSERT INTO Items (ItemID, ItemName, Size, Price, Description, StockQuantity, CreatedDate) VALUES
(1, 'Laptop Dell XPS 13', '13 inch', 1299.99, 'High-performance ultrabook', 50, '2024-01-01'),
(2, 'iPhone 15 Pro', 'Medium', 999.99, 'Latest Apple smartphone', 100, '2024-01-02'),
(3, 'Samsung Galaxy S24', 'Large', 899.99, 'Android flagship phone', 80, '2024-01-03'),
(4, 'iPad Pro 12.9', 'Large', 1099.99, 'Professional tablet', 60, '2024-01-04'),
(5, 'MacBook Air M2', '13 inch', 1199.99, 'Lightweight laptop', 45, '2024-01-05'),
(6, 'Sony WH-1000XM5', 'Small', 399.99, 'Noise-cancelling headphones', 120, '2024-01-06'),
(7, 'Apple Watch Series 9', 'Small', 429.99, 'Smartwatch', 90, '2024-01-07'),
(8, 'Dell UltraSharp Monitor', '27 inch', 549.99, '4K professional monitor', 70, '2024-01-08'),
(9, 'Logitech MX Master 3', 'Small', 99.99, 'Wireless mouse', 150, '2024-01-09'),
(10, 'Mechanical Keyboard RGB', 'Medium', 149.99, 'Gaming keyboard', 110, '2024-01-10'),
(11, 'Samsung SSD 1TB', 'Small', 129.99, 'NVMe solid state drive', 200, '2024-01-11'),
(12, 'Canon EOS R6', 'Large', 2499.99, 'Mirrorless camera', 30, '2024-01-12'),
(13, 'Nintendo Switch OLED', 'Medium', 349.99, 'Gaming console', 85, '2024-01-13'),
(14, 'PlayStation 5', 'Large', 499.99, 'Next-gen console', 40, '2024-01-14'),
(15, 'Xbox Series X', 'Large', 499.99, 'Gaming console', 55, '2024-01-15'),
(16, 'AirPods Pro 2', 'Small', 249.99, 'Wireless earbuds', 130, '2024-01-16'),
(17, 'Kindle Paperwhite', 'Small', 139.99, 'E-reader', 95, '2024-01-17'),
(18, 'GoPro Hero 12', 'Small', 399.99, 'Action camera', 75, '2024-01-18'),
(19, 'Dyson V15 Vacuum', 'Large', 649.99, 'Cordless vacuum cleaner', 50, '2024-01-19'),
(20, 'Ring Video Doorbell', 'Small', 99.99, 'Smart doorbell', 140, '2024-01-20');

SET IDENTITY_INSERT Items OFF;
GO

-- ====================================================
-- Insert Sample Data - Agents (15 agents)
-- ====================================================
SET IDENTITY_INSERT Agents ON;

INSERT INTO Agents (AgentID, AgentName, Address, PhoneNumber, Email, CreatedDate) VALUES
(1, 'TechWorld Solutions', '123 Main St, New York, NY 10001', '555-0101', 'contact@techworld.com', '2024-01-01'),
(2, 'Global Electronics', '456 Oak Ave, Los Angeles, CA 90001', '555-0102', 'info@globalelec.com', '2024-01-02'),
(3, 'Digital Hub Inc', '789 Pine Rd, Chicago, IL 60601', '555-0103', 'sales@digitalhub.com', '2024-01-03'),
(4, 'Smart Devices Co', '321 Elm St, Houston, TX 77001', '555-0104', 'orders@smartdevices.com', '2024-01-04'),
(5, 'Prime Technology', '654 Maple Dr, Phoenix, AZ 85001', '555-0105', 'support@primetech.com', '2024-01-05'),
(6, 'Metro Electronics', '987 Cedar Ln, Philadelphia, PA 19101', '555-0106', 'contact@metroelec.com', '2024-01-06'),
(7, 'Tech Express', '147 Birch Blvd, San Antonio, TX 78201', '555-0107', 'info@techexpress.com', '2024-01-07'),
(8, 'Innovation Store', '258 Spruce Way, San Diego, CA 92101', '555-0108', 'sales@innovstore.com', '2024-01-08'),
(9, 'Future Electronics', '369 Willow Ct, Dallas, TX 75201', '555-0109', 'orders@futureelec.com', '2024-01-09'),
(10, 'Elite Tech Group', '741 Ash Ave, San Jose, CA 95101', '555-0110', 'contact@elitetech.com', '2024-01-10'),
(11, 'NextGen Supplies', '852 Poplar St, Austin, TX 78701', '555-0111', 'info@nextgensup.com', '2024-01-11'),
(12, 'Quantum Electronics', '963 Walnut Rd, Jacksonville, FL 32099', '555-0112', 'sales@quantumelec.com', '2024-01-12'),
(13, 'Apex Technology', '159 Cherry Ln, Fort Worth, TX 76101', '555-0113', 'orders@apextech.com', '2024-01-13'),
(14, 'Vision Tech Co', '357 Hickory Dr, Columbus, OH 43085', '555-0114', 'contact@visiontech.com', '2024-01-14'),
(15, 'Summit Electronics', '486 Chestnut Way, Charlotte, NC 28201', '555-0115', 'info@summitelec.com', '2024-01-15');

SET IDENTITY_INSERT Agents OFF;
GO

-- ====================================================
-- Insert Sample Data - Orders (25 orders)
-- ====================================================
SET IDENTITY_INSERT Orders ON;

INSERT INTO Orders (OrderID, OrderDate, AgentID, UserID, OrderStatus, TotalAmount, Notes) VALUES
(1, '2024-02-01', 1, 1, 'Completed', 2599.98, 'Bulk order'),
(2, '2024-02-02', 2, 2, 'Pending', 999.99, 'Priority delivery'),
(3, '2024-02-03', 3, 1, 'Completed', 1799.98, 'Regular order'),
(4, '2024-02-04', 4, 3, 'Processing', 549.99, 'Express shipping'),
(5, '2024-02-05', 5, 2, 'Completed', 3299.97, 'Large order'),
(6, '2024-02-06', 1, 1, 'Shipped', 1549.97, 'Standard shipping'),
(7, '2024-02-07', 6, 3, 'Completed', 829.98, 'Gift wrap requested'),
(8, '2024-02-08', 7, 2, 'Pending', 2499.99, 'Corporate order'),
(9, '2024-02-09', 8, 1, 'Completed', 499.99, 'Regular customer'),
(10, '2024-02-10', 9, 3, 'Processing', 1449.97, 'Urgent order'),
(11, '2024-02-11', 10, 2, 'Completed', 999.96, 'Wholesale'),
(12, '2024-02-12', 2, 1, 'Shipped', 689.97, 'Standard order'),
(13, '2024-02-13', 11, 3, 'Completed', 1999.98, 'Bulk discount'),
(14, '2024-02-14', 12, 2, 'Pending', 349.99, 'Valentine special'),
(15, '2024-02-15', 13, 1, 'Completed', 2499.95, 'Premium items'),
(16, '2024-02-16', 3, 3, 'Processing', 799.98, 'Fast delivery'),
(17, '2024-02-17', 14, 2, 'Completed', 1299.99, 'Regular order'),
(18, '2024-02-18', 15, 1, 'Shipped', 649.99, 'Standard shipping'),
(19, '2024-02-19', 4, 3, 'Completed', 1999.97, 'Corporate purchase'),
(20, '2024-02-20', 5, 2, 'Pending', 429.99, 'Gift order'),
(21, '2024-02-21', 6, 1, 'Completed', 3999.96, 'High value order'),
(22, '2024-02-22', 7, 3, 'Processing', 899.99, 'Express delivery'),
(23, '2024-02-23', 8, 2, 'Completed', 1599.97, 'Bulk order'),
(24, '2024-02-24', 9, 1, 'Shipped', 749.97, 'Regular shipping'),
(25, '2024-02-25', 10, 3, 'Completed', 2199.98, 'Premium order');

SET IDENTITY_INSERT Orders OFF;
GO

-- ====================================================
-- Insert Sample Data - OrderDetails
-- ====================================================
SET IDENTITY_INSERT OrderDetails ON;

INSERT INTO OrderDetails (ID, OrderID, ItemID, Quantity, UnitAmount) VALUES
-- Order 1
(1, 1, 1, 2, 1299.99),
-- Order 2
(2, 2, 2, 1, 999.99),
-- Order 3
(3, 3, 3, 2, 899.99),
-- Order 4
(4, 4, 8, 1, 549.99),
-- Order 5
(5, 5, 1, 1, 1299.99),
(6, 5, 4, 1, 1099.99),
(7, 5, 3, 1, 899.99),
-- Order 6
(8, 6, 6, 2, 399.99),
(9, 6, 16, 3, 249.99),
-- Order 7
(10, 7, 7, 1, 429.99),
(11, 7, 6, 1, 399.99),
-- Order 8
(12, 8, 12, 1, 2499.99),
-- Order 9
(13, 9, 14, 1, 499.99),
-- Order 10
(14, 10, 10, 3, 149.99),
(15, 10, 9, 10, 99.99),
-- Order 11
(16, 11, 16, 4, 249.99),
-- Order 12
(17, 12, 11, 2, 129.99),
(18, 12, 7, 1, 429.99),
-- Order 13
(19, 13, 2, 2, 999.99),
-- Order 14
(20, 14, 13, 1, 349.99),
-- Order 15
(21, 15, 15, 5, 499.99),
-- Order 16
(22, 16, 6, 2, 399.99),
-- Order 17
(23, 17, 1, 1, 1299.99),
-- Order 18
(24, 18, 19, 1, 649.99),
-- Order 19
(25, 19, 18, 2, 399.99),
(26, 19, 5, 1, 1199.99),
-- Order 20
(27, 20, 7, 1, 429.99),
-- Order 21
(28, 21, 14, 4, 499.99),
(29, 21, 15, 4, 499.99),
-- Order 22
(30, 22, 3, 1, 899.99),
-- Order 23
(31, 23, 8, 2, 549.99),
(32, 23, 15, 1, 499.99),
-- Order 24
(33, 24, 20, 5, 99.99),
(34, 24, 16, 1, 249.99),
-- Order 25
(35, 25, 4, 2, 1099.99);

SET IDENTITY_INSERT OrderDetails OFF;
GO

-- ====================================================
-- Create Useful Views
-- ====================================================

-- View: Order Summary
CREATE VIEW vw_OrderSummary AS
SELECT 
    o.OrderID,
    o.OrderDate,
    a.AgentName,
    u.UserName,
    o.OrderStatus,
    COUNT(od.ID) AS ItemCount,
    SUM(od.Quantity) AS TotalQuantity,
    o.TotalAmount
FROM Orders o
LEFT JOIN Agents a ON o.AgentID = a.AgentID
LEFT JOIN Users u ON o.UserID = u.UserID
LEFT JOIN OrderDetails od ON o.OrderID = od.OrderID
GROUP BY o.OrderID, o.OrderDate, a.AgentName, u.UserName, o.OrderStatus, o.TotalAmount;
GO

-- View: Best Selling Items
CREATE VIEW vw_BestSellingItems AS
SELECT TOP 10
    i.ItemID,
    i.ItemName,
    i.Price,
    SUM(od.Quantity) AS TotalQuantitySold,
    SUM(od.Quantity * od.UnitAmount) AS TotalRevenue,
    COUNT(DISTINCT od.OrderID) AS OrderCount
FROM Items i
INNER JOIN OrderDetails od ON i.ItemID = od.ItemID
GROUP BY i.ItemID, i.ItemName, i.Price
ORDER BY TotalQuantitySold DESC;
GO

-- View: Agent Performance
CREATE VIEW vw_AgentPerformance AS
SELECT 
    a.AgentID,
    a.AgentName,
    a.Email,
    COUNT(o.OrderID) AS TotalOrders,
    SUM(o.TotalAmount) AS TotalRevenue,
    COUNT(CASE WHEN o.OrderStatus = 'Completed' THEN 1 END) AS CompletedOrders
FROM Agents a
LEFT JOIN Orders o ON a.AgentID = o.AgentID
GROUP BY a.AgentID, a.AgentName, a.Email;
GO

-- ====================================================
-- Create Stored Procedures
-- ====================================================

-- Procedure: Get Order Details
CREATE PROCEDURE sp_GetOrderDetails
    @OrderID INT
AS
BEGIN
    SELECT 
        o.OrderID,
        o.OrderDate,
        o.OrderStatus,
        o.TotalAmount,
        o.Notes,
        a.AgentName,
        a.Email AS AgentEmail,
        a.PhoneNumber AS AgentPhone,
        u.UserName,
        u.Email AS UserEmail
    FROM Orders o
    LEFT JOIN Agents a ON o.AgentID = a.AgentID
    LEFT JOIN Users u ON o.UserID = u.UserID
    WHERE o.OrderID = @OrderID;

    SELECT 
        od.ID,
        od.Quantity,
        od.UnitAmount,
        i.ItemName,
        i.Description,
        (od.Quantity * od.UnitAmount) AS TotalAmount
    FROM OrderDetails od
    INNER JOIN Items i ON od.ItemID = i.ItemID
    WHERE od.OrderID = @OrderID;
END;
GO

-- ====================================================
-- Display Table Information
-- ====================================================
PRINT 'Database created successfully!';
PRINT '================================';
PRINT 'Tables created:';
PRINT '- Users (3 records)';
PRINT '- Items (20 records)';
PRINT '- Agents (15 records)';
PRINT '- Orders (25 records)';
PRINT '- OrderDetails (35 records)';
PRINT '';
PRINT 'Views created:';
PRINT '- vw_OrderSummary';
PRINT '- vw_BestSellingItems';
PRINT '- vw_AgentPerformance';
PRINT '';
PRINT 'Stored Procedures created:';
PRINT '- sp_GetOrderDetails';
PRINT '';
PRINT 'Default Login Credentials:';
PRINT 'Admin: admin / admin123';
PRINT 'User: john_doe / user123';
GO

