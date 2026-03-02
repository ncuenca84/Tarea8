using System;
using System.Collections.Generic;
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
    public class TareasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tareas.Include(t => t.Proyecto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Proyecto)
                .FirstOrDefaultAsync(m => m.TareaId == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            ViewData["ProyectoId"] = new SelectList(
                _context.Proyectos,
                "ProyectoId",
                "Nombre"
            );

            return View(new TareaCreateVM
            {
                FechaEntrega = DateTime.Now
            });
        }


        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed(TareaCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ProyectoId"] = new SelectList(
                    _context.Proyectos,
                    "ProyectoId",
                    "Nombre",
                    vm.ProyectoId
                );
                return View(vm);
            }

            var tarea = new Tarea
            {
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                FechaEntrega = vm.FechaEntrega,
                Estado = vm.Estado,
                ProyectoId = vm.ProyectoId
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "ProyectoId", "ProyectoId", tarea.ProyectoId);
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TareaId,Nombre,Descripcion,FechaEntrega,Estado,ProyectoId")] Tarea tarea)
        {
            if (id != tarea.TareaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.TareaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "ProyectoId", "ProyectoId", tarea.ProyectoId);
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Proyecto)
                .FirstOrDefaultAsync(m => m.TareaId == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.TareaId == id);
        }
    }
}
