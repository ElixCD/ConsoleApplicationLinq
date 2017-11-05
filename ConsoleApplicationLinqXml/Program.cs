using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection; //MemberTypes;
using System.Xml.Linq;
using System.IO;


namespace ConsoleApplicationLinqXml
{
    class Program
    {
       
        static void Main(string[] args)
        {

            Juego objeto = new Juego();

            IEnumerable<MemberInfo> consulta = from miembro in objeto.GetType().GetMembers()
                                               where (miembro != null)
                                               orderby miembro.MemberType ascending
                                               select miembro;

            //foreach (MemberInfo salida in consulta)
            //    Console.WriteLine(salida.GetType().ToString());

            /*
            IEnumerable<MemberTypes> consulta = from miembro in Enum.GetValues(typeof(MemberTypes))
                                           select miembro;
                                           */

            IEnumerable<MemberTypes> consultaMiembros = from miembro in Enum.GetValues(typeof(MemberTypes)).Cast<MemberTypes>()
                                                        select miembro;
            
            XDocument documentoInfoClase = new XDocument(
                new XElement(objeto.GetType().Name));

            foreach(MemberTypes tipo in consultaMiembros)
            {
                XElement elemento = new XElement(tipo.ToString());
                //elemento.Add("Name",
                //    from miembro in Enum.GetValues(typeof(MemberTypes)).Cast<MemberTypes>()
                //    where ((miembro.ToString() != null) && (miembro.ToString() == elemento.Name))
                //    select new XElement(miembro.ToString() + "Miembro")
                //    );
                documentoInfoClase.Root.Add(elemento);
                
            }

            XDocument documentoInfoClase1 = new XDocument(
                new XElement(objeto.GetType().Name, // Nodo raíz
                    from tipoMiembro in Enum.GetValues(typeof(MemberTypes)).Cast<MemberTypes>()
                    select new XElement(tipoMiembro.ToString() + "Member",// Cada iteración devolverá un nuevo nodo
                        // [SENTENCIA LINQ QUE GENERARÁ UN LISTADO DE XElement
                        //  CUYO TIPO COINCIDA CON tipoMiembro]
                        // Listado con los nodos correspondientes a los MemberType de la clase
                        from miembro in objeto.GetType().GetMembers()
                        where ((miembro != null) && (miembro.MemberType == tipoMiembro))
                        orderby miembro.MemberType ascending
                        select new XElement(miembro.MemberType.ToString(),
                            new XAttribute("name", miembro.Name),
                            new XAttribute("value", 
                                miembro.GetType(). IsAssignableFrom(typeof(PropertyInfo)) ? ((PropertyInfo)miembro).GetValue(objeto, null) : miembro.ToString())
                        )
                    )
                )
            );

            Console.ReadLine();
        }
                
    }

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

}
