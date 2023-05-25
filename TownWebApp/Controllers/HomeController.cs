using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TownWebApp.DataContext;
using TownWebApp.Models;
using TownWebApp.ViewModels;

namespace TownWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TownDbContext _townDbContext;

        public HomeController(TownDbContext townDbContext)
        {
            _townDbContext = townDbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Introduction> introductions = await _townDbContext.Introductions.ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Introductions = introductions,
            };
            return View(homeVM);
        }

       
    }
}