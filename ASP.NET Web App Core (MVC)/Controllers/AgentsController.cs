using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Web_App_Core__MVC_.Data;
using ASP.NET_Web_App_Core__MVC_.Models;

namespace ASP.NET_Web_App_Core__MVC_.Controllers
{
    public class AgentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agents
        public async Task<IActionResult> Index(string searchString)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["CurrentFilter"] = searchString;

            var agents = from a in _context.Agents select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                agents = agents.Where(a => a.AgentName.Contains(searchString) 
                                        || a.Address.Contains(searchString)
                                        || a.Email!.Contains(searchString));
            }

            return View(await agents.OrderBy(a => a.AgentName).ToListAsync());
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return NotFound();

            var agent = await _context.Agents
                .Include(a => a.Orders)
                    .ThenInclude(o => o.OrderDetails)
                .FirstOrDefaultAsync(m => m.AgentID == id);

            if (agent == null) return NotFound();

            return View(agent);
        }

        // GET: Agents/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Agents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgentID,AgentName,Address,PhoneNumber,Email")] Agent agent)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                agent.CreatedDate = DateTime.Now;
                _context.Add(agent);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Agent created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return NotFound();

            var agent = await _context.Agents.FindAsync(id);
            if (agent == null) return NotFound();

            return View(agent);
        }

        // POST: Agents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgentID,AgentName,Address,PhoneNumber,Email,CreatedDate")] Agent agent)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id != agent.AgentID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agent);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Agent updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.AgentID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null) return NotFound();

            var agent = await _context.Agents
                .FirstOrDefaultAsync(m => m.AgentID == id);
            if (agent == null) return NotFound();

            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var agent = await _context.Agents.FindAsync(id);
            if (agent != null)
            {
                _context.Agents.Remove(agent);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Agent deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.AgentID == id);
        }
    }
}

