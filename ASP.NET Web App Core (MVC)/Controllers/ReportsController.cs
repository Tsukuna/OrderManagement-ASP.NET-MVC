using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Web_App_Core__MVC_.Data;
using ASP.NET_Web_App_Core__MVC_.Models;

namespace ASP.NET_Web_App_Core__MVC_.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports/BestItems
        public async Task<IActionResult> BestItems()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bestItems = await _context.OrderDetails
                .Include(od => od.Item)
                .GroupBy(od => new { od.ItemID, od.Item!.ItemName, od.Item.Price })
                .Select(g => new
                {
                    ItemID = g.Key.ItemID,
                    ItemName = g.Key.ItemName,
                    Price = g.Key.Price,
                    TotalQuantity = g.Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(od => od.Quantity * od.UnitAmount),
                    OrderCount = g.Count()
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(10)
                .ToListAsync();

            return View(bestItems);
        }

        // GET: Reports/ItemsPurchasedByCustomer
        public async Task<IActionResult> ItemsPurchasedByCustomer(int? userId, DateTime? startDate, DateTime? endDate)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["Users"] = new SelectList(await _context.Users.ToListAsync(), "UserID", "UserName");
            ViewData["SelectedUser"] = userId;
            ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");

            var query = _context.OrderDetails
                .Include(od => od.Item)
                .Include(od => od.Order)
                    .ThenInclude(o => o.User)
                .AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(od => od.Order!.UserID == userId.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(od => od.Order!.OrderDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(od => od.Order!.OrderDate <= endDate.Value);
            }

            var result = await query
                .Select(od => new
                {
                    UserName = od.Order!.User!.UserName,
                    ItemName = od.Item!.ItemName,
                    OrderDate = od.Order.OrderDate,
                    Quantity = od.Quantity,
                    UnitAmount = od.UnitAmount,
                    TotalAmount = od.Quantity * od.UnitAmount
                })
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();

            return View(result);
        }

        // GET: Reports/CustomerPurchases
        public async Task<IActionResult> CustomerPurchases(DateTime? startDate, DateTime? endDate)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");

            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= endDate.Value);
            }

            // Get the orders first, then calculate in memory to avoid nested aggregates
            var orders = await query.ToListAsync();

            var result = orders
                .GroupBy(o => new { o.UserID, o.User!.UserName, o.User.Email })
                .Select(g => new
                {
                    UserName = g.Key.UserName,
                    Email = g.Key.Email,
                    OrderCount = g.Count(),
                    TotalItems = g.SelectMany(o => o.OrderDetails!).Sum(od => od.Quantity),
                    TotalSpent = g.Sum(o => o.TotalAmount),
                    LastOrderDate = g.Max(o => o.OrderDate)
                })
                .OrderByDescending(x => x.TotalSpent)
                .ToList();

            return View(result);
        }

        // GET: Reports/AgentPerformance
        public async Task<IActionResult> AgentPerformance(DateTime? startDate, DateTime? endDate)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");

            var query = _context.Orders
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= endDate.Value);
            }

            // Get the orders first, then calculate in memory to avoid nested aggregates
            var orders = await query.ToListAsync();

            var result = orders
                .GroupBy(o => new { o.AgentID, o.Agent!.AgentName, o.Agent.Email })
                .Select(g => new
                {
                    AgentName = g.Key.AgentName,
                    Email = g.Key.Email,
                    OrderCount = g.Count(),
                    TotalItems = g.SelectMany(o => o.OrderDetails!).Sum(od => od.Quantity),
                    TotalRevenue = g.Sum(o => o.TotalAmount),
                    CompletedOrders = g.Count(o => o.OrderStatus == "Completed")
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToList();

            return View(result);
        }

        // GET: Reports/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var dashboardData = new
            {
                TotalOrders = await _context.Orders.CountAsync(),
                TotalRevenue = await _context.Orders.SumAsync(o => o.TotalAmount),
                TotalItems = await _context.Items.CountAsync(),
                TotalAgents = await _context.Agents.CountAsync(),
                PendingOrders = await _context.Orders.CountAsync(o => o.OrderStatus == "Pending"),
                CompletedOrders = await _context.Orders.CountAsync(o => o.OrderStatus == "Completed"),
                RecentOrders = await _context.Orders
                    .Include(o => o.Agent)
                    .Include(o => o.User)
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .ToListAsync(),
                TopItems = await _context.OrderDetails
                    .Include(od => od.Item)
                    .GroupBy(od => new { od.ItemID, od.Item!.ItemName })
                    .Select(g => new
                    {
                        ItemName = g.Key.ItemName,
                        TotalQuantity = g.Sum(od => od.Quantity)
                    })
                    .OrderByDescending(x => x.TotalQuantity)
                    .Take(5)
                    .ToListAsync()
            };

            return View(dashboardData);
        }
    }
}

