using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using usuarios.Data;
using usuarios.Models;
using usuarios.ViewModels;

namespace usuarios.Controllers
{
    public class ProyectosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProyectosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var proyectos = _context.Proyectos
                .Include(p => p.Categoria)
                .Include(p => p.Cliente);

            return View(await proyectos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var proyecto = await _context.Proyectos
                .Include(p => p.Categoria)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.ProyectoId == id);

            if (proyecto == null)
                return NotFound();

            return View(proyecto);
        }

        public IActionResult Create()
        {
            CargarCombos();
            return View(new ProyectoCreateVM
            {
                FechaInicio = DateTime.Now
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed(ProyectoCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                CargarCombos();
                return View(vm);
            }

            var proyecto = new Proyecto
            {
                Nombre = vm.Nombre,
                FechaInicio = vm.FechaInicio,
                Estado = vm.Estado,
                Presupuesto = vm.Presupuesto,
                ClienteId = vm.ClienteId,
                CategoriaId = vm.CategoriaId
            };

            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
                return NotFound();

            CargarCombos(proyecto);
            return View(proyecto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proyecto proyecto)
        {
            if (id != proyecto.ProyectoId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                CargarCombos(proyecto);
                return View(proyecto);
            }

            _context.Update(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var proyecto = await _context.Proyectos
                .Include(p => p.Categoria)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.ProyectoId == id);

            if (proyecto == null)
                return NotFound();

            return View(proyecto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void CargarCombos(Proyecto proyecto = null)
        {
            ViewData["ClienteId"] = new SelectList(
                _context.Clientes,
                "ClienteId",
                "Nombre",
                proyecto?.ClienteId
            );

            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias,
                "CategoriaId",
                "Nombre",
                proyecto?.CategoriaId
            );
        }
    }
}
