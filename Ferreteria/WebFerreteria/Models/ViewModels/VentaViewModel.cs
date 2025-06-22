using System.Collections.Generic;

namespace WebFerreteria.Models.ViewModels
{
    public class VentaViewModel
    {
        public Venta Venta { get; set; } = new Venta();
        public Cliente Cliente { get; set; } = new Cliente();
        public List<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
        public decimal Total { get; set; }
        public decimal Pago { get; set; }
        public decimal Cambio => Pago - Total;
    }
}
