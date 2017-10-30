using System;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ConsoleApplicationXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(GenerarXML());
            Console.Read();
        }

        public static string GenerarXML()
        {
            XDocument documentoXML = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("configuration",
                    new XElement("startup",
                        new XElement("supportedRuntime",
                            new XAttribute("version", "v4.0"),
                            new XAttribute("sku", ".NETFramework,Version=v4.5")
                        )
                    )
                )
            );

            // Para poder obtener la cabecera del documento, pero obtiene el documento con utf-16
            StringWriter writer = new StringWriter();
            documentoXML.Save(writer);

            // Volvemos a guardar el documento pero ahora utilizando la clase creada para que pueda cambiar la codificación
            StringWriter writer0 = new StringWriteEncode(Encoding.UTF8);
            documentoXML.Save(writer0);

            return writer0.GetStringBuilder().ToString();
        }
    }

    // Clase para cambiar la codificación del documento XML
    public class StringWriteEncode : StringWriter
    {
        // Añadimos un atributo que almacenara la nueva codificación
        private Encoding encoding;

        // Creamos un nuevo constructor que permita asociar una nueva codificación
        public StringWriteEncode(Encoding e) : base()
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
