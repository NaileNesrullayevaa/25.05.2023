using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TownWebApp.DataContext;
using TownWebApp.Models;
using TownWebApp.ViewModels.IntroductionViewModels;

namespace TownWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IntroductionController : Controller
    {
        private readonly TownDbContext _townDbContext;
        

        public IntroductionController(TownDbContext townDbContext)
        {
            _townDbContext = townDbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Introduction> introductions = await _townDbContext.Introductions.ToListAsync();
            IntroductionIndexVM indexVM = new IntroductionIndexVM()
            {
                Introductions = introductions
            };
            return View(indexVM);
        }
        public async Task<IActionResult> Create()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IntroductionCreateVM newIntroduction)
        {
            if (!ModelState.IsValid)
            {
                return View(newIntroduction);
            }
            Introduction introduction = new Introduction()
            {
                IconName = newIntroduction.IconName,
                Title = newIntroduction.Title,
                Description = newIntroduction.Description,
            };
            await _townDbContext.Introductions.AddAsync(introduction);
            await _townDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async  Task<IActionResult>Detail(int id)
        {
            Introduction? introduction=await _townDbContext.Introductions.FindAsync(id);
            if (introduction == null)
            {
                return NotFound();
            }
            return View(introduction);

        }

        public async Task<IActionResult>Edit(int id)
        {
            Introduction introduction=await _townDbContext.Introductions.FindAsync(id); 
            if(introduction== null)
            {
                return NotFound();
            }
            IntroductionEditVM editVM=new IntroductionEditVM()
            {
                IconName=introduction.IconName,
                Description=introduction.Description,   
                Title=introduction.Title,
            };
            return View(editVM);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(int id,IntroductionEditVM editVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }
            Introduction? introduction=await _townDbContext.Introductions.FindAsync(id);
            if(introduction == null)
            {
                return NotFound();
            }
            introduction.Title = editVM.Title;
            introduction.IconName = editVM.IconName;
            introduction.Description=editVM.Description;
            await _townDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }



























        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            Introduction? introduction = await _townDbContext.Introductions.FindAsync(id);
            if (introduction == null)
            {
                return NotFound();
            }
            _townDbContext.Introductions.Remove(introduction);
            await _townDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
