using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
 public class Tienda
    {
        public List<Producto> Productos { get; private set; }
        public List<Categoria> Categorias { get; private set; }

        public Tienda()
        {
            Categorias = new List<Categoria>();
            Productos = new List<Producto>();
            InicializarDatos();
        }

        private void InicializarDatos()
        {
            // Crear categorías
            var electronica = new Categoria("Electrónicos", "Dispositivos electrónicos y tecnología");
            var ropa = new Categoria("Ropa", "Vestimenta y accesorios");
            var hogar = new Categoria("Hogar", "Artículos para el hogar");
            var libros = new Categoria("Libros", "Literatura y material educativo");

            Categorias.AddRange(new[] { electronica, ropa, hogar, libros });

            // Crear productos
            Productos.AddRange(new[]
            {
                new Producto("Smartphone Samsung", 25000m, 15, electronica),
                new Producto("Laptop Dell", 45000m, 8, electronica),
                new Producto("Auriculares Bluetooth", 3500m, 25, electronica),
                new Producto("Camiseta Básica", 1200m, 50, ropa),
                new Producto("Jeans", 2800m, 30, ropa),
                new Producto("Zapatillas Deportivas", 4500m, 20, ropa),
                new Producto("Cafetera", 8500m, 12, hogar),
                new Producto("Aspiradora", 15000m, 6, hogar),
                new Producto("Set de Ollas", 6800m, 10, hogar),
                new Producto("El Quijote", 950m, 40, libros),
                new Producto("Programación en C#", 2200m, 15, libros),
                new Producto("Historia Argentina", 1800m, 25, libros)
            });
        }

        public List<Producto> ObtenerProductosPorCategoria(string nombreCategoria)
        {
            return Productos.Where(p => p.Categoria.Nombre.Equals(nombreCategoria, StringComparison.OrdinalIgnoreCase))
                           .ToList();
        }

        public Producto BuscarProductoPorCodigo(int codigo)
        {
            return Productos.FirstOrDefault(p => p.Codigo == codigo);
        }

        public void ProcesarCompra(Carrito carritoCompra)
        {
            if (carritoCompra.EstaVacio())
            {
                throw new InvalidOperationException("El carrito está vacío");
            }

            // Reducir stock de productos comprados
            foreach (var item in carritoCompra.ObtenerItems())
            {
                item.Producto.ReducirStock(item.Cantidad);
            }

            carritoCompra.Limpiar();
        }
    }
}
