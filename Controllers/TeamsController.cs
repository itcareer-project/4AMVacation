using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Vacation_Manager.Data;
using Project_Vacation_Manager.Models;

namespace Project_Vacation_Manager.Controllers
{
    public class TeamsController : Controller
    {
        private readonly Project_Vacation_ManagerContext _context;
        public TeamsController(Project_Vacation_ManagerContext context)
        {_context = context;}
        public async Task<IActionResult> Index()
        {return View(await _context.Team.ToListAsync());}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {return NotFound();}
            var team = await _context.Team.FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {return NotFound();}
            return View(team);
        }
        [Authorize(Roles = "CEO")]
        public IActionResult Create()
        {return View();}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ProjectName,Developers,TeamLeader")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {return NotFound();}
            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {return NotFound();}
            return View(team);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ProjectName,Developers,TeamLeader")] Team team)
        {
            if (id != team.Id)
            {return NotFound();}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
                    {return NotFound();}
                    else
                    {throw;}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {return NotFound();}
            var team = await _context.Team.FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {return NotFound();}
            return View(team);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Team.FindAsync(id);
            _context.Team.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool TeamExists(int id)
        {return _context.Team.Any(e => e.Id == id);}
    }
}
