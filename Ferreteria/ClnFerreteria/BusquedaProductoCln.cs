using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class BusquedaProductoCln
    {
        public static int insertar(VentaDetalle detalle)
        {
            using (var contexto = new FerreteriaEntities())
            {
                Console.WriteLine("--- Detalle de Venta a insertar ---");
                Console.WriteLine($"ID: {detalle.id}");
                Console.WriteLine($"ID Venta: {detalle.idVenta}");
                Console.WriteLine($"ID Producto: {detalle.idProducto}");
                Console.WriteLine($"Cantidad: {detalle.cantidad}");
                Console.WriteLine($"Precio Unitario: {detalle.precioUnitario}");
                Console.WriteLine($"Total: {detalle.total}");
                // Agrega también estas líneas si tus modelos las tienen:
                Console.WriteLine($"Usuario Registro: {detalle.usuarioRegistro}");
                Console.WriteLine($"Fecha Registro: {detalle.fechaRegistro}");
                Console.WriteLine($"Estado: {detalle.estado}");
                Console.WriteLine("-----------------------------------");

                contexto.VentaDetalle.Add(detalle);

                try
                {
                    int filasAfectadas = contexto.SaveChanges();
                    Console.WriteLine($"Detalle de venta guardado exitosamente. Nuevo ID generado: {detalle.id}");
                    return filasAfectadas;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    // ***** ESTA ES LA CLAVE PARA VER EL ERROR ESPECÍFICO *****
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.WriteLine($"Propiedad: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                    Console.WriteLine($"Error de validación de entidad: {ex.Message}");
                    return 0; // Indica que la inserción falló
                }
                catch (Exception ex)
                {
                    // Para otros tipos de errores (ej. de la BD, si la validación de EF pasa)
                    Console.WriteLine($"Error inesperado al guardar el detalle de venta: {ex.Message}");
                    // Si quieres ver el mensaje de error original de la base de datos (si aplica):
                    // Console.WriteLine($"Excepción interna: {ex.InnerException?.Message}");
                    return 0; // Indica que la inserción falló
                }
            }
        }

        public static List<VentaDetalle> listarPorVenta(int idVenta)
        {
            using (var contexto = new FerreteriaEntities())
            {
                return contexto.VentaDetalle
                    .Where(vd => vd.idVenta == idVenta)
                    .ToList();
            }
        }
    }
}
