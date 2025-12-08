using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Web_App_Core__MVC_.Data;
using ASP.NET_Web_App_Core__MVC_.Models;

namespace ASP.NET_Web_App_Core__MVC_.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string status, int? agentId)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["CurrentStatus"] = status;
            ViewData["CurrentAgent"] = agentId;
            ViewData["Agents"] = new SelectList(await _context.Agents.ToListAsync(), "AgentID", "AgentName");

            var orders = _context.Orders
                .Include(o => o.Agent)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (!String.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.OrderStatus == status);
            }

            if (agentId.HasValue)
            {
                orders = orders.Where(o => o.AgentID == agentId.Value);
            }

            return View(await orders.OrderByDescending(o => o.OrderDate).ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Agent)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                .FirstOrDefaultAsync(m => m.OrderID == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["Agents"] = new SelectList(_context.Agents, "AgentID", "AgentName");
            ViewData["Items"] = _context.Items.ToList();
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order, List<int> itemIds, List<int> quantities)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                if (itemIds == null || itemIds.Count == 0)
                {
                    ModelState.AddModelError("", "Please select at least one item.");
                    ViewData["Agents"] = new SelectList(_context.Agents, "AgentID", "AgentName", order.AgentID);
                    ViewData["Items"] = _context.Items.ToList();
                    return View(order);
                }

                order.UserID = HttpContext.Session.GetInt32("UserID");
                order.OrderDate = DateTime.Now;
                order.OrderStatus = "Pending";

                var orderDetails = new List<OrderDetail>();
                decimal totalAmount = 0;

                for (int i = 0; i < itemIds.Count; i++)
                {
                    if (quantities[i] > 0)
                    {
                        var item = await _context.Items.FindAsync(itemIds[i]);
                        if (item != null)
                        {
                            var detail = new OrderDetail
                            {
                                ItemID = itemIds[i],
                                Quantity = quantities[i],
                                UnitAmount = item.Price
                            };
                            orderDetails.Add(detail);
                            totalAmount += detail.TotalAmount;
                        }
                    }
                }

                order.TotalAmount = totalAmount;
                order.OrderDetails = orderDetails;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Order created successfully!";
                return RedirectToAction(nameof(Details), new { id = order.OrderID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating order: {ex.Message}");
                ViewData["Agents"] = new SelectList(_context.Agents, "AgentID", "AgentName", order.AgentID);
                ViewData["Items"] = _context.Items.ToList();
                return View(order);
            }
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null) return NotFound();

            ViewData["Agents"] = new SelectList(_context.Agents, "AgentID", "AgentName", order.AgentID);
            ViewData["StatusList"] = new SelectList(new[] { "Pending", "Processing", "Shipped", "Completed", "Cancelled" }, order.OrderStatus);

            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id != order.OrderID) return NotFound();

            try
            {
                var existingOrder = await _context.Orders.FindAsync(id);
                if (existingOrder != null)
                {
                    existingOrder.OrderStatus = order.OrderStatus;
                    existingOrder.Notes = order.Notes;
                    existingOrder.AgentID = order.AgentID;

                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Order updated successfully!";
                }
                return RedirectToAction(nameof(Details), new { id = order.OrderID });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.OrderID))
                    return NotFound();
                else
                    throw;
            }
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Agent)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(m => m.OrderID == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Order deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Print/5
        public async Task<IActionResult> Print(int? id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Agent)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                .FirstOrDefaultAsync(m => m.OrderID == id);

            if (order == null) return NotFound();

            return View(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}

