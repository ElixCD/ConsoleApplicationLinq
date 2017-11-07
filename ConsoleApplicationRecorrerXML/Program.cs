using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using System.IO;

namespace ConsoleApplicationRecorrerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Juego objeto = new Juego();

            XDocument docXML = new XDocument(
                new XElement(objeto.GetType().Name,
                    from tipoMiembro in Enum.GetValues(typeof(MemberTypes)).Cast<MemberTypes>()
                    select new XElement(tipoMiembro.ToString() + "Member",
                        from miembro in objeto.GetType().GetMembers()
                        where ((miembro != null) && (miembro.MemberType == tipoMiembro))
                        orderby miembro.MemberType ascending
                        select new XElement( miembro.MemberType.ToString(),
                            new XAttribute("name", miembro.Name),
                            new XAttribute("value", 
                                miembro.GetType().IsAssignableFrom(typeof(PropertyInfo)) ? ((PropertyInfo)miembro).GetValue(objeto, null) : miembro.ToString())
                        )
                    )
                )
            );

            StringWriter writer = new StringWriterEncode(Encoding.UTF8);
            docXML.Save(writer);

            // Iteramos sobre los elemetos descendientes de Juego/MethodMemeber y recuperamos
            // cuyo nombre sea "Method". Finalmente, calculamos la cuenta.
            int numMetodos = (from metodo in docXML.Elements("Juego").Elements("MethodMember").Descendants()
                              where metodo.Name == "Method"
                              select metodo).Count();

            // Itermaos sobre TODOS los descendientes del documento.
            // En él, seleccionamos todos aquellos nodos cuyo nombre sea "Property"
            // Sobre ese nodo, creamos un listado  con todos sus atributos (nombre y valor)
            var atributosPropiedades = (from nodo in docXML.Descendants()
                                        where nodo.Name == "Property"
                                        select new{
                                            NombreNodo = nodo.Name,
                                            Atributos = (from atributo in nodo.Attributes()
                                                         select new
                                                         {
                                                             Nombre = atributo.Name,
                                                             Valor = atributo.Value
                                                         })
                                            }
                                        );

            Console.WriteLine(String.Format("La clase posee un total de {0} metodos.", numMetodos));

            foreach (var nodo in atributosPropiedades)
            {
                Console.WriteLine(string.Format("\nELEMENTO: {0}", nodo.NombreNodo));
                foreach(var atributo in nodo.Atributos)
                {
                    Console.WriteLine(string.Format("\tNombre: {0} \tValor: {1}", atributo.Nombre, atributo.Valor));
                }
            }
            Console.ReadKey();
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

    // Clase para cambiar la codificación del documento XML
    public class StringWriterEncode : StringWriter
    {
        // Añadimos un atributo que almacenara la nueva codificación
        private Encoding encoding;

        // Creamos un nuevo constructor que permita asociar una nueva codificación
        public StringWriterEncode(Encoding e) : base()
        {
            this.encoding = e;
        }

        // Sobrecargamos el getter que devuelve la codificación
        public override Encoding Encoding
        {
            get
            {
                return encoding;
            }
        }

        // Añadimos un nuevo getter que permita recuperar la codificación por defecto
        public Encoding DefaultEncoding
        {
            get
            {
                return base.Encoding;
            }
        }
    }
}
