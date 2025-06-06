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
            // Rango mínimo permitido por SQL Server para datetime
            DateTime minSqlDate = new DateTime(1753, 1, 1);

            if (venta.fecha < minSqlDate)
                venta.fecha = DateTime.Now;
            if (venta.fechaRegistro < minSqlDate)
                venta.fechaRegistro = DateTime.Now;

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
