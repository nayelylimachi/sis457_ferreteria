using CadFerreteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnFerreteria
{
    public class UsuarioCln
    {
        public static Usuario validar(string usuario, string contraseña)
        {
            using (var context = new FerreteriaEntities())
            {
                return context.Usuario
                    .Where(u => u.usuario1 == usuario && u.contraseña == contraseña)
                    .FirstOrDefault();
            }
        }
    }
}
