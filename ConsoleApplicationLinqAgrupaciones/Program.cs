using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClassLibraryLinq;

namespace ConsoleApplicationLinqAgrupaciones
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================= Agrupación sencilla =================");
            var agrupacion0 = from p in DataLists.ListaPedidos
                             group p by p.IdCliente into grupo
                             select grupo;

            foreach (var grupo in agrupacion0)
            {
                Console.WriteLine("ID Cliente: " + grupo.Key);
                foreach (var objetoAgrupado in grupo)
                {
                    Console.Write("\t\tPedido n° " + objetoAgrupado.Id + ": " + objetoAgrupado.FechaPedido + "]" + Environment.NewLine);
                }
            }

            Console.WriteLine("\n================= Agrupación multiple =================");

            var agrupacion1 = from p in DataLists.ListaPedidos
                         join c in DataLists.ListaClientes on p.IdCliente equals c.Id
                         group p by new { p.IdCliente, c.Nombre } into grupo
                         select grupo;

            foreach (var grupo in agrupacion1)
            {
                Console.WriteLine("Nombre cliente: " + grupo.Key.Nombre + " (ID: " + grupo.Key.IdCliente + ")");
                foreach (var objetoAgrupado in grupo)
                {
                    Console.Write("\tPedido n° " + objetoAgrupado.Id + ": " + objetoAgrupado.FechaPedido + "]" + Environment.NewLine);
                }
            }

            Console.WriteLine("\n================= Agrupación anidada =================");

            var consultaClientes = from pedido in DataLists.ListaPedidos
                                   join cliente in DataLists.ListaClientes
                                   on pedido.IdCliente equals cliente.Id
                                   group pedido by new
                                   {
                                       cliente.Id,
                                       cliente.Nombre
                                   } into pedidosPorCliente
                                   select pedidosPorCliente;    // Key = {Id de cliente, Nombre de cliente
                                                                // Grupo = List

            var consultaPedidos = from lineaPedido in DataLists.ListaLineaPedido
                                  join pedido in DataLists.ListaPedidos
                                    on lineaPedido.IdPedido equals pedido.Id
                                  group lineaPedido by new
                                  {
                                      pedido.Id,
                                      pedido.FechaPedido
                                  } into lineasPorPedido
                                  select lineasPorPedido;   // Key = {Id de pedido, Fecha de realización del pedido}
                                                            // Grupo = List

            var lineaProducto = from linea in DataLists.ListaLineaPedido
                                join producto in DataLists.ListaProductos
                                    on linea.IdProducto equals producto.Id
                                select new
                                {
                                    IdLineaPedido = linea.Id,
                                    Nombre = producto.Descripcion,
                                    Cantidad = linea.Cantidad,
                                    PrecioUnitario = producto.Precio,
                                    PrecioTotal = (producto.Precio * linea.Cantidad)
                                };

            var consulta = from pedido in DataLists.ListaPedidos
                           join cliente in DataLists.ListaClientes
                            on pedido.IdCliente equals cliente.Id
                           group pedido by new
                           {
                               cliente.Id,
                               cliente.Nombre
                           } into pedidosPorCliente
                           select
                            from lineaPedido in DataLists.ListaLineaPedido
                            join pedido in DataLists.ListaPedidos
                                on lineaPedido.IdPedido equals pedido.Id
                            group lineaPedido by new
                            {
                                pedido.Id,
                                pedido.FechaPedido
                            } into lineasPorPedido
                            select lineasPorPedido;

            //consulta = from 


            Console.ReadKey();
        }
    }
}
