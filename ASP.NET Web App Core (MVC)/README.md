# Order Management System - Exercise 4
## ASP.NET Core MVC Web Application (.NET 8.0)

A comprehensive order management system built with ASP.NET Core MVC, Entity Framework Core, and SQL Server.

## 📋 Features

### ✅ Complete CRUD Operations
- **Items/Products Management**: Add, edit, delete, and view product inventory
- **Agents Management**: Manage supplier/agent information
- **Orders Management**: Create orders with multiple items, track order status
- **Users Management**: User authentication and registration

### 📊 Advanced Reporting
- Dashboard with key metrics
- Best selling items report
- Items purchased by customer
- Customer purchases summary
- Agent performance analysis

### 🔐 Authentication & Authorization
- User login/logout
- Session management
- Role-based access (Admin/User)

### 🎨 Modern UI
- Bootstrap 5 responsive design
- Bootstrap Icons integration
- Mobile-friendly interface
- Print-ready order invoices

## 🗄️ Database Schema

### Tables:
1. **Users** - User accounts and credentials
2. **Items** - Product catalog
3. **Agents** - Supplier/Agent information
4. **Orders** - Order headers
5. **OrderDetails** - Order line items

### Relationships:
- Users ➔ Orders (1:N)
- Agents ➔ Orders (1:N)
- Orders ➔ OrderDetails (1:N)
- Items ➔ OrderDetails (1:N)

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB, Express, or Full)
- Visual Studio 2022 or VS Code
- Git

### Installation Steps

#### 1. Clone the Repository
```bash
git clone <your-repository-url>
cd "ASP.NET Web App Core (MVC)"
```

#### 2. Restore NuGet Packages
```bash
dotnet restore
```

#### 3. Update Database Connection String
Edit `appsettings.json` and update the connection string if needed:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=OrderManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

#### 4. Apply Database Migrations

**Option A: Using Entity Framework Migrations (Recommended)**
```bash
# Install EF Core tools globally (if not already installed)
dotnet tool install --global dotnet-ef

# Create initial migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

**Option B: Using SQL Script**
```bash
# Execute the SQL script in Database/CreateDatabase.sql
# Using SSMS or command line:
sqlcmd -S (localdb)\mssqllocaldb -i Database/CreateDatabase.sql
```

#### 5. Run the Application
```bash
dotnet run
```

Or press F5 in Visual Studio.

The application will start at: `https://localhost:5001` or `http://localhost:5000`

## 👤 Default Login Credentials

### Admin Account:
- **Username**: admin
- **Password**: admin123

### User Accounts:
- **Username**: john_doe | **Password**: user123
- **Username**: jane_smith | **Password**: user123

## 📦 NuGet Packages Used

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
<PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="8.0.0" />
```

## 🏗️ Project Structure

```
ASP.NET Web App Core (MVC)/
│
├── Controllers/
│   ├── AccountController.cs      # Login/Register
│   ├── HomeController.cs         # Dashboard
│   ├── ItemsController.cs        # Products CRUD
│   ├── AgentsController.cs       # Agents CRUD
│   ├── OrdersController.cs       # Orders CRUD
│   └── ReportsController.cs      # Analytics
│
├── Models/
│   ├── Item.cs                   # Product model
│   ├── Agent.cs                  # Agent model
│   ├── Order.cs                  # Order model
│   ├── OrderDetail.cs            # Order detail model
│   ├── User.cs                   # User model
│   └── ErrorViewModel.cs
│
├── Data/
│   └── ApplicationDbContext.cs   # EF Core DbContext
│
├── Views/
│   ├── Account/                  # Login/Register views
│   ├── Items/                    # Product views
│   ├── Agents/                   # Agent views
│   ├── Orders/                   # Order views
│   ├── Reports/                  # Report views
│   ├── Home/                     # Home views
│   └── Shared/                   # Layout & shared views
│
├── wwwroot/                      # Static files (CSS, JS, images)
├── Database/
│   └── CreateDatabase.sql        # SQL creation script
│
├── Program.cs                    # Application entry point
├── appsettings.json             # Configuration
└── README.md                    # This file
```

## 🎯 Key Features Implementation

### 1. Order Creation with Multiple Items
- Dynamic item selection
- Real-time total calculation
- Stock validation
- Agent assignment

### 2. Reports & Analytics
- **Best Items**: Top 10 products by sales
- **Customer Report**: Purchase history by customer
- **Agent Performance**: Sales performance metrics
- **Dashboard**: System overview and statistics

### 3. Search & Filter
- Items: Search by name, size, description
- Agents: Search by name, address, email
- Orders: Filter by status and agent

### 4. Order Management
- View order details with items
- Edit order status and notes
- Print-friendly invoice
- Delete orders with confirmation

## 📊 Sample Data Included

- **20 Products** (Electronics, gadgets, appliances)
- **15 Agents** (Various suppliers)
- **3 Users** (1 Admin, 2 regular users)
- **25 Orders** with multiple order details

## 🔧 Common Commands

### Entity Framework Commands
```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Generate SQL script
dotnet ef migrations script
```

### Build & Run
```bash
# Clean solution
dotnet clean

# Build solution
dotnet build

# Run application
dotnet run

# Run with watch (auto-reload)
dotnet watch run
```

## 📝 Exercise Requirements Completed

✅ **Exercise 4 (Code First Approach)**:
- Models defined with data annotations
- Entity Framework Core DbContext
- Migrations support
- Seed data included

✅ **Scenario Implementation**:
- Items, Agents, Orders, OrderDetails, Users tables
- 15-30 rows of sample data per table
- Complete CRUD operations
- Order management with multiple items

✅ **Advanced Features**:
- Login/Authentication
- Reports and filtering
- Best items report
- Items purchased by customers
- Customer purchases summary
- Print-ready invoices

## 🐛 Troubleshooting

### Database Connection Issues
```bash
# Check SQL Server service is running
# Test connection string in appsettings.json
# Ensure LocalDB is installed with Visual Studio
```

### Migration Errors
```bash
# Delete Migrations folder and try again
# Drop database and recreate
dotnet ef database drop
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Port Already in Use
```bash
# Change port in Properties/launchSettings.json
# Or kill the process using the port
```

## 📧 Contact & Support

**Course**: SE Lab Assignment 2 (Exercise 4)
**Framework**: ASP.NET Core MVC (.NET 8.0)
**Database**: SQL Server (Code First)

## 📄 License

This project is created for educational purposes as part of SE Lab Assignment 2.

---

## 🎓 Learning Outcomes

This project demonstrates:
- ASP.NET Core MVC architecture
- Entity Framework Core (Code First)
- CRUD operations
- Authentication & Session management
- Master-Detail relationships
- Reporting & Analytics
- Modern web UI development
- Bootstrap responsive design

**Happy Coding! 🚀**

