using SmartTaskManagement.Data;
using SmartTaskManagement.Models;
using SmartTaskManagement.Models.ViewModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace SmartTaskManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController()
        {
            _db = new ApplicationDbContext();
        }

        // ================= LOGIN =================
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string hashedPassword = ComputeHash(model.Password);

            var dbUser = _db.Users.FirstOrDefault(u => u.Email.Trim().ToLower() == model.EmailAddress.Trim().ToLower());
            System.Diagnostics.Debug.WriteLine($"Login attempt for {model.EmailAddress}");
            System.Diagnostics.Debug.WriteLine($"Computed hash: {hashedPassword}");
            System.Diagnostics.Debug.WriteLine($"DB stored hash: {(dbUser == null ? "<not found>" : dbUser.PasswordHash)}");

            var user = _db.Users.FirstOrDefault(u =>
                u.Email.Trim().ToLower() == model.EmailAddress.Trim().ToLower() &&
                u.PasswordHash == hashedPassword);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password. Please try again or sign up first.");
                return View(model);
            }

            // ✅ Set session
            Session["UserId"] = user.UserId;
            Session["UserName"] = user.Name;
            Session["UserRole"] = user.Role;
            Session["Email"] = user.Email;
            System.Diagnostics.Debug.WriteLine($"Login attempt by {user.Role}");

            // ✅ Redirect to dashboard by role
            if (user.Role == "Manager") return RedirectToAction("AssignTask", "Manager"); 
            else return RedirectToAction("ViewTasks", "Employee");

        }

        // ================= SIGN UP =================
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check if email already exists
            if (_db.Users.Any(u => u.Email.Trim().ToLower() == model.Email.Trim().ToLower()))
            {
                ModelState.AddModelError("", "Email already registered. Please log in instead.");
                return View(model);
            }

            // Hash password
            string hashedPassword = ComputeHash(model.Password);

            var newUser = new User
            {
                Name = model.Name,
                Email = model.Email.Trim().ToLower(),
                PasswordHash = hashedPassword,
                Role = string.IsNullOrEmpty(model.Role) ? "Employee" : model.Role,
                Department = model.Department
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Account created successfully! Please log in.";
            return RedirectToAction("Login");
        }

        // ================= LOGOUT =================
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        // ================= HELPER =================
        private string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash); // ✅ Base64 format
            }
        }
    }
}
