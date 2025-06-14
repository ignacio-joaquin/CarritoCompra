using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    public class Carrito
    {
        private List<ItemCarrito> items;

        public Carrito()
        {
            items = new List<ItemCarrito>();
        }

        public List<ItemCarrito> ObtenerItems()
        {
            return new List<ItemCarrito>(items);
        }

        public void AgregarProducto(Producto producto, int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser positiva");
            }

            if (!producto.TieneStock(cantidad))
            {
                throw new InvalidOperationException($"Stock insuficiente. Disponible: {producto.Stock}");
            }

            // Verificar si el producto ya existe en el carrito
            var itemExistente = items.FirstOrDefault(i => i.Producto.Codigo == producto.Codigo);

            if (itemExistente != null)
            {
                int nuevaCantidad = itemExistente.Cantidad + cantidad;
                if (!producto.TieneStock(nuevaCantidad))
                {
                    throw new InvalidOperationException($"Stock insuficiente. Disponible: {producto.Stock}, en carrito: {itemExistente.Cantidad}");
                }
                itemExistente.Cantidad = nuevaCantidad;
            }
            else
            {
                items.Add(new ItemCarrito(producto, cantidad));
            }
        }

        public bool EliminarProducto(int codigoProducto)
        {
            var item = items.FirstOrDefault(i => i.Producto.Codigo == codigoProducto);
            if (item != null)
            {
                items.Remove(item);
                return true;
            }
            return false;
        }

        public decimal CalcularSubtotal()
        {
            return items.Sum(item => item.CalcularSubtotal());
        }

        public decimal CalcularTotal()
        {
            decimal subtotal = CalcularSubtotal();
            return subtotal * 1.21m; // Agregar 21% de IVA
        }

        public bool EstaVacio()
        {
            return items.Count == 0;
        }

        public void Limpiar()
        {
            items.Clear();
        }
    }
}
