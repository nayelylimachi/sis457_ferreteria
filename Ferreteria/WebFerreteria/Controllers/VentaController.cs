using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFerreteria.Models;
using WebFerreteria.Models.ViewModels;

public class VentaController : Controller
{
    private readonly FinalFerreteriaContext _context;

    public VentaController(FinalFerreteriaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var ventas = await _context.Venta
            .Include(v => v.IdClienteNavigation)
            .Include(v => v.IdUsuarioNavigation)
            .OrderByDescending(v => v.Fecha)
            .ToListAsync();

        return View(ventas);
    }

    public async Task<IActionResult> Detalles(int id)
    {
        var venta = await _context.Venta
            .Include(v => v.IdClienteNavigation)
            .Include(v => v.IdUsuarioNavigation)
            .Include(v => v.VentaDetalle)
                .ThenInclude(d => d.IdProductoNavigation)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (venta == null)
        {
            return NotFound();
        }

        return View(venta);
    }


    // GET: Venta/Create
    public IActionResult Create()
    {
        ViewBag.Productos = _context.Producto
            .Where(p => p.Estado == 1 && p.Saldo > 0)
            .Select(p => new { p.Id, p.Descripcion, p.PrecioVenta, p.Saldo })
            .ToList();

        ViewBag.Clientes = _context.Cliente
            .Where(c => c.Estado == 1)
            .Select(c => new { c.Id, c.Nombre, c.Nit })
            .ToList();

        return View(new VentaViewModel());
    }

    // POST: Venta/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VentaViewModel model)
    {
        Console.WriteLine("Productos recibidos: " + model.Detalles.Count);
        foreach (var d in model.Detalles)
        {
            Console.WriteLine($"Producto: {d.IdProducto}, Cantidad: {d.Cantidad}");
        }
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Buscar o registrar cliente
            if (model.Cliente.Id > 0)
            {
                model.Cliente = await _context.Cliente.FindAsync(model.Cliente.Id);
            }
            else
            {
                var clienteExistente = _context.Cliente
                    .FirstOrDefault(c => c.Nit == model.Cliente.Nit && c.Nombre == model.Cliente.Nombre && c.Estado == 1);

                if (clienteExistente != null)
                {
                    model.Cliente = clienteExistente;
                }
                else
                {
                    model.Cliente.Estado = 1;
                    model.Cliente.UsuarioRegistro = User.Identity.Name;
                    model.Cliente.FechaRegistro = DateTime.Now;
                    _context.Cliente.Add(model.Cliente);
                    await _context.SaveChangesAsync();
                }
            }

            // 2. Guardar venta
            model.Venta.IdCliente = model.Cliente.Id;
            model.Venta.IdUsuario = 1; // reemplazar con usuario en sesión
            model.Venta.Fecha = DateOnly.FromDateTime(DateTime.Now);
            model.Venta.FechaRegistro = DateTime.Now;
            model.Venta.UsuarioRegistro = User.Identity.Name;
            model.Venta.Estado = 1;
            model.Venta.Transaccion = new Random().Next(100000, 999999);
            _context.Venta.Add(model.Venta);
            await _context.SaveChangesAsync();

            // 3. Guardar detalles y actualizar stock
            foreach (var d in model.Detalles)
            {
                d.IdVenta = model.Venta.Id;
                d.Total = d.Cantidad * d.PrecioUnitario;
                d.FechaRegistro = DateTime.Now;
                d.UsuarioRegistro = User.Identity.Name;
                d.Estado = 1;

                var producto = await _context.Producto.FindAsync(d.IdProducto);
                if (producto == null || producto.Saldo < d.Cantidad)
                {
                    ModelState.AddModelError("", "Stock insuficiente");
                    await transaction.RollbackAsync();
                    return View(model);
                }

                producto.Saldo -= d.Cantidad;
                _context.VentaDetalle.Add(d);
                _context.Producto.Update(producto);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            ModelState.AddModelError("", "Error al guardar: " + ex.Message);
            return View(model);
        }
    }
}
