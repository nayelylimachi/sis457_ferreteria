using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class VentaCln
    {
        public static int insertar(Venta venta)
        {
            using (var contexto = new FerreteriaEntities())
            {
                contexto.Venta.Add(venta);
                contexto.SaveChanges();
                return venta.id;
            }
        }
        public static List<Venta> listar()
        {
            using (var contexto = new FerreteriaEntities())
            {
                return contexto.Venta.ToList();
            }
        }
    }
}
