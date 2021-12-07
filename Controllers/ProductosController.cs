using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TpFinalJoaquinGaido.Data;
using TpFinalJoaquinGaido.Models;

namespace TpFinalJoaquinGaido.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;

        public ProductosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Productos
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? busquedaNombre, int? CategoriaId )
        {


            var appDbcontexto = await _context.Productos.Include(a => a.Marca).Include(a => a.Categoria).Include(a => a.Proveedor).ToListAsync();
            if (!string.IsNullOrEmpty(busquedaNombre))
            {
                appDbcontexto = appDbcontexto.Where(a => a.Nombre.Contains(busquedaNombre)).ToList();
            }
            if (CategoriaId.HasValue)
            {
                appDbcontexto = appDbcontexto.Where(a => a.CategoriaId == CategoriaId).ToList();
            }


            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", CategoriaId);

            return View(appDbcontexto);
        }

        // GET: Productos/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["idProveedores"] = new SelectList(_context.Proveedores, "Id", "Nombre");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Descripcion");
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Nombre, Precio, Descripcion, Imagen, Marcaid, CategoriaId, ProveedorId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if(archivos != null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    var pathDestino = Path.Combine(env.WebRootPath, "images");
                    if(archivoFoto.Length > 0)
                    {
                        var archivoDestino = Guid.NewGuid().ToString();
                        archivoDestino = archivoDestino.Replace("-", "");
                        archivoDestino += Path.GetExtension(archivoFoto.FileName);
                        var rutaDestino = Path.Combine(pathDestino, archivoDestino);

                        using (var filestream = new FileStream(rutaDestino, FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            producto.Imagen = archivoDestino;
                        };
                    }

                }
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idProveedores"] = new SelectList(_context.Proveedores, "Id", "Nombre", producto.ProveedorId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Descripcion", producto.Marcaid);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            ViewData["idProveedores"] = new SelectList(_context.Proveedores, "Id", "Nombre");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Descripcion");
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nombre, Precio, Descripcion, Imagen, Marcaid, CategoriaId, ProveedorId")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(producto.Id))
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
            ViewData["idProveedores"] = new SelectList(_context.Proveedores, "Id", "Nombre", producto.ProveedorId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Descripcion", producto.Marcaid);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", producto.CategoriaId);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productos = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
