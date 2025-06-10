using System;
using System.Collections.Generic;

namespace WebFerreteria.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string CedulaIdentidad { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public string? Direccion { get; set; }

    public long? Celular { get; set; }

    public string? Cargo { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
