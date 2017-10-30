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
        /*
        IEnumerable<MemberTypes> consulta = from miembro in Enum.GetValues(typeof(MemberTypes))
                                            select miembro;
                                            */

        IEnumerable<MemberTypes> consultaMiembros = from miembro in Enum.GetValues(typeof(MemberTypes)).Cast<MemberTypes>()
                                                    select miembro;

        static void Main(string[] args)
        {
           

        }
                
    }

}
