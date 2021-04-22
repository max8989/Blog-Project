using Blog_Project.Data;
using Blog_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // IMPLEMENT
        public async Task<IActionResult> Index(int? categoryId)
        {
            //else get all post
            var applicationDbContext = _context.Posts
                .Include(c => c.Category)
                .OrderBy(d => d.DateCreated);

            if (categoryId != null)
            {
                return View(await applicationDbContext.Where(c => c.CategoryId == categoryId).ToListAsync());
            }

            return View(await applicationDbContext.ToListAsync());

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
