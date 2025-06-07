using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class ProductoCln
    {
        public static int insertar(Producto producto)
        {
            using (var contexto = new FerreteriaEntities())
            {
                contexto.Producto.Add(producto);
                contexto.SaveChanges();
                return producto.id;
            }
        }

        public static int actualizar(Producto producto)
        {
            using (var contexto = new FerreteriaEntities())
            {
                var existente = contexto.Producto.Find(producto.id);
                existente.codigo= producto.codigo;
                existente.descripcion = producto.descripcion;
                existente.precioVenta = producto.precioVenta;
                existente.saldo = producto.saldo;
                existente.unidadMedida = producto.unidadMedida;
                existente.idCategoria = producto.idCategoria;
                existente.idProveedor = producto.idProveedor;
                existente.usuarioRegistro = producto.usuarioRegistro;
                return contexto.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var contexto = new FerreteriaEntities())
            {
                var producto = contexto.Producto.Find(id);
                producto.estado = -1;
                producto.usuarioRegistro = usuario;
                return contexto.SaveChanges();
            }
        }

        public static Producto obtenerUno(int id)
        {
            using (var contexto = new FerreteriaEntities())
            {
                return contexto.Producto.Find(id);
            }
        }

        public static List<Producto> listar()
        {
            using (var contexto = new FerreteriaEntities())
            {
                return contexto.Producto.Where(x => x.estado != -1).ToList();
            }
        }
        public static List<paProductoListar_Result> listarPa(string parametro)
        {
            using (var contexto = new FerreteriaEntities())
            {
                return contexto.paProductoListar(parametro).ToList();
            }
        }
        public static Producto obtener(int idProducto)
        {
            using (var contexto = new FerreteriaEntities())
            {
                return contexto.Producto.FirstOrDefault(p => p.id == idProducto && p.estado == 1);
            }
        }
        // En tu clase ProductoCln.cs
        public static int actualizarSaldo(Producto producto)
        {
            using (var contexto = new FerreteriaEntities())
            {
                // Adjuntar el objeto al contexto y marcarlo como modificado
                contexto.Producto.Attach(producto);
                contexto.Entry(producto).Property(p => p.saldo).IsModified = true;
                contexto.Entry(producto).Property(p => p.usuarioRegistro).IsModified = true; 

                try
                {
                    return contexto.SaveChanges(); // Devuelve las filas afectadas (1 si todo bien)
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar saldo de producto: {ex.Message}");
                    // Opcional: Console.WriteLine(ex.InnerException?.Message);
                    return 0; // Indica fallo
                }
            }
        }
    }
}
