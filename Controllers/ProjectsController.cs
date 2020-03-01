using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Vacation_Manager.Data;
using Project_Vacation_Manager.Models;

namespace Project_Vacation_Manager.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly Project_Vacation_ManagerContext _context;
        public ProjectsController(Project_Vacation_ManagerContext context)
        {_context = context;}
        public async Task<IActionResult> Index()
        {return View(await _context.Project.ToListAsync());}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {return NotFound();}
            var project = await _context.Project.FirstOrDefaultAsync(m => m.Id == id);
            project.Teams = _context.Team.Where(x => x.ProjectName == project.Title).ToList();          
            if (project == null)
            {return NotFound();}
            return View(project);
        }
        [Authorize(Roles = "CEO")]
        public IActionResult Create()
        {return View();}

        [Authorize(Roles = "CEO")]
        public RedirectToActionResult DeleteTeam(Team teamm)
        { return RedirectToAction(nameof(Index)); }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Teams")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {return NotFound();}

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {return NotFound();}
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Teams")] Project project)
        {
            if (id != project.Id)
            {return NotFound();}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {return NotFound();}
                    else
                    {throw;}
                }
                return RedirectToAction(nameof(Index));
            }return View(project);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {return NotFound();}
            var project = await _context.Project.FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {return NotFound();}
            return View(project);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ProjectExists(int id)
        {return _context.Project.Any(e => e.Id == id);}
    }
}
