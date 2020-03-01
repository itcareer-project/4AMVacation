using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Vacation_Manager.Data;
using Project_Vacation_Manager.Models;

namespace Project_Vacation_Manager.Controllers
{
    public class VacationsController : Controller
    {
        private readonly Project_Vacation_ManagerContext _context;
        public VacationsController(Project_Vacation_ManagerContext context)
        { _context = context;}
        public async Task<IActionResult> Index()
        {return View(await _context.Vacation.ToListAsync());}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {return NotFound();}
            var vacation = await _context.Vacation.FirstOrDefaultAsync(m => m.Id == id);
            if (vacation == null)
            {return NotFound();}
            return View(vacation);
        }
        public IActionResult Create()
        {return View();}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User,StartVac,EndVac,RequestDate,Accepted")] Vacation vacation)
        {
            vacation.RequestDate = DateTime.Now;
            vacation.User = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _context.Add(vacation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }return View(vacation);
        }
        [Authorize(Roles = "CEO, Team Lead")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {return NotFound();}
            var vacation = await _context.Vacation.FindAsync(id);
            if (vacation == null)
            {return NotFound();}
            return View(vacation);
        }
        [HttpPost]
        [Authorize(Roles = "CEO")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User,StartVac,EndVac,RequestDate,Accepted")] Vacation vacation)
        {
            if (id != vacation.Id)
            {return NotFound();}
            if (ModelState.IsValid)
            {
                try
                {                    
                    _context.Vacation.Attach(vacation).Property(x => x.Accepted).IsModified = true;                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationExists(vacation.Id))
                    {return NotFound();}
                    else{throw;}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vacation);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {return NotFound();}
            var vacation = await _context.Vacation.FirstOrDefaultAsync(m => m.Id == id);
            if (vacation == null)
            {return NotFound();}
            return View(vacation);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacation = await _context.Vacation.FindAsync(id);
            _context.Vacation.Remove(vacation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool VacationExists(int id)
        {return _context.Vacation.Any(e => e.Id == id);}
    }
}
