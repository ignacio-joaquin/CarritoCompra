using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
public class ItemCarrito
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public ItemCarrito(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }

        public decimal CalcularSubtotal()
        {
            decimal subtotal = Producto.Precio * Cantidad;
            
            // Aplicar descuento del 15% si se compran 5 o más unidades
            if (Cantidad >= 5)
            {
                subtotal *= 0.85m; // 15% de descuento
            }
            
            return subtotal;
        }

        public override string ToString()
        {
            decimal subtotal = CalcularSubtotal();
            string descuento = Cantidad >= 5 ? " (15% descuento aplicado)" : "";
            return $"{Producto.Nombre} x{Cantidad} - ${subtotal:F2}{descuento}";
        }
    }

}
