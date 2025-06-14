using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
public class Producto
    {
        private static int contadorCodigo = 1;
        
        public int Codigo { get; private set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Categoria Categoria { get; set; }

        public Producto(string nombre, decimal precio, int stock, Categoria categoria)
        {
            Codigo = contadorCodigo++;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Categoria = categoria;
        }

        public bool TieneStock(int cantidad)
        {
            return Stock >= cantidad;
        }

        public void ReducirStock(int cantidad)
        {
            if (TieneStock(cantidad))
            {
                Stock -= cantidad;
            }
            else
            {
                throw new InvalidOperationException("Stock insuficiente");
            }
        }

        public override string ToString()
        {
            return $"[{Codigo}] {Nombre} - ${Precio:F2} (Stock: {Stock}) - Categoría: {Categoria.Nombre}";
        }
    }
}
