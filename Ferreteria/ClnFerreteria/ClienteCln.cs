using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class ClienteCln
    {
        public static Cliente bucar(string nit)
        {
            using (var contexto = new FerreteriaEntities())
            {
                return contexto.Cliente
                    .Where(c => c.nit == nit && c.estado != -1)
                    .FirstOrDefault();
            }
        }

        public static int insertar(Cliente cliente)
        {
            using (var contexto = new FerreteriaEntities())
            {
                contexto.Cliente.Add(cliente);
                contexto.SaveChanges();
                return cliente.id;
            }
        }

        public static int actualizar(Cliente cliente)
        {
            using (var contexto = new FerreteriaEntities())
            {
                var existente = contexto.Cliente.Find(cliente.id);
                existente.nit = cliente.nit;
                existente.nombre = cliente.nombre;
                existente.usuarioRegistro = cliente.usuarioRegistro;
                return contexto.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var contexto = new FerreteriaEntities())
            {
                var cliente = contexto.Cliente.Find(id);
                cliente.estado = -1;
                cliente.usuarioRegistro = usuario;
                return contexto.SaveChanges();
            }
        }
        public static Cliente obtenerUno(int id)
        {
            using (var contexto = new FerreteriaEntities())
            {
                return contexto.Cliente.Find(id);
            }
        }

        public static List<Cliente> listar()
        {
            using (var contexto = new FerreteriaEntities())
            {
                return contexto.Cliente.Where(x => x.estado != -1).ToList();
            }
        }
    }
}

