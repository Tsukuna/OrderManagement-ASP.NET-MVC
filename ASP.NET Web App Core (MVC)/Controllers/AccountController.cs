using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Web_App_Core__MVC_.Data;
using ASP.NET_Web_App_Core__MVC_.Models;

namespace ASP.NET_Web_App_Core__MVC_.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            // Redirect to home if already logged in
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            if (user.IsLocked)
            {
                ViewBag.Error = "Your account is locked. Please contact administrator.";
                return View();
            }

            // Update last login
            user.LastLoginDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // Set session
            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == user.UserName || u.Email == user.Email);

                if (existingUser != null)
                {
                    ViewBag.Error = "Username or email already exists.";
                    return View(user);
                }

                user.CreatedDate = DateTime.Now;
                user.Role = "User";
                user.IsLocked = false;
                _context.Add(user);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}

