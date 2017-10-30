using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationLinq
{
    public class Juego
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int MinJugadores { get; set; }
        public int MaxJugadores { get; set; }
    }

    public class JuegoMultitudinario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
     
    class Program
    {
        static void Main(string[] args)
        {
            List<Juego> resultado = consultar();
            foreach (Juego j in resultado)
                Console.WriteLine(String.Format("{0} ({1}). De {2} a {3} jugadores.", j.Nombre, j.Id, j.MinJugadores, j.MaxJugadores));

            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine();

            IEnumerable<JuegoMultitudinario> resultado1 = ConsultarJuegosMultitudinarios();
            foreach (JuegoMultitudinario j in resultado1)
                Console.WriteLine(String.Format("{0} ({1}). Creado el {2}.", j.Nombre, j.Id, j.FechaCreacion));

            Console.ReadLine();
        }

        private static IEnumerable<Juego> juegos = new List<Juego>()
        {
            new Juego {Id = 1, Nombre="Elix1", MinJugadores = 1, MaxJugadores = 5 },
            new Juego {Id = 2, Nombre="Elix2", MinJugadores = 3, MaxJugadores = 4 },
            new Juego {Id = 3, Nombre="Elix3", MinJugadores = 5, MaxJugadores = 7 },
            new Juego {Id = 4, Nombre="Elix4", MinJugadores = 2, MaxJugadores = 4 },
            new Juego {Id = 5, Nombre="Elix5", MinJugadores = 1, MaxJugadores = 3 }
        };

        private static List<Juego> consultar()
        {
            List<Juego> consulta  = (from j in juegos
                                    where j.MaxJugadores > 4
                                    orderby j.Nombre ascending
                                    select j).ToList<Juego>();

            return consulta;
        }

        private static IEnumerable<JuegoMultitudinario> ConsultarJuegosMultitudinarios()
        {
            IEnumerable<JuegoMultitudinario> consulta = from j in juegos
                                                        where j.MaxJugadores > 4
                                                        select new JuegoMultitudinario { Id = j.Id, Nombre = j.Nombre, FechaCreacion = DateTime.Now };
            return consulta;
        }
    }
}
