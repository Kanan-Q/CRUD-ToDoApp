using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            using (TestDbContext db = new())
            {
                var views = await db.Users.ToListAsync();
                return View(views);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            using (TestDbContext db = new())
            {
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            User? user;
            using (TestDbContext db = new())
            {
                if (await db.Users.AnyAsync(x => x.UserId == id))
                {
                    db.Users.Remove(new User { UserId = id.Value });
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> View(int? id)
        {
            if (!id.HasValue) return BadRequest();
            using (TestDbContext db = new())
            {
                var data = await db.Users.FindAsync(id.Value);
                if (data is null) return NotFound();
                return View(data);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();
            User? users;
            using(TestDbContext db = new())
            {
                users = await db.Users.FindAsync(id.Value);
                if (users is null) return NotFound();
                return View(users);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,User user)
        {
            User? users;
            if (!id.HasValue) return BadRequest();
            using (TestDbContext db = new())
            {
                users = await db.Users.FindAsync(id.Value);
                if(users is null) return NotFound();
                users.Title = user.Title;
                users.Description=user.Description;
                users.Deadline = user.Deadline;
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
