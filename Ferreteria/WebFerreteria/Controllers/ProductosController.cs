using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFerreteria.Models;

namespace WebFerreteria.Controllers
{
    public class ProductosController : Controller
    {
        private readonly FinalFerreteriaContext _context;

        public ProductosController(FinalFerreteriaContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var finalFerreteriaContext = _context.Producto.Include(p => p.IdCategoriaNavigation).Include(p => p.IdProveedorNavigation)
                .Where(x=>x.Estado != -1)
                .OrderByDescending(x => x.Descripcion);
            return View(await finalFerreteriaContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            var categorias = _context.Categoria
    .Where(c => c.Estado != -1)
    .OrderBy(c => c.Nombre)
    .ToList();
            categorias.Insert(0, new Categoria { Id = 0, Nombre = "Seleccione una categoría" });
            ViewData["IdCategoria"] = new SelectList(categorias, "Id", "Nombre");

            var proveedores = _context.Proveedor
                .Where(p => p.Estado != -1)
                .OrderBy(p => p.RazonSocial)
                .ToList();
            proveedores.Insert(0, new Proveedor { Id = 0, RazonSocial = "Seleccione un proveedor" });
            ViewData["IdProveedor"] = new SelectList(proveedores, "Id", "RazonSocial");

            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (!string.IsNullOrEmpty(producto.Codigo) && !string.IsNullOrEmpty(producto.Descripcion) && producto.PrecioVenta > 0 && producto.Saldo >= 0 && !string.IsNullOrEmpty(producto.UnidadMedida)
                && !string.IsNullOrEmpty(producto.Descripcion))
            {
                producto.UsuarioRegistro = "admin";
                producto.FechaRegistro = DateTime.Now;
                producto.Estado = 1;
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "RazonSocial", producto.IdProveedor);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "RazonSocial", producto.IdProveedor);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCategoria,IdProveedor,Codigo,Descripcion,PrecioVenta,Saldo,UnidadMedida,UsuarioRegistro,FechaRegistro,Estado")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(producto.Codigo) && !string.IsNullOrEmpty(producto.Descripcion) &&
        producto.PrecioVenta > 0 && producto.Saldo >= 0 && !string.IsNullOrEmpty(producto.UnidadMedida))
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id", "RazonSocial", producto.IdProveedor);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdProveedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}
