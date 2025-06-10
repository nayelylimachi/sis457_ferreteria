using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Venta
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public DateOnly Fecha { get; set; }

    public int Transaccion { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalle { get; set; } = new List<VentaDetalle>();
}
