using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ClassLibraryLinq;

namespace ConsoleApplicationLinqOps1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================= TIPO ANONIMO ==================");
            // Declaramos (y cumplimentamos) el tipo anónimo
            var tipoAnonimo = new { PropiedadEntera = 1, PropiedadCadena = "cadena" };

            // Mostramos por pantalla su contenido
            Console.WriteLine(String.Format("El contenido del tipo anónimo es {0} y {1}",
                tipoAnonimo.PropiedadEntera, tipoAnonimo.PropiedadCadena));

            Console.WriteLine("\n================= SELECT ==================");

            var listaNombreFechaClientes01 = from c in DataLists.ListaClientes
                                             select c.Nombre;

            foreach (var nombreFecha in listaNombreFechaClientes01)
                Console.WriteLine(string.Format("El cliente {0} ", nombreFecha));

            Console.WriteLine("\n================= SELECT VARIOS CAMPOS ==================");

            var listaNombreFechaClientes02 = from c in DataLists.ListaClientes
                                             select new { NombreCliente = c.Nombre, FechaNacimiento = c.FechaNac };

            foreach (var nombreFecha in listaNombreFechaClientes02)
                Console.WriteLine(string.Format("El cliente {0} nació el {1}",
                    nombreFecha.NombreCliente, nombreFecha.FechaNacimiento));

            Console.WriteLine("\n================= WHERE ==================");
            var productosDeMasDeSieteEuros = from p in DataLists.ListaProductos
                                             where p.Precio > 7
                                             select p;

            foreach (var articulo in productosDeMasDeSieteEuros)
                Console.WriteLine(String.Format("{0} {1} {2}", articulo.Id, articulo.Descripcion, articulo.Precio));

            Console.WriteLine("\n================= WHERE MULTICONDICIÓN ==================");
            productosDeMasDeSieteEuros = from p in DataLists.ListaProductos
                                         where (p.Precio > 7) || ((p.Precio > 3) && (p.Precio < 5))
                                         select p;

            foreach (var articulo in productosDeMasDeSieteEuros)
                Console.WriteLine(String.Format("{0} {1} {2}", articulo.Id, articulo.Descripcion, articulo.Precio));

            Console.WriteLine("\n================= SELECT VARIOS OBJETOS ==================");

            var listaPedidosClientes = from c in DataLists.ListaClientes    // Lista de clientes
                                       from p in DataLists.ListaPedidos     // Lista de pedidos
                                       where p.IdCliente == c.Id            // Filtro: ID del pedido == ID del cliente
                                       select new { NumPedido = p.Id, FechaPedido = p.FechaPedido, NombreCliente = c.Nombre };

            foreach (var pedidoCliente in listaPedidosClientes)
            {
                Console.WriteLine(string.Format("El pedido {0} enviado en {1} pertenece a {2}.", pedidoCliente.NumPedido, pedidoCliente.FechaPedido, pedidoCliente.NombreCliente));
            }

            Console.WriteLine("\n================= SELECT VARIOS OBJETOS JOIN ==================");

            listaPedidosClientes = from c in DataLists.ListaClientes    // Lista de clientes
                                   join p in DataLists.ListaPedidos on c.Id equals p.IdCliente  // Equivale al INNER JOIN TABLA2 ON TABLA1.ID_PK = TABLA2.ID_FK
                                   select new { NumPedido = p.Id, FechaPedido = p.FechaPedido, NombreCliente = c.Nombre };

            foreach (var pedidoCliente in listaPedidosClientes)
            {
                Console.WriteLine(string.Format("El pedido {0} enviado en {1} pertenece a {2}.", pedidoCliente.NumPedido, pedidoCliente.FechaPedido, pedidoCliente.NombreCliente));
            }

            Console.ReadKey();
        }
    }
}
