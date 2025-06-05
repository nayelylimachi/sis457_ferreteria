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
                contexto.VentaDetalle.Add(detalle);
                return contexto.SaveChanges();
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
