using System;

namespace CarritoCompras
{
    class Program
    {
        private static Tienda tienda = new Tienda();
        private static Carrito carrito = new Carrito();

        static void Main(string[] args)
        {
            Console.WriteLine("=== BIENVENIDO A LA TIENDA ONLINE ===\n");

            bool continuar = true;
            while (continuar)
            {
                MostrarMenu();
                int opcion = LeerOpcion();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            MostrarCategorias();
                            break;
                        case 2:
                            MostrarTodosLosProductos();
                            break;
                        case 3:
                            MostrarProductosPorCategoria();
                            break;
                        case 4:
                            AgregarProductoAlCarrito();
                            break;
                        case 5:
                            EliminarProductoDelCarrito();
                            break;
                        case 6:
                            MostrarCarrito();
                            break;
                        case 7:
                            MostrarTotalAPagar();
                            break;
                        case 8:
                            FinalizarCompra();
                            break;
                        case 9:
                            continuar = false;
                            Console.WriteLine("¡Gracias por visitarnos!");
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Intente nuevamente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("=== MENÚ PRINCIPAL ===");
            Console.WriteLine("1. Ver categorías disponibles");
            Console.WriteLine("2. Ver todos los productos");
            Console.WriteLine("3. Ver productos por categoría");
            Console.WriteLine("4. Agregar producto al carrito");
            Console.WriteLine("5. Eliminar producto del carrito");
            Console.WriteLine("6. Ver contenido del carrito");
            Console.WriteLine("7. Ver total a pagar");
            Console.WriteLine("8. Finalizar compra");
            Console.WriteLine("9. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        static int LeerOpcion()
        {
            if (int.TryParse(Console.ReadLine(), out int opcion))
            {
                return opcion;
            }
            return -1;
        }

        static void MostrarCategorias()
        {
            Console.WriteLine("\n=== CATEGORÍAS DISPONIBLES ===");
            foreach (var categoria in tienda.Categorias)
            {
                Console.WriteLine($"• {categoria}");
            }
        }

        static void MostrarTodosLosProductos()
        {
            Console.WriteLine("\n=== TODOS LOS PRODUCTOS ===");
            foreach (var producto in tienda.Productos)
            {
                Console.WriteLine($"• {producto}");
            }
        }

        static void MostrarProductosPorCategoria()
        {
            Console.WriteLine("\n=== PRODUCTOS POR CATEGORÍA ===");
            Console.WriteLine("Categorías disponibles:");
            for (int i = 0; i < tienda.Categorias.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tienda.Categorias[i].Nombre}");
            }

            Console.Write("Seleccione una categoría (número): ");
            if (int.TryParse(Console.ReadLine(), out int opcion) &&
                opcion >= 1 && opcion <= tienda.Categorias.Count)
            {
                string nombreCategoria = tienda.Categorias[opcion - 1].Nombre;
                var productos = tienda.ObtenerProductosPorCategoria(nombreCategoria);

                Console.WriteLine($"\n--- PRODUCTOS DE {nombreCategoria.ToUpper()} ---");
                foreach (var producto in productos)
                {
                    Console.WriteLine($"• {producto}");
                }
            }
            else
            {
                Console.WriteLine("Categoría inválida.");
            }
        }

        static void AgregarProductoAlCarrito()
        {
            Console.WriteLine("\n=== AGREGAR PRODUCTO AL CARRITO ===");
            Console.Write("Ingrese el código del producto: ");

            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                var producto = tienda.BuscarProductoPorCodigo(codigo);
                if (producto == null)
                {
                    Console.WriteLine("Producto no encontrado.");
                    return;
                }

                Console.WriteLine($"Producto seleccionado: {producto}");
                Console.Write("Ingrese la cantidad: ");

                if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
                {
                    carrito.AgregarProducto(producto, cantidad);
                    Console.WriteLine($"✓ {cantidad} unidad(es) de {producto.Nombre} agregada(s) al carrito.");
                }
                else
                {
                    Console.WriteLine("Cantidad inválida.");
                }
            }
            else
            {
                Console.WriteLine("Código inválido.");
            }
        }

        static void EliminarProductoDelCarrito()
        {
            Console.WriteLine("\n=== ELIMINAR PRODUCTO DEL CARRITO ===");

            if (carrito.EstaVacio())
            {
                Console.WriteLine("El carrito está vacío.");
                return;
            }

            MostrarCarrito();
            Console.Write("Ingrese el código del producto a eliminar: ");

            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                if (carrito.EliminarProducto(codigo))
                {
                    Console.WriteLine("✓ Producto eliminado del carrito.");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado en el carrito.");
                }
            }
            else
            {
                Console.WriteLine("Código inválido.");
            }
        }

        static void MostrarCarrito()
        {
            Console.WriteLine("\n=== CONTENIDO DEL CARRITO ===");

            if (carrito.EstaVacio())
            {
                Console.WriteLine("El carrito está vacío.");
                return;
            }

            foreach (var item in carrito.ObtenerItems())
            {
                Console.WriteLine($"• [{item.Producto.Codigo}] {item}");
            }
        }

        static void MostrarTotalAPagar()
        {
            Console.WriteLine("\n=== RESUMEN DE COMPRA ===");

            if (carrito.EstaVacio())
            {
                Console.WriteLine("El carrito está vacío.");
                return;
            }

            MostrarCarrito();

            decimal subtotal = carrito.CalcularSubtotal();
            decimal iva = subtotal * 0.21m;
            decimal total = carrito.CalcularTotal();

            Console.WriteLine($"\nSubtotal: ${subtotal:F2}");
            Console.WriteLine($"IVA (21%): ${iva:F2}");
            Console.WriteLine($"TOTAL A PAGAR: ${total:F2}");
        }

        static void FinalizarCompra()
        {
            Console.WriteLine("\n=== FINALIZAR COMPRA ===");

            if (carrito.EstaVacio())
            {
                Console.WriteLine("El carrito está vacío. No se puede finalizar la compra.");
                return;
            }

            MostrarTotalAPagar();

            Console.Write("\n¿Confirma la compra? (s/n): ");
            string confirmacion = Console.ReadLine()?.ToLower();

            if (confirmacion == "s" || confirmacion == "si")
            {
                tienda.ProcesarCompra(carrito);
                Console.WriteLine("\n✓ ¡Compra realizada exitosamente!");
                Console.WriteLine("Gracias por su compra. El stock ha sido actualizado.");
            }
            else
            {
                Console.WriteLine("Compra cancelada.");
            }
        }
    }
}
