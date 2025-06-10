using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Proveedor
{
    public int Id { get; set; }

    public long Nit { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Representante { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}
